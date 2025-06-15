using System;
using Verse;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace VanillaPlantsExpandedMushrooms
{
    [HarmonyPatch(typeof(MouseoverReadout), "MouseoverReadoutOnGUI")]
    public static class MouseoverReadout_MouseoverReadoutOnGUI_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var getThingListInfo = AccessTools.Method(typeof(GridsUtility), "GetThingList", new Type[] { typeof(IntVec3), typeof(Map) });
            foreach (var code in codeInstructions)
            {
                yield return code;
                if (code.Calls(getThingListInfo))
                {
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MouseoverReadout_MouseoverReadoutOnGUI_Patch), "FilterZapCapBuildings"));
                }
            }
        }

        public static List<Thing> FilterZapCapBuildings(List<Thing> things)
        {
            return things.Where(x => !(x is ZapcapBuilding)).ToList();
        }
    }
}