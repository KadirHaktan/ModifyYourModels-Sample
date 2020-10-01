using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Adapters.Abstract
{
    public interface IEngineServiceAdapter
    {
        Task<List<string>> GetEnginesToStringAdapter();
    }
}
