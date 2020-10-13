using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class StartWorkItemInput
    {
        public IFormFile inputFile { get; set; }
        public string data { get; set; }
    }
}
