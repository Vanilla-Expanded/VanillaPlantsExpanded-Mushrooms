using Verse;
using HarmonyLib;
using RimWorld;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace VanillaPlantsExpandedMushrooms
{
    [HarmonyPatch(typeof(ShortCircuitUtility), "DrainBatteriesAndCauseExplosion")]
    public static class ShortCircuitUtility_DrainBatteriesAndCauseExplosion_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            bool patched = false;
            foreach (var instruction in codeInstructions)
            {
                yield return instruction;
                if (!patched && instruction.opcode == OpCodes.Ldc_R4 && instruction.OperandIs(0.0))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ShortCircuitUtility_DrainBatteriesAndCauseExplosion_Patch), 
                        "AddPowerFromZapcanPlants"));
                    yield return new CodeInstruction(OpCodes.Add);
                    patched = true;
                }
            }
        }

        public static float AddPowerFromZapcanPlants(PowerNet net)
        {
            return net.Map.listerThings.ThingsOfDef(InternalDefOf.VPE_Plant_Zapcap).Count() * 60;
        }
    }
}