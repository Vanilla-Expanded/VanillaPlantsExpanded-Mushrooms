using RimWorld;
using Verse;

namespace VanillaPlantsExpandedMushrooms
{
    public class ZapcapBuilding : Building
    {
        public ZapcapPlant source;

        protected override void TickInterval(int delta)
        {
            base.TickInterval(delta);
            if (this.IsHashIntervalTick(300,delta))
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