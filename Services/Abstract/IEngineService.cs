using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
   public interface IEngineServices
   {
       Task<List<string>> GetAvailableEnginesToString();
   }
}
