using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Services.Adapters.Abstract
{
    public interface IAppBundleServiceAdapter
    {
        Task<dynamic> CreateAppBundleAsync(JObject appBundle, string localBundlesFolder);
        string[] GetLocalBundles(string localBundlesFolder);
    }
}
