using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.Adapters.Abstract
{
    public interface IWorkItemServiceAdapter
    {
        Task<dynamic> StartWorkItem(StartWorkItemInput input, string rootPath);

    }
}
