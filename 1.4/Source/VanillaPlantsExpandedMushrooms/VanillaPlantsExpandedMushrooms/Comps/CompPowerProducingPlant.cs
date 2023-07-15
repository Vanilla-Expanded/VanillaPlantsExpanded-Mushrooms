using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaPlantsExpandedMushrooms
{
    [StaticConstructorOnStartup]
    public class CompPowerProducingPlant : CompPowerPlant
    {
        protected override float DesiredPowerOutput
        {
            get
            {
                return GrowthBasedPowerOutputFactor * FullGrowthPower;
            }
        }

        private float GrowthBasedPowerOutputFactor
        {
            get
            {
                Plant plant = this.parent as Plant;
                if (plant != null)
                {
                    return plant.Growth;
                }
                else return 0;
                
            }
        }

       
        private const float FullGrowthPower = 10f;

    }
}