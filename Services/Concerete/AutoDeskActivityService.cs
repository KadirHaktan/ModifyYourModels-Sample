using Newtonsoft.Json.Linq;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Forge.DesignAutomation.Model;
using Core.Utilities.Configurations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Abstract;
using Services.Helpers.AutoDeskEngine;

namespace Services.Concerete
{
    public class AutoDeskActivityService : IActivityService
    {
        private readonly IAutoDeskDesignAutomationRepository _repository;

        string NickName = AppSettings.Get("FORGE_CLIENT_ID");
        public AutoDeskActivityService(IAutoDeskDesignAutomationRepository repository)
        {
            this._repository = repository;
        }


        public async Task<dynamic> CreateActivity(JObject activitySpecs)
        {
            string zipFileName = activitySpecs["zipFileName"].Value<string>();
            string engineName = activitySpecs["engine"].Value<string>();

            string appBundleName = zipFileName + "AppBundle";
            string activityName = zipFileName + "Activity";

            string Alias = "dev";

            Page<string> activities = await _repository.GetActivities(null);

            string qualifiedActivityId = string.Format("{0}.{1}+{2}", NickName, activityName, Alias);

            if (!activities.Data.Contains(qualifiedActivityId))
            {
                return await ActivityNotExist(engineName, appBundleName, activityName, NickName, Alias, qualifiedActivityId);
            }

            else
            {
                return new
                {
                    Activity = "Activity already defined"
                };
            }
        }

        public async Task<List<string>> GetDefinedActivities()
        {

            Page<string> activities = await _repository.GetActivities(null);
            List<string> definedActivities = new List<string>();
            foreach (string activity in activities.Data)
                if (activity.StartsWith(NickName) && activity.IndexOf("$LATEST") == -1)
                    definedActivities.Add(activity.Replace(NickName + ".", String.Empty));

            return definedActivities;
        }

        private async Task<dynamic> ActivityNotExist(string engineName, string appBundleName, string activityName, string NickName, string Alias,
            string qualifiedActivityId)
        {
            dynamic engineAttributes = EngineStrategyFactory.GetEngineStrategy(engineName).GenerateToAttributes();
            string commandLine = String.Format(engineAttributes.commandLine, appBundleName);
            Activity activitySpec = new Activity()
            {
                Id = activityName,
                Appbundles = new List<string>() {string.Format("{0}.{1}+{2}", NickName, appBundleName, Alias)},
                CommandLine = new List<string>() {commandLine},
                Engine = engineName,
                Parameters = new Dictionary<string, Parameter>()
                {
                    {
                        "inputFile",
                        new Parameter()
                        {
                            Description = "input file", LocalName = "$(inputFile)", Ondemand = false, Required = true,
                            Verb = Verb.Get, Zip = false
                        }
                    },
                    {
                        "inputJson",
                        new Parameter()
                        {
                            Description = "input json", LocalName = "params.json", Ondemand = false, Required = false,
                            Verb = Verb.Get, Zip = false
                        }
                    },
                    {
                        "outputFile",
                        new Parameter()
                        {
                            Description = "output file", LocalName = "outputFile." + engineAttributes.extension,
                            Ondemand = false, Required = true, Verb = Verb.Put, Zip = false
                        }
                    }
                },
                Settings = new Dictionary<string, ISetting>()
                {
                    {"script", new StringSetting() {Value = engineAttributes.script}}
                }
            };
            await _repository.CreateActitivity(activitySpec);
            Alias aliasSpec = new Alias() {Id = Alias, Version = 1};
           await _repository.CreateActitvityAliasAsync(activityName, aliasSpec);

            return new {Activity = qualifiedActivityId};
        }
    }
}
