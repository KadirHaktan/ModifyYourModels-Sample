using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Forge.DesignAutomation.Model;
using Core.Interfaces.Repository;

namespace Repository.Abstract
{
    public interface IAutoDeskAppBundleRepository:IRepository<AppBundle>
    {


        Task<Alias> ModifyAsync(string appBundleName,string Alias,AliasPatch aliasSpec);

        Task<Alias> CreateAppBundleAsync(string appBundleName, Alias aliasSpec);

        Task<dynamic> CreateAppBundleVersionAsync(string appBundleName, AppBundle appBundleSpec);

        Task<Page<string>> GetAppBundles(string? page);


    }
}
