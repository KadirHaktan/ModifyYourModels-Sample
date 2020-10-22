using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;
using RestSharp;

namespace Services.Adapters.Abstract
{
    public interface IWorkItemServiceAdapter
    {
        Task<dynamic> StartWorkItem(StartWorkItemInput input, string rootPath);

        string CreateToLSendingLog(RestClient client, RestRequest request);

        Task<dynamic> GenerateSignedUrl(string outputFileName);

    }
}
