using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.Abstract;
using Services.Adapters.Abstract;

namespace Services.Adapters.Concerete
{
    public class EngineServiceAdapter : IEngineServiceAdapter
    {
        private readonly IEngineServices _engineServices;

        public EngineServiceAdapter(IEngineServices engineServices)
        {
            this._engineServices = engineServices;
        }
        public  async Task<List<string>> GetEnginesToStringAdapter()
        {
            return await _engineServices.GetAvailableEnginesToString();
        }
    }
}
