
using System;
using System.Threading.Tasks;
using Autodesk.Forge.DesignAutomation;
using Autodesk.Forge.DesignAutomation.Model;
using Repository.Abstract;

namespace Repository.Concerete
{
    public class AutoDeskDesignAutomationRepository:IAutoDeskDesignAutomationRepository
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

        public async Task<Page<string>> GetAppBundles(string page)
        {
            return await _designAutomation.GetAppBundlesAsync(page);
        }

        public async Task<dynamic> CreateActitivity(Activity activitySpec)
        {
            return await _designAutomation.CreateActivityAsync(activitySpec);
        }

        public async Task<Page<string>> GetActivities(string page)
        {
            return await _designAutomation.GetActivitiesAsync(page);
        }

        public async Task<dynamic> CreateActitvityAliasAsync(string Id, Alias aliasSpec)
        {
            return await _designAutomation.CreateActivityAliasAsync(Id, aliasSpec);
        }


        public async Task<Alias> ModifyAsync(string appBundleName, string Alias, AliasPatch aliasSpec)
        {
            return await _designAutomation.ModifyAppBundleAliasAsync(appBundleName, Alias, aliasSpec);
        }

        public async Task<Page<string>> GetAppEngines(string page)
        {
            return await _designAutomation.GetEnginesAsync(page);
        }

        public Task<Alias> CreateAppBundleAsync(string appBundleName, Alias aliasSpec)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkItemStatus> CreateWorkItem(WorkItem workItemSpec)
        {
            return await _designAutomation.CreateWorkItemAsync(workItemSpec);
        }
    }
}
