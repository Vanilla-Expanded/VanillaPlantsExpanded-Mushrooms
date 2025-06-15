using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;

using Verse;
using Verse.AI;
using Verse.Noise;

namespace VanillaPlantsExpandedMushrooms
{
    public class Plant_Firefoam : Plant
    {



        public override void TickRare()
        {
            if (this.IsHashIntervalTick(8))
            {

                base.TickLong();
            }

            if (Spawned && HarvestableNow && GenClosest.ClosestThingReachable(Position, Map, ThingRequest.ForDef(ThingDefOf.Fire), PathEndMode.OnCell, TraverseParms.For(TraverseMode.NoPassClosedDoors), 3) != null)
            {
                FirefoamExplosion();
                Destroy();
            }

        }

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

        public override void PostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            base.PostApplyDamage(dinfo, totalDamageDealt);

            if(dinfo.Def.ExternalViolenceFor(this) && dinfo.Def == DamageDefOf.Flame)
            {
                FirefoamExplosion();
            }

        }

        public new float GrowthRateFactor_Light
        {
            get { return 1f; }

        }

        public void FirefoamExplosion()
        {
            GenExplosion.DoExplosion(instigator: this, center: PositionHeld, map: Map, radius: 1.9f, damType: DamageDefOf.Extinguish, 
                damAmount: DamageDefOf.Extinguish.defaultDamage, armorPenetration: DamageDefOf.Extinguish.defaultArmorPenetration, 
                explosionSound: DamageDefOf.Extinguish.soundExplosion, weapon: null, projectile: null, intendedTarget: null, 
                postExplosionSpawnThingDef: ThingDefOf.Filth_FireFoam, postExplosionSpawnChance: 1f, postExplosionSpawnThingCount: 1,  
                direction: null, affectedAngle: null, doVisualEffects: true);
        }

        public override bool HarvestableNow
        {
            get
            {
                
                return Growth > 0.99f;
               
            }
        }







    }
}
