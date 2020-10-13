using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Entities.Concerete
{
    public class StartupWorkItemInput
    {
        public IFormFile inputFile { get; set; }
        public string data { get; set; }
    }
}
