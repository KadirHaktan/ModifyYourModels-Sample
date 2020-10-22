using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Configurations
{
    public class AppSettings
    {
        public static string Get(string settingKey)
        {
            string result = Environment.GetEnvironmentVariable(settingKey).Trim();
            return result;
        }
    }
}
