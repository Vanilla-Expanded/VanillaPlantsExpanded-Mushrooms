using Verse;
using HarmonyLib;
using RimWorld;
using System.Text;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace VanillaPlantsExpandedMushrooms
{
    [HarmonyPatch(typeof(ShortCircuitUtility), "DoShortCircuit")]
    public static class ShortCircuitUtility_DoShortCircuit_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions, ILGenerator generator)
        {
            bool patched = false;
            var codes = codeInstructions.ToList();
            for (var i = 0; i < codes.Count; i++)
            {
                var instruction = codes[i];
                if (!patched && instruction.opcode == OpCodes.Brfalse_S)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(ShortCircuitUtility_DoShortCircuit_Patch), "ZapcanPlantsPresent"));
                    patched = true;
                }
                yield return instruction;
                if (i > 2 && codes[i].opcode == OpCodes.Ldloc_2 && codes[i + 2].opcode == OpCodes.Ble_Un_S)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(ShortCircuitUtility_DoShortCircuit_Patch), "TrueTotalEnergy"));
                }
            }
        }

        public static float TrueTotalEnergy(float totalEnergy, Building culprit)
        {
            if (culprit.PowerComp.PowerNet.batteryComps.Any() is false)
            {
                return 0;
            }
            return totalEnergy;
        }

        public static bool ZapcanPlantsPresent(bool result, Building culprit)
        {
            if (!result)
            {
                return culprit.Map.listerThings.ThingsOfDef(InternalDefOf.VPE_Plant_Zapcap).Any();
            }
            return result;
        }
    }
}