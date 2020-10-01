using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace API.Helpers.Http.Restsharp
{
    public static class RestSharpHttpEnvoirmentHelper
    {
        public static async Task CreateHttpFormDataEnvoirment(string url, Method method, dynamic FormData, string zipFileName, string cacheControl)
        {
            RestClient uploadClient = new RestClient(url);
            RestRequest request = new RestRequest(string.Empty, method);
            request.AlwaysMultipartFormData = true;
            foreach (KeyValuePair<string, string> x in FormData)
                request.AddParameter(x.Key, x.Value);
            request.AddFile("file", zipFileName);
            request.AddHeader("Cache-Control", cacheControl);
            await uploadClient.ExecuteTaskAsync(request);
        }
    }
}
