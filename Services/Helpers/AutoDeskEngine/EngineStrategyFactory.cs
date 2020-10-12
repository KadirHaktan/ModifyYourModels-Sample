using System;
using System.Collections.Generic;
using Services.Helpers.AutoDeskEngine.Abstract;
using Services.Helpers.AutoDeskEngine.Concerete;

namespace Services.Helpers.AutoDeskEngine
{
    public static class EngineStrategyFactory
    {
        public static Dictionary<string, IEngineStrategy> engineStrategyDict = new Dictionary<string, IEngineStrategy>();

        private static void FillToDict()
        {
            engineStrategyDict.Add("Inventor",new InventorEngineStrategy());
            engineStrategyDict.Add("3dsMax",new ThreeDsMaxEngineStrategy());
            engineStrategyDict.Add("AutoCAD", new AutoCadEngineStrategy());
            engineStrategyDict.Add("Revit", new RevitEngineStrategy());
        }

        public static IEngineStrategy GetEngineStrategy(string engine)
        {
            FillToDict();

            if (!engineStrategyDict.ContainsKey(engine))
            {
                throw new Exception("Invalid engine");
            }

            return engineStrategyDict[engine];

            
        }


    }
}
