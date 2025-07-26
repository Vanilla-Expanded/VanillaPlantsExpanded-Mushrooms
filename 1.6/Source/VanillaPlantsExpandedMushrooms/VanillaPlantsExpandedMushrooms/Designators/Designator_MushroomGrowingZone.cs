
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaPlantsExpandedMushrooms
{
    public class Designator_MushroomGrowingZone : Designator_ZoneAdd
    {
        protected override string NewZoneLabel
        {
            get
            {
                return "VPE_MushroomGrowingZone".Translate();
            }
        }

        public Designator_MushroomGrowingZone()
        {

            this.zoneTypeToPlace = typeof(Zone_GrowingMushroom);
            this.defaultLabel = "VPE_MushroomGrowingZone".Translate();
            this.defaultDesc = "VPE_MushroomGrowingZoneDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/Designators/ZoneCreate_Mushrooms", true);
            this.hotKey = KeyBindingDefOf.Misc2;

            this.tutorTag = "ZoneAdd_Growing";
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!base.CanDesignateCell(c).Accepted)
            {
                return false;
            }
            TerrainDef terrainDef = Map.terrainGrid.TerrainAt(c);
            foreach (MushroomGrowZoneTerrainsDef element in DefDatabase<MushroomGrowZoneTerrainsDef>.AllDefs)
            {
                foreach (string allowed in element.allowedTerrains)
                {
                   //Log.Message(terrainDef.defName);
                    if ((terrainDef.defName.Contains(allowed)) && c.Walkable(Map))
                    {
                        return true;

                    }


                }
            }
            return false;
        }



        protected override Zone MakeNewZone()
        {
            PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.GrowingFood, KnowledgeAmount.Total);
            return new Zone_GrowingMushroom(Find.CurrentMap.zoneManager);
        }
    }
}
