using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Abstract;
using Newtonsoft.Json.Linq;

namespace Services.Abstract
{
    public interface IAppBundleService<T> where T:class,IAppBundle
    {
        Task<dynamic> CreateAppBundle(T appBundle,string LocalBundlesFolder);

    }
}
