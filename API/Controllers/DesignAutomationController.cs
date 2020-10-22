using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using API.Helpers.Http.Restsharp;
using API.Hubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using Newtonsoft.Json.Linq;
using Repository.Abstract;
using RestSharp;
using Services.Adapters.Abstract;

namespace API.Controllers
{
    [ApiController]
    public class DesignAutomationController : ControllerBase
    {
        private readonly IAppBundleServiceAdapter _serviceAdapter;
        private readonly IEngineServiceAdapter _engineServiceAdapter;
        private readonly IActivityServiceAdapter _activityServiceAdapter;
        private readonly IWorkItemServiceAdapter _workItemServiceAdapter;
        private readonly IAutoDeskDesignAutomationRepository _repository;

        private IWebHostEnvironment _envoironment;
        private IHubContext<DesignAutomationHub> _designAutomationHub;

        public DesignAutomationController(IAppBundleServiceAdapter serviceAdapter,
            IEngineServiceAdapter engineServiceAdapter, IActivityServiceAdapter activityServiceAdapter,
            IWorkItemServiceAdapter workItemServiceAdapter, IWebHostEnvironment environment,
            IAutoDeskDesignAutomationRepository repository,
            IHubContext<DesignAutomationHub> designAutomationHub)
        {
            this._serviceAdapter = serviceAdapter;
            this._envoironment = environment;
            this._engineServiceAdapter = engineServiceAdapter;
            this._designAutomationHub = designAutomationHub;
            this._activityServiceAdapter = activityServiceAdapter;
            this._workItemServiceAdapter = workItemServiceAdapter;
            this._repository = repository;
        }


        [HttpPost]
        [Route("api/forge/designautomation/appbundles")]
        public async Task<IActionResult> CreateAppBundle([FromBody] JObject bundle)
        {
            string localBundlesFolder = Path.Combine(_envoironment.WebRootPath, "bundles");
            dynamic result = await _serviceAdapter.CreateAppBundleAsync(bundle, localBundlesFolder);

            dynamic newVersion = result.newAppVersion;
            dynamic packageZipFileName = result.packageZipFileName;
            dynamic UploadParameters = newVersion.UploadParameters;
            dynamic QualifiedAppBundleId = result.QualifiedAppBundleId;

            await RestSharpHttpEnvoirmentHelper.CreateHttpFormDataEnvoirment(UploadParameters.EndpointURL, Method.POST,
                UploadParameters.FormData, packageZipFileName, "no-cache");


            return Ok(new
            {
                AppBundle = QualifiedAppBundleId,
                Version = newVersion.Version

            });

        }

        [HttpPost]
        [Route("api/forge/designautomation/workitems")]
        public async Task<IActionResult> StartWorkitem([FromForm] StartWorkItemInput input)
        {
            dynamic result = await _workItemServiceAdapter.StartWorkItem(input, _envoironment.WebRootPath);

            return Ok(result);
        }

        [HttpPost]
        [Route("api/forge/designautomation/activities")]
        public async Task<IActionResult> CreateActivity([FromBody] JObject activitySpecs)
        {
            dynamic result = await _activityServiceAdapter.CreateActivity(activitySpecs);

            return Ok(result);
        }

        [HttpGet]
        [Route("api/forge/designautomation/activities")]
        public async Task<List<string>> GetDefinedActivities()
        {
            List<string> result = await _activityServiceAdapter.GetDefinedActivities();
            return result;
        }

        [HttpGet]
        [Route("api/forge/designautomation/engines")]
        public async Task<List<string>> GetAvailableEngines()
        {
            return await _engineServiceAdapter.GetEnginesToStringAdapter();
        }

        [HttpPost]
        [Route("/api/forge/callback/designautomation")]
        public async Task<IActionResult> OnCallback(string id, string outputFileName, [FromBody] dynamic body)
        {
            try
            {
                JObject bodyJson = JObject.Parse((string)body.ToString());
                await OnComplete(id, bodyJson.ToString());

                var client = new RestClient(bodyJson["reportUrl"].Value<string>());
                var request = new RestRequest(string.Empty);

                string report = _workItemServiceAdapter.CreateToLSendingLog(client, request);
                await OnComplete(id, report);

                dynamic signedUrl = await _workItemServiceAdapter.GenerateSignedUrl(outputFileName);
                await OnDownloadResult(id, (string)(signedUrl.Data.signedUrl));


            }

            catch{}

            return Ok();
        }
        [HttpGet]
        [Route("api/appbundles")]
        public string[] GetLocalBundles()
        {
            string localBundlesFolder = Path.Combine(_envoironment.WebRootPath, "bundles");

            return _serviceAdapter.GetLocalBundles(localBundlesFolder);
        }

        [HttpDelete]
        [Route("api/forge/designautomation/account")]
        public async Task<IActionResult> ClearAccount()
        {
            await _repository.DeleteForgeApp("me");
            return Ok();
        }

        private async Task OnDownloadResult(string id, string arg)
        {
            await _designAutomationHub.Clients.Client(id).SendAsync("downloadResult", arg);
        }

        private async Task OnComplete(string id, string arg)
        {
            await _designAutomationHub.Clients.Client(id).SendAsync("onComplete", arg);
        }
    }
}