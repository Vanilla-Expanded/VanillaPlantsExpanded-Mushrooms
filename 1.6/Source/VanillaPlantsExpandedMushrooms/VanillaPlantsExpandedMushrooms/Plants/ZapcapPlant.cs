using Verse;
using RimWorld;

namespace VanillaPlantsExpandedMushrooms
{
    public class ZapcapPlant : Plant
    {
        public ZapcapBuilding plantAsBuilding;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            if (!respawningAfterLoad)
            {
                plantAsBuilding = GenSpawn.Spawn(InternalDefOf.VPE_Building_Zapcap, Position, Map) as ZapcapBuilding;
                plantAsBuilding.source = this;
            }
        }

        public override string GetInspectString()
        {
            return plantAsBuilding.GetInspectString() + "\n" + base.GetInspectString();
        }

        public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
        {
            base.Destroy(mode);
            plantAsBuilding?.Destroy();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref plantAsBuilding, "fakeBuilding");
        }
    }
}