﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;

using Verse;

namespace VanillaPlantsExpandedMushrooms
{
    public class Plant_IgnoresLight : Plant
    {

        public override float GrowthRate
        {
            get
            {

                if (base.Spawned && !PlantUtility.GrowthSeasonNow(base.Position, base.Map, def))
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


    }
}
