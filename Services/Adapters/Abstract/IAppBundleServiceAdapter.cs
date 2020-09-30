using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Abstract;

namespace Services.Adapters.Abstract
{
    public interface IAppBundleServiceAdapter<T> where T:class,IAppBundle
    {
        Task<dynamic> CreateAppBundleAsync(T appBundle, string localBundlesFolder);
    }
}
