using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Services.Abstract;
using Services.Adapters.Abstract;

namespace Services.Adapters.Concerete
{
    public class ActivityServiceAdapter : IActivityServiceAdapter
    {
        private readonly IActivityService _service;

        public ActivityServiceAdapter(IActivityService service)
        {
            this._service = service;
        }
        public async Task<dynamic> CreateActivity(JObject activitySpecs)
        {
            return await _service.CreateActivity(activitySpecs);
        }

        public Task<List<string>> GetDefinedActivities()
        {
            return _service.GetDefinedActivities();
        }
    }
}
