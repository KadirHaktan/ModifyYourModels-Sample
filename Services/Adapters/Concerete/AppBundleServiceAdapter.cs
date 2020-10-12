using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Abstract;
using Newtonsoft.Json.Linq;
using Services.Abstract;
using Services.Adapters.Abstract;

namespace Services.Adapters.Concerete
{
    public class AppBundleServiceAdapter: IAppBundleServiceAdapter
    {
        private readonly IAppBundleService _appBundleService;

        public AppBundleServiceAdapter(IAppBundleService appBundleService)
        {
            this._appBundleService = appBundleService;
        }
        public async Task<dynamic> CreateAppBundleAsync(JObject appBundle, string localBundlesFolder)
        {
            return await _appBundleService.CreateAppBundle(appBundle, localBundlesFolder);
        }
    }
}
