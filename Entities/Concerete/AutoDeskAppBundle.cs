using System;
using System.Collections.Generic;
using System.Text;
using Entities.Abstract;
using Newtonsoft.Json.Linq;

namespace Entities.Concerete
{
    public class AutoDeskAppBundle : IAppBundle
    {
        public JObject appBundleSpecs { get; set; }
    }
}
