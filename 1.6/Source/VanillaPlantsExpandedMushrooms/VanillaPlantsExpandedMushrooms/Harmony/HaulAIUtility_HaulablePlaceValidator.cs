using HarmonyLib;
using RimWorld;
using Verse.AI;
using Verse;



namespace VanillaPlantsExpandedMushrooms
{



    [HarmonyPatch(typeof(HaulAIUtility))]
    [HarmonyPatch("HaulablePlaceValidator")]
    public static class VanillaPlantsExpandedMushrooms_HaulAIUtility_HaulablePlaceValidator_Patch
    {
        [HarmonyPostfix]
        public static void MakeZonesNotHaulable(Thing haulable, Pawn worker, IntVec3 c, ref bool __result)
        {
            if (haulable != null && haulable.def.BlocksPlanting() && (worker.Map.zoneManager.ZoneAt(c) is Zone_GrowingMushroom))
            {
                __result = false;
            }

        }


    }


}











