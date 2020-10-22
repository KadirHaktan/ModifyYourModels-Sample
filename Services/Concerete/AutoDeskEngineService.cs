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
       // private readonly IAuthServiceAdapter _authServiceAdapter;

        private readonly IAutoDeskDesignAutomationRepository _repository;
        private readonly IAuthServiceAdapter _authServiceAdapter;
        public AutoDeskEngineService(IAutoDeskDesignAutomationRepository repository,IAuthServiceAdapter authServiceAdapter)
        {
            this._repository = repository;
            this._authServiceAdapter = authServiceAdapter;
        }
        public async Task<List<string>> GetAvailableEnginesToString()
        {
            try
            {
                dynamic oauth = await  _authServiceAdapter.GetAccessTokenAdapterAsync();
                Page<string> engines = await _repository.GetAppEngines();

                engines.Data.Sort();

                return engines.Data;
            }
            catch (Exception e)
            {
                throw e;
            }
          
        }
    }
}
