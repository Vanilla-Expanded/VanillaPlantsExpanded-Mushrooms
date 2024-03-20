using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using HarmonyLib;


namespace VanillaPlantsExpandedMushrooms
{
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("CheckForDisturbedSleep")]
    public static class VanillaPlantsExpandedMushrooms_Pawn_CheckForDisturbedSleep_Patch
    {
       

        [HarmonyPrefix]
        public static bool NullifyDisturbedSleepThought(Pawn __instance)

        {
            List<Thing> listFacilities = __instance.CurrentBed()?.TryGetComp<CompAffectedByFacilities>()?.LinkedFacilitiesListForReading;
            if(listFacilities != null)
            {
                foreach (Thing facility in listFacilities)
                {
                    if(facility.def == InternalDefOf.VPE_Plant_Lullabloom)
                    {
                        return false;
                    }
                }
            }
            
            return true;


        }
    }
}
