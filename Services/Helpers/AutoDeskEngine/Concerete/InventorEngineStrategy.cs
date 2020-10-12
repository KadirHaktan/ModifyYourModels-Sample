using System;
using System.Collections.Generic;
using System.Text;
using Services.Helpers.AutoDeskEngine.Abstract;

namespace Services.Helpers.AutoDeskEngine.Concerete
{
    public class InventorEngineStrategy : IEngineStrategy
    {
        public dynamic GenerateToAttributes()
        {
            return new
            {
                commandLine = "$(engine.path)\\inventorcoreconsole.exe /i \"$(args[inputFile].path)\" /al \"$(appbundles[{0}].path)\"",
                extension = "ipt",
                script = string.Empty
            };
        }
    }
}
