using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Forge.DesignAutomation.Model;
using Core.Interfaces.Adapters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Abstract;
using Services.Abstract;

namespace Services.Concerete
{
    public class AutoDeskEngineService : IEngineServices
    {
        private readonly IAuthServiceAdapter _authServiceAdapter;

        private readonly IAutoDeskAppBundleRepository _repository;
        public AutoDeskEngineService(IAuthServiceAdapter authServiceAdapter,IAutoDeskAppBundleRepository repository)
        {
            this._authServiceAdapter = authServiceAdapter;
            this._repository = repository;
        }
        public async Task<List<string>> GetAvailableEnginesToString()
        {
            dynamic token = await _authServiceAdapter.GetAccessTokenAdapterAsync();

            Page<string> engines = await _repository.GetAppEngines(null);

            engines.Data.Sort();

            return engines.Data;
        }
    }
}
