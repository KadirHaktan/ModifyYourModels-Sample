using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;
using RestSharp;
using Services.Abstract;
using Services.Adapters.Abstract;

namespace Services.Adapters.Concerete
{
    public class WorkItemServiceAdapter : IWorkItemServiceAdapter
    {
        private readonly IWorkItemService _service;

        public WorkItemServiceAdapter(IWorkItemService service)
        {
            this._service = service;

        }

        public string CreateToLSendingLog(RestClient client, RestRequest request)
        {
            return _service.CreateToLSendingLog(client, request);
        }

        public async Task<dynamic> GenerateSignedUrl(string outputFileName)
        {
            return await _service.GenerateSignedUrl(outputFileName);
        }

        public async Task<dynamic> StartWorkItem(StartWorkItemInput input, string rootPath)
        {
            return await _service.StartWorkItem(input, rootPath);
        }
    }
}
