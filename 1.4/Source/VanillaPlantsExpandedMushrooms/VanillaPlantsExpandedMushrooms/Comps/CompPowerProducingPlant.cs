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
                var building = this.parent as ZapcapBuilding;
                if (building.source != null)
                {
                    return building.source.Growth;
                }
                else return 0;
                
            }
        }

       
        private const float FullGrowthPower = 10f;

    }
}