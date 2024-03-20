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
    public class Plant_ReleasesGas : Plant
    {

       

        public override void Tick()
        {
            if (this.IsHashIntervalTick(10))
            {
                List<Thing> thingList = this.Position.GetThingList(this.Map);
                foreach (Thing thing in thingList)
                {

                    Pawn pawn = thing as Pawn;
                    if (pawn != null)
                    {
                        GasUtility.AddGas(this.PositionHeld, this.MapHeld, GasType.ToxGas, GasProduced());
                    }
                }


            }
            if (this.IsHashIntervalTick(2000))
            {

                base.TickLong();
            }

        }

        public int GasProduced()
        {
            return (int)Mathf.Lerp(0, 12, this.growthInt);

        }


    }
}
