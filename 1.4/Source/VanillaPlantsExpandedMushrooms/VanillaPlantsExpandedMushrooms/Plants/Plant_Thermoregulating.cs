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
    public class Plant_Thermoregulating : Plant
    {
        public bool isCold = true;


        public override float GrowthRate
        {
            get
            {

                if (base.Spawned && !PlantUtility.GrowthSeasonNow(base.Position, base.Map, false))
                {
                    return 0f;
                }
                return this.GrowthRateFactor_Fertility * this.GrowthRateFactor_Temperature;
            }
        }

        public new float GrowthRateFactor_Light
        {
            get { return 1f; }

        }

        public override void Tick()
        {
            if (this.IsHashIntervalTick(60))
            {
                if (this.Growth>0.99) {
                    if (this.AmbientTemperature > 16)
                    {
                        isCold = true;
                        GenTemperature.PushHeat(this.PositionHeld, this.MapHeld, -11);
                    }
                    else
                    {
                        isCold = false;
                        GenTemperature.PushHeat(this.PositionHeld, this.MapHeld, 11);
                    }
                }
                


            }
            if (this.IsHashIntervalTick(2000))
            {

                base.TickLong();
            }

        }

        public override Graphic Graphic
        {
            get
            {
                if (this.Growth < 0.99)
                {
                    return base.Graphic;

                }
                else {

                    if (isCold)
                    {
                        return GraphicsCache.ColdMushroom;
                    }
                    else
                    {
                        return GraphicsCache.HotMushroom;
                    }
                }
                
                    
                
            }
        }



    }
}
