using RimWorld;
using Verse;

namespace VanillaPlantsExpandedMushrooms
{
    public class ZapcapBuilding : Building
    {
        public ZapcapPlant source;

        public override void Tick()
        {
            base.Tick();
            if (this.IsHashIntervalTick(300))
            {
                FleckMaker.Static(this.Position, this.Map, InternalDefOf.BlastEMP);
            }
            if (this.source.DestroyedOrNull())
            {
                this.Destroy();
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref source, "source");
        }
    }
}