using System;
using System.IO;
using System.Threading.Tasks;
using Autodesk.Forge.DesignAutomation.Model;
using Core.Utilities.Configurations;
using Entities.Concerete;
using Newtonsoft.Json.Linq;
using Repository.Abstract;
using Services.Abstract;

namespace Services.Concerete
{
    public class AutoDeskAppBundleService : IAppBundleService<AutoDeskAppBundle>
    {

        private readonly IAutoDeskAppBundleRepository _repository;

        public AutoDeskAppBundleService(IAutoDeskAppBundleRepository repository)
        {
            this._repository = repository;
        }
        
        
        public async Task<dynamic> CreateAppBundle(AutoDeskAppBundle appBundle,string LocalBundlesFolder)
        {
            var appBundlesSpecs = appBundle.appBundleSpecs;

            string zipFileName = appBundlesSpecs["zipFileName"].Value<string>();
            string engineName = appBundlesSpecs["engineName"].Value<string>();

            string appBundleName = zipFileName + "AppBundle";
            string NickName = AppSettings.Get("FORGE_CLIENT_ID");
            string Alias = "dev";

            await CheckOutExistToZipPath(LocalBundlesFolder, zipFileName);


            Page<string> pages = await _repository.GetAppBundles(null);


            dynamic result;

            string qualifiedAppBundleId = string.Format("{0}.{1}+{2}", NickName, appBundleName, Alias);

            if (!pages.Data.Contains(qualifiedAppBundleId))
            {
                result=await CreateNewAppBundle(appBundleName,engineName,Alias,1);
            }

            else
            {
                result=await CreateNewVersion(engineName, appBundleName, Alias);
            }


            return result;


        }


        #region async return a value methods
        private async Task<Alias> CreateNewAppBundle(string appBundleName,string engineName,string Alias,int version)
        {
            AppBundle appBundleSpec = new AppBundle()
            {
                Package = appBundleName,
                Engine = engineName,
                Id = appBundleName,
                Description = string.Format("Description for {0}", appBundleName)
            };

            
            CheckOutToNull(await _repository.CreateAsync(appBundleSpec));

            Alias aliasSpec = new Alias()
            {
                Id = Alias,
                Version = version
            };

            Alias newAlias = await _repository.CreateAppBundleAsync(appBundleName, aliasSpec);

            return newAlias;



        }

        private async Task<Alias> CreateNewVersion(string engineName,string appBundleName,string Alias)
        {
            AppBundle appBundleSpec = new AppBundle()
            {
                Engine = engineName,
                Description = appBundleName
            };

            dynamic newAppVersion = await _repository.CreateAppBundleVersionAsync(appBundleName, appBundleSpec);
            CheckOutToNull(newAppVersion);

            AliasPatch aliasPatch = new AliasPatch()
            {
                Version = newAppVersion.Version
            };

            return await _repository.ModifyAsync(appBundleName, Alias, aliasPatch);

        }

        #endregion


        #region async void methods


        private async Task CheckOutExistToZipPath(string LocalBundlesFolder, string zipFileName)
        {
            string packageZipPath = Path.Combine(LocalBundlesFolder, zipFileName + ".zip");
            if (!File.Exists(packageZipPath)) throw new Exception("Appbundle not found at " + packageZipPath);
        }


        private void CheckOutToNull(dynamic newVersion)
        {
            if (newVersion == null)
            {
                throw new Exception("Cannot create new app");
            }
        }

        #endregion



    }
}
