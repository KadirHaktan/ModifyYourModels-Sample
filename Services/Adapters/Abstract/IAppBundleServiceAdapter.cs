using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Abstract;
using Newtonsoft.Json.Linq;

namespace Services.Adapters.Abstract
{
    public interface IAppBundleServiceAdapter
    {
        Task<dynamic> CreateAppBundleAsync(JObject appBundle, string localBundlesFolder);
    }
}
