using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Forge.DesignAutomation.Model;
using Core.Interfaces.Repository;

namespace Repository.Abstract
{
    public interface IAutoDeskDesignAutomationRepository:IRepository
    {

        Task<AppBundle> CreateAsync(AppBundle entity);

        Task CreateVoidAsync(AppBundle entity);
        Task<Alias> ModifyAsync(string appBundleName,string Alias,AliasPatch aliasSpec);

        Task<Alias> CreateAppBundleAsync(string appBundleName, Alias aliasSpec);

        Task<dynamic> CreateAppBundleVersionAsync(string appBundleName, AppBundle appBundleSpec);

        Task<dynamic> CreateActitvityAliasAsync(string Id, Alias aliasSpec);
        Task<dynamic> CreateActitivity(Activity activitySpec);

        Task<WorkItemStatus> CreateWorkItem(WorkItem workItemSpec);

        Task<Page<string>> GetAppBundles();

        Task<Page<string>> GetAppEngines();

        Task<Page<string>> GetActivities();

        Task DeleteForgeApp(string id);


    }
}
