using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Forge;
using Autodesk.Forge.DesignAutomation;
using Autodesk.Forge.DesignAutomation.Model;
using Autodesk.Forge.Model;
using Core.Extensions;
using Core.Interfaces.Adapters;
using Core.Utilities.Configurations;
using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Abstract;
using RestSharp;
using Services.Abstract;
using WorkItem = Autodesk.Forge.DesignAutomation.Model.WorkItem;

namespace Services.Concerete
{
    public class AutoDeskWorkItemService : IWorkItemService
    {
        private readonly IAutoDeskDesignAutomationRepository _repository;
        private readonly IAuthServiceAdapter _serviceAdapter;

        string NickName = AppSettings.Get("FORGE_CLIENT_ID");
        string WebHookUrl = AppSettings.Get("FORGE_WEBHOOK_URL");

      

        public AutoDeskWorkItemService(IAutoDeskDesignAutomationRepository repository,IAuthServiceAdapter serviceAdapter)
        {
            this._repository = repository;
            this._serviceAdapter = serviceAdapter;
        }

     


        public async Task<dynamic> StartWorkItem(StartWorkItemInput input,string rootPath)
        {
            string fileSavePath = await SaveTheFileOnTheServer(input.inputFile, rootPath);

            string bucketKey = NickName.ToLower().ReplyToCharToEnglishFormat() + "-designautomation7";

            var accessToken = await AccessToken();

            await CreateBucketsApi(SetAccessTokenToBuckets(accessToken), bucketKey);

            string inputFileOss = await UploadInputFileToObject(accessToken, fileSavePath, input.inputFile, bucketKey);
            string inputFileArgumentUrl = CreateArgumentUrl(bucketKey, inputFileOss);
            var inputFileArgument = CreateArgument(inputFileArgumentUrl, accessToken);

            dynamic inputJson = new JObject();
            var validationData = BasicInputValidation(input.data);
            inputJson.Width = validationData["widthParam"];
            inputJson.Height = validationData["heigthParam"];
            string inputJsonArgumentUrl = "data:application/json, " + ((JObject) inputJson).ToString(Formatting.None).Replace("\"", "'");
            var inputJsonArgument = CreateArgument(inputJsonArgumentUrl);


            string outputFileNameOSS = CreateFileNameOss(input.inputFile, "output");
            string outputFileArgumentUrl = CreateArgumentUrl(bucketKey, outputFileNameOSS);
            var outputFileArgument = CreateArgument(outputFileArgumentUrl, accessToken, Verb.Put);
            string browerConnectionId = validationData["browerConnectionId"];
            string callbackUrl = string.Format("{0}/api/forge/callback/designautomation?id={1}&outputFileName={2}",WebHookUrl,browerConnectionId, outputFileNameOSS);



            return await GetWorkItemId(inputFileArgument, outputFileArgument, inputJsonArgument, callbackUrl, validationData["activityName"]);


        }

        public string CreateToLSendingLog(RestClient client, RestRequest request)
        {
            byte[] bs = client.DownloadData(request);
            string report = System.Text.Encoding.Default.GetString(bs);

            return report;
        }

        public async Task<dynamic> GenerateSignedUrl(string outputFileName)
        {
            string bucketKey = NickName.ToLower().ReplyToCharToEnglishFormat() + "-designautomation7";
            ObjectsApi objectsApi = new ObjectsApi();
            dynamic signedUrl = await objectsApi.CreateSignedResourceAsyncWithHttpInfo(bucketKey, outputFileName, new PostBucketsSigned(10), "read");

            return signedUrl;
        }

        private string CreateArgumentUrl(string bucketKey, string FileNameOss)
        {
            return string.Format("https://developer.api.autodesk.com/oss/v2/buckets/{0}/objects/{1}", bucketKey, FileNameOss);
        }

        private XrefTreeArgument CreateArgument(string url)
        {
            return new XrefTreeArgument()
            {
                Url = url
            };
        }

        private XrefTreeArgument CreateArgument(string url,dynamic accessToken,Verb verb)
        {
           return new XrefTreeArgument()
            {
                Url = url,
                Headers = new Dictionary<string, string>()
                {
                    {"Authorization", "Bearer " + accessToken}
                },
                Verb = verb
            };
        }
        private XrefTreeArgument CreateArgument(string url, dynamic accessToken)
        {
            return new XrefTreeArgument()
            {
                Url = url,
                Headers = new Dictionary<string, string>()
                {
                    {"Authorization", "Bearer " + accessToken}
                },
                
            };
        }
        public async Task<dynamic> AccessToken()
        {
            dynamic oauth = await _serviceAdapter.GetAccessTokenAdapterAsync();
            return oauth.access_token;
            
        }

        private Dictionary<string,string> BasicInputValidation(string data)
        {
            JObject workItemData = JObject.Parse(data);
            string widthParam = workItemData["width"].Value<string>();
            string heigthParam = workItemData["height"].Value<string>();
            string activityName = string.Format("{0}.{1}", NickName, workItemData["activityName"].Value<string>());
            string browerConnectionId = workItemData["browerConnectionId"].Value<string>();

            return new Dictionary<string, string>()
            {
                {"widthParam", widthParam},
                {"heigthParam", heigthParam},
                {"activityName", activityName},
                {"browerConnectionId",browerConnectionId}
              

            };
        }

        private async Task<string> SaveTheFileOnTheServer(IFormFile inputFile,string rootPath)
        {
            var fileSavePath = Path.Combine(rootPath, Path.GetFileName(inputFile.FileName));
            using (var stream = new FileStream(fileSavePath, FileMode.Create)) 
            { 
                await inputFile.CopyToAsync(stream); 
            }

            return fileSavePath;
        }

        private BucketsApi SetAccessTokenToBuckets(dynamic accessToken)
        {
            
            return new BucketsApi()
            {
                Configuration=
                {
                    AccessToken = accessToken
                }
            };

        }

        private ObjectsApi SetAccessTokenToObjects(dynamic accessToken)
        {

            return new ObjectsApi()
            {
                Configuration =
                {
                    AccessToken = accessToken
                }
            };

        }

        private async Task<dynamic> CreateBucketsApi(BucketsApi bucketsApi,string bucketKey)
        {
            try
            {
                PostBucketsPayload postBucketsPayload=new PostBucketsPayload(bucketKey,null,PostBucketsPayload.PolicyKeyEnum.Transient);
                var result= await bucketsApi.CreateBucketAsync(postBucketsPayload, "US");
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private async Task<string> UploadInputFileToObject(dynamic accessToken,string fileSavePath,IFormFile inputFile,string bucketKey)
        {
            try
            {
                var inputFileNameOSS = CreateFileNameOss(inputFile, "input");

                ObjectsApi objects = SetAccessTokenToObjects(accessToken);

                using (StreamReader streamReader = new StreamReader(fileSavePath))
                {
                    await objects.UploadObjectAsync(bucketKey, inputFileNameOSS, (int)streamReader.BaseStream.Length, streamReader.BaseStream, "application/octet-stream");
                }
                System.IO.File.Delete(fileSavePath);

                return inputFileNameOSS;

            }

            catch(Exception e)
            {
                throw e;
            }
        }

        private string CreateFileNameOss(IFormFile inputFile,string option)
        {
            string FileNameOSS = string.Format("{0}_{1}_{2}", DateTime.Now.ToString("yyyyMMddhhmmss"),
                option,Path.GetFileName(inputFile.FileName));
            return FileNameOSS;
        }

        private async Task<dynamic> GetWorkItemId(XrefTreeArgument inputFileArgument,XrefTreeArgument outputFileArgument,XrefTreeArgument inputJsonArgument,string callbackUrl,string ActivityId )
        {
            WorkItem workItemSpec = new WorkItem()
            {
                ActivityId = ActivityId,
                Arguments = new Dictionary<string, IArgument>()
                {
                    { "inputFile", inputFileArgument },
                    { "inputJson",  inputJsonArgument },
                    { "outputFile", outputFileArgument },
                    { "onComplete", new XrefTreeArgument { Verb = Verb.Post, Url = callbackUrl } }
                }
            };
            WorkItemStatus workItemStatus = await _repository.CreateWorkItem(workItemSpec);

            return new
            {
                WorkItemId = workItemStatus.Id
            };
        }

      
    }
}
