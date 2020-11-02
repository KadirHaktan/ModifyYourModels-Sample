using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autodesk.Forge.DesignAutomation.Model;
using Core.Utilities.Configurations;
using Newtonsoft.Json.Linq;
using Repository.Abstract;
using Services.Abstract;

namespace Services.Concerete
{
    public class AutoDeskAppBundleService : IAppBundleService
    {

        private readonly IAutoDeskDesignAutomationRepository _repository;

        public string QualifiedAppBundleId = null;

        public AutoDeskAppBundleService(IAutoDeskDesignAutomationRepository repository)
        {
            this._repository = repository;
        }
        
        
        public async Task<dynamic> CreateAppBundle(JObject appBundlesSpecs,string LocalBundlesFolder)
        {
            try
            {
                string zipFileName = appBundlesSpecs["zipFileName"].Value<string>();
                string engineName = appBundlesSpecs["engine"].Value<string>();

                string appBundleName = zipFileName + "AppBundle";
                string NickName = AppSettings.Get("FORGE_CLIENT_ID");
                string Alias = "dev";

                await CheckOutExistToZipPath(LocalBundlesFolder, zipFileName);


                Page<string> pages = await _repository.GetAppBundles();


                dynamic newAppVersion;

                QualifiedAppBundleId = string.Format("{0}.{1}+{2}", NickName, appBundleName, Alias);

                if (!pages.Data.Contains(QualifiedAppBundleId))
                {
                    newAppVersion = await CreateNewAppBundle(appBundleName, engineName, Alias, 1);
                }

                else
                {
                    newAppVersion = await CreateNewVersion(engineName, appBundleName, Alias);
                }


                return new Dictionary<string, dynamic>()
                {
                    {"newAppVersion",newAppVersion},
                    {"packageZipFileName",zipFileName},
                    {"QualifiedAppBundleId",QualifiedAppBundleId}
                };


            }

            catch (Exception e)
            {
                throw e;
            }



        }


        #region async return a value methods
        private async Task<dynamic> CreateNewAppBundle(string appBundleName,string engineName,string Alias,int version)
        {
            AppBundle appBundleSpec = new AppBundle()
            {
                Package = appBundleName,
                Engine = engineName,
                Id = appBundleName,
                Description = string.Format("Description for {0}", appBundleName)
            };

            dynamic newAppVersion = await _repository.CreateAsync(appBundleSpec);



            CheckOutToNull(newAppVersion);

            Alias aliasSpec = new Alias()
            {
                Id = Alias,
                Version = version
            };

            await _repository.CreateAppBundleAsync(appBundleName, aliasSpec);

            return newAppVersion;



        }

        private async Task<dynamic> CreateNewVersion(string engineName,string appBundleName,string Alias)
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
            
            await _repository.ModifyAsync(appBundleName, Alias, aliasPatch);

            return newAppVersion;

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

        public string[] GetLocalBundles(string LocalBundlesFolder)
        {
            string[] result=Directory.GetFiles(LocalBundlesFolder, "*.zip").Select(Path.GetFileNameWithoutExtension).ToArray();
            return result;
        }

        #endregion



    }
}
