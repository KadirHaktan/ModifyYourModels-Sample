using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Abstract;
using Services.Abstract;
using Services.Adapters.Abstract;

namespace Services.Adapters.Concerete
{
    public class AppBundleServiceAdapter<T> : IAppBundleServiceAdapter<T> where T : class, IAppBundle
    {
        private readonly IAppBundleService<T> _appBundleService;

        public AppBundleServiceAdapter(IAppBundleService<T> appBundleService)
        {
            this._appBundleService = appBundleService;
        }
        public async Task<dynamic> CreateAppBundleAsync(T appBundle, string localBundlesFolder)
        {
            return await _appBundleService.CreateAppBundle(appBundle, localBundlesFolder);
        }
    }
}
