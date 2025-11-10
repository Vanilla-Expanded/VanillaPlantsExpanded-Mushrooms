using RimWorld;
using System.Linq;
using System.Collections.Generic;
using Verse;

namespace VanillaPlantsExpandedMushrooms
{
    [StaticConstructorOnStartup]
    public static class Startup
    {
        static Startup()
        {
            

            var beds = DefDatabase<ThingDef>.AllDefsListForReading
                .Where(d => d.IsBed)
                .ToList();

           
            foreach (var bed in beds)
            {
                AddAffectedByFacilityComp(bed);
            }

            
        }

        private static void AddAffectedByFacilityComp(ThingDef def)
        {
            if (def.comps == null)
            {
                def.comps = new List<CompProperties>();
            }

            var existingComp = def.GetCompProperties<CompProperties_AffectedByFacilities>();
            if (existingComp != null)
            {
                existingComp.linkableFacilities.Add(InternalDefOf.VPE_Plant_Lullabloom);
               
            }
            else
            {
                var comp = new CompProperties_AffectedByFacilities
                {
                    linkableFacilities = new List<ThingDef> { InternalDefOf.VPE_Plant_Lullabloom }
                };
                
                def.comps.Add(comp);
                
            }
        }

       
    }
}
