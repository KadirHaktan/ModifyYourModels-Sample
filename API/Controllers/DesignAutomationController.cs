﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers.Http.Restsharp;
using API.Hubs;
using Entities.Concerete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using RestSharp;
using Services.Adapters.Abstract;

namespace API.Controllers
{
    [ApiController]
    public class DesignAutomationController : ControllerBase
    {
        private readonly IAppBundleServiceAdapter<AutoDeskAppBundle> _serviceAdapter;
        private readonly IEngineServiceAdapter _engineServiceAdapter;

        private IWebHostEnvironment _envoironment;
        private IHubContext<DesignAutomationHub> _designAutomationHub;
        
        public DesignAutomationController(IAppBundleServiceAdapter<AutoDeskAppBundle> serviceAdapter, IEngineServiceAdapter engineServiceAdapter, IWebHostEnvironment environment, IHubContext<DesignAutomationHub>designAutomationHub)
        {
            this._serviceAdapter = serviceAdapter;
            this._envoironment = environment;
            this._engineServiceAdapter = engineServiceAdapter;
            this._designAutomationHub = designAutomationHub;
        }

        [HttpPost]
        [Route("api/forge/designautomation/appbundles")]
        public async Task<IActionResult> CreateAppBundle([FromBody] AutoDeskAppBundle bundle)
        {
            string localBundlesFolder = Path.Combine(_envoironment.WebRootPath, "bundles");
            dynamic result=_serviceAdapter.CreateAppBundleAsync(bundle,localBundlesFolder);

            dynamic newVersion = result.newVersion;
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

        
    }
}