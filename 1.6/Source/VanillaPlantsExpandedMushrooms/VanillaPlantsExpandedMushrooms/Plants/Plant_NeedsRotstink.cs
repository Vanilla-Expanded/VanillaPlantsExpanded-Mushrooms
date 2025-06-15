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
    public class Plant_NeedsRotstink : Plant
    {

        public override float GrowthRate
        {
            get
            {
              
                if (base.Spawned && !PlantUtility.GrowthSeasonNow(base.Position, base.Map, def))
                {
                    return 0f;
                }
                return this.GrowthRateFactor_Fertility * this.GrowthRateFactor_Temperature * this.GrowthRateFactor_Light * this.GrowthRateFactor_RotStink;
            }
        }

        public float GrowthRateFactor_RotStink
        {
            get
            {
                if (this.Position.AnyGas(this.Map,GasType.RotStink))
                {
                    return 1f;
                }
                else return 0.05f;
            }
        }
    }
}
