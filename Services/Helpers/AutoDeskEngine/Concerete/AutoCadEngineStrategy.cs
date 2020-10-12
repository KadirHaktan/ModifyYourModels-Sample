using System;
using System.Collections.Generic;
using System.Text;
using Services.Helpers.AutoDeskEngine.Abstract;

namespace Services.Helpers.AutoDeskEngine.Concerete
{
    public class AutoCadEngineStrategy : IEngineStrategy
    {
        public dynamic GenerateToAttributes()
        {
            return new 
            {
                commandLine = "$(engine.path)\\accoreconsole.exe /i \"$(args[inputFile].path)\" /al \"$(appbundles[{0}].path)\" /s $(settings[script].path)",
                extension = "dwg",
                script = "UpdateParam\n"

            };
        }
    }
}
