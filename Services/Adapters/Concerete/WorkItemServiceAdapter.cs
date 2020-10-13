using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;
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
        public async Task<dynamic> StartWorkItem(StartWorkItemInput input, string rootPath)
        {
            return await _service.StartWorkItem(input, rootPath);
        }
    }
}
