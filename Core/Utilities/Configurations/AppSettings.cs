using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Configurations
{
    public static class AppSettings
    {
        public static string Get(string settingKey)
        {
            return Environment.GetEnvironmentVariable(settingKey);
        }
    }
}
