using System;
using System.Collections.Generic;
using System.Text;
using Services.Helpers.AutoDeskEngine.Abstract;

namespace Services.Helpers.AutoDeskEngine.Concerete
{
    public class ThreeDsMaxEngineStrategy : IEngineStrategy
    {
        public dynamic GenerateToAttributes()
        {
            return new 
            { 
                commandLine = "$(engine.path)\\3dsmaxbatch.exe -sceneFile \"$(args[inputFile].path)\" $(settings[script].path)",
                extension = "max",
                script = "da = dotNetClass(\"Autodesk.Forge.Sample.DesignAutomation.Max.RuntimeExecute\")\nda.ModifyWindowWidthHeight()\n"
            };
        }
    }
}
