using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

using System;


namespace VanillaPlantsExpandedMushrooms
{



    [HarmonyPatch(typeof(PlantUtility))]
    [HarmonyPatch("CanSowOnGrower")]
    public static class VanillaPlantsExpandedMushrooms_PlantUtility_CanSowOnGrower_Patch
    {
        [HarmonyPostfix]
        public static void SowTagsOnAquaticPlants(ThingDef plantDef, object obj, ref bool __result)
        {
            if (obj is Zone_GrowingMushroom)
            {
                __result = plantDef.plant.sowTags.Contains("VPE_Mushroom");
            }
           


        }


    }


}











