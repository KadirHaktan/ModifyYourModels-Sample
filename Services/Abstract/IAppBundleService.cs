using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Abstract;
using Newtonsoft.Json.Linq;

namespace Services.Abstract
{
    public interface IAppBundleService
    {
        Task<dynamic> CreateAppBundle(JObject appBundle,string LocalBundlesFolder);

    }
}
