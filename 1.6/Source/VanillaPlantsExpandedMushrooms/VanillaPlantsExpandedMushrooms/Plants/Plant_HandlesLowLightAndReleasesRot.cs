using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;

using Verse;

namespace VanillaPlantsExpandedMushrooms
{
    public class Plant_HandlesLowLightAndReleasesRot : Plant
    {

        public override bool DyingBecauseExposedToLight
        {
            get
            {
                if (base.Spawned)
                {
                    return base.Map.glowGrid.GroundGlowAt(base.Position, ignoreCavePlants: true) > 0.5f;
                }
                return false;
            }
        }

        protected override void TickInterval(int delta)
        {
            if (this.IsHashIntervalTick(250,delta))
            {

                GasUtility.AddGas(this.PositionHeld, this.MapHeld, GasType.RotStink, GasProduced());
            }
            if (this.IsHashIntervalTick(2000,delta))
            {

                base.TickLong();
            }

        }

        public int GasProduced()
        {

            return (int)Mathf.Lerp(0, 75, this.growthInt);


        }
    }
}
