using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;

using Verse;
using Verse.Noise;

namespace VanillaPlantsExpandedMushrooms
{
    public class Plant_NeedsCorpseBile : Plant
    {

        public override float GrowthRate
        {
            get
            {
              
                if (base.Spawned && !PlantUtility.GrowthSeasonNow(base.Position, base.Map, def))
                {
                    return 0f;
                }
                return this.GrowthRateFactor_Fertility * this.GrowthRateFactor_Temperature * this.GrowthRateFactor_Light * this.GrowthRateFactor_CorpseBile;
            }
        }

        public float GrowthRateFactor_CorpseBile
        {
            get
            {
                bool flag = false;
                List<Thing> thingList = this.Position.GetThingList(this.Map);
                foreach(Thing thing in thingList)
                {
                    if(thing.def == ThingDefOf.Filth_CorpseBile)
                    {
                        flag = true;
                    }
                }

                if (flag)
                {
                    return 1f;
                }
                else return 0.05f;
            }
        }
    }
}
