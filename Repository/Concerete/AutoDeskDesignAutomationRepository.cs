
using System;
using System.Threading.Tasks;
using Autodesk.Forge.DesignAutomation;
using Autodesk.Forge.DesignAutomation.Model;
using Repository.Abstract;

namespace Repository.Concerete
{
    public class AutoDeskDesignAutomationRepository:IAutoDeskDesignAutomationRepository
    {
        private readonly DesignAutomationClient _designAutomation;

        public AutoDeskDesignAutomationRepository(DesignAutomationClient designAutomation)
        {
            this._designAutomation = designAutomation;
        }


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

        public async Task<Page<string>> GetAppBundles()
        {
            var result= await _designAutomation.GetAppBundlesAsync();
            return result;
        }

        public async Task<dynamic> CreateActitivity(Activity activitySpec)
        {
            return await _designAutomation.CreateActivityAsync(activitySpec);
        }

        public async Task<Page<string>> GetActivities()
        {
            var result= await _designAutomation.GetActivitiesAsync();
            return result;
        }

        public async Task<dynamic> CreateActitvityAliasAsync(string Id, Alias aliasSpec)
        {
            return await _designAutomation.CreateActivityAliasAsync(Id, aliasSpec);
        }


        public async Task<Alias> ModifyAsync(string appBundleName, string Alias, AliasPatch aliasSpec)
        {
            return await _designAutomation.ModifyAppBundleAliasAsync(appBundleName, Alias, aliasSpec);
        }

        public async Task<Page<string>> GetAppEngines()
        {
            var result = await _designAutomation.GetEnginesAsync();
            return result;
        }

        public async Task<Alias> CreateAppBundleAsync(string appBundleName, Alias aliasSpec)
        {
            var result = await _designAutomation.CreateAppBundleAliasAsync(appBundleName, aliasSpec);
            return result;
        }

        public async Task<WorkItemStatus> CreateWorkItem(WorkItem workItemSpec)
        {
            return await _designAutomation.CreateWorkItemAsync(workItemSpec);
        }

        public async Task DeleteForgeApp(string id)
        { 
            await _designAutomation.DeleteForgeAppAsync(id);
        }
    }
}
