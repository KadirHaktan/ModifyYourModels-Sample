
using System;
using System.Threading.Tasks;
using Autodesk.Forge.DesignAutomation;
using Autodesk.Forge.DesignAutomation.Model;
using Repository.Abstract;

namespace Repository.Concerete
{
    public class AutoDeskAppBundleRepository:IAutoDeskAppBundleRepository
    {
        private readonly DesignAutomationClient _designAutomation=new DesignAutomationClient();


        public async Task<dynamic> CreateAppBundleVersionAsync(string appBundleName, AppBundle appBundleSpec)
        {
            return await _designAutomation.CreateAppBundleVersionAsync(appBundleName, appBundleSpec);
        }

        public async Task<AppBundle> CreateAsync(AppBundle entity)
        {
            return await _designAutomation.CreateAppBundleAsync(entity);
        }


        public async Task CreateVoidAsync(AppBundle entity)
        {
            await _designAutomation.CreateAppBundleAsync(entity);
        }

        public async Task<Page<string>> GetAppBundles(string? page)
        {
            return await _designAutomation.GetAppBundlesAsync(page);
        }


        public async Task<Alias> ModifyAsync(string appBundleName, string Alias, AliasPatch aliasSpec)
        {
            return await _designAutomation.ModifyAppBundleAliasAsync(appBundleName, Alias, aliasSpec);
        }

        #region these methods have not been used
        public void CreateVoid(AppBundle entity)
        {
            throw new NotImplementedException();
        }

        public AppBundle Create(AppBundle entity)
        {
            throw new NotImplementedException();
        }

        public Task<Alias> CreateAppBundleAsync(string appBundleName, Alias aliasSpec)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
