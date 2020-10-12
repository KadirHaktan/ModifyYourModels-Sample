﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Forge.DesignAutomation.Model;
using Core.Interfaces.Repository;

namespace Repository.Abstract
{
    public interface IAutoDeskDesignAutomationRepository:IRepository<AppBundle>
    {


        Task<Alias> ModifyAsync(string appBundleName,string Alias,AliasPatch aliasSpec);

        Task<Alias> CreateAppBundleAsync(string appBundleName, Alias aliasSpec);

        Task<dynamic> CreateAppBundleVersionAsync(string appBundleName, AppBundle appBundleSpec);

        Task<dynamic> CreateActitvityAliasAsync(string Id, Alias aliasSpec);
        Task<dynamic> CreateActitivity(Activity activitySpec);

        Task<Page<string>> GetAppBundles(string page);

        Task<Page<string>> GetAppEngines(string page);

        Task<Page<string>> GetActivities(string page);


    }
}