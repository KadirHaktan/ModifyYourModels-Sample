using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IWorkItemService
    {
        Task<dynamic> StartWorkItem(StartWorkItemInput input,string rootPath);

    } 
}
