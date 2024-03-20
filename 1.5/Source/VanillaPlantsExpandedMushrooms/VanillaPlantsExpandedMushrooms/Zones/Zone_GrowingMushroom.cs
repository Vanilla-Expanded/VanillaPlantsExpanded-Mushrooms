
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Verse;
using RimWorld;


namespace VanillaPlantsExpandedMushrooms
{
    public class Zone_GrowingMushroom : Zone, IPlantToGrowSettable
    {
        private ThingDef plantDefToGrow = InternalDefOf.VPE_Plant_Timbercap;

        public bool allowSow = true;

        public bool allowCut = true;

        public override bool IsMultiselectable => true;

        private static List<Color> mushroomZoneColors = new List<Color>();

        private static int nextMushroomZoneColorIndex = 0;

        IEnumerable<IntVec3> IPlantToGrowSettable.Cells
        {
            get
            {
                return base.Cells;
            }
        }

        protected override Color NextZoneColor
        {
            get
            {

                return NextMushroomZoneColor();


            }
        }

        private static IEnumerable<Color> MushroomZoneColors()
        {
            yield return Color.Lerp(new Color(0.5f, 0.5f, 0.5f), Color.gray, 0.5f);
            yield return Color.Lerp(new Color(0.4f, 0.4f, 0.4f), Color.gray, 0.5f);
            yield return Color.Lerp(new Color(0.25f, 0.25f, 0.25f), Color.gray, 0.5f);
            yield return Color.Lerp(new Color(0.15f, 0.15f, 0.15f), Color.gray, 0.5f);
           
            yield break;
        }

        public static Color NextMushroomZoneColor()
        {
            mushroomZoneColors.Clear();
            foreach (Color color in MushroomZoneColors())
            {
                Color item = new Color(color.r, color.g, color.b, 0.09f);
                mushroomZoneColors.Add(item);

            }
            Color result = mushroomZoneColors[nextMushroomZoneColorIndex];
            nextMushroomZoneColorIndex++;
            if (nextMushroomZoneColorIndex >= mushroomZoneColors.Count)
            {
                nextMushroomZoneColorIndex = 0;
            }
            return result;
        }

        public Zone_GrowingMushroom()
        {


        }

        public Zone_GrowingMushroom(ZoneManager zoneManager) : base("VPE_MushroomGrowingZone".Translate(), zoneManager)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Defs.Look<ThingDef>(ref this.plantDefToGrow, "plantDefToGrow");
            Scribe_Values.Look<bool>(ref this.allowSow, "allowSow", true, false);
            Scribe_Values.Look(ref allowCut, "allowCut", defaultValue: true);
        }

        public override string GetInspectString()
        {
            string text = string.Empty;
            if (!base.Cells.NullOrEmpty<IntVec3>())
            {
                IntVec3 c = base.Cells.First<IntVec3>();
                if (c.UsesOutdoorTemperature(base.Map))
                {
                    string text2 = text;
                    text = string.Concat(new string[]
                    {
                        text2,
                        "OutdoorGrowingPeriod".Translate(),
                        ": ",
                        Zone_Growing.GrowingQuadrumsDescription(base.Map.Tile),
                        "\n"
                    });
                }
                if (PlantUtility.GrowthSeasonNow(c, base.Map, true))
                {
                    text += "GrowSeasonHereNow".Translate();
                }
                else
                {
                    text += "CannotGrowBadSeasonTemperature".Translate();
                }
            }
            return text;
        }

        public static string GrowingQuadrumsDescription(int tile)
        {
            List<Twelfth> list = GenTemperature.TwelfthsInAverageTemperatureRange(tile, 10f, 42f);
            if (list.NullOrEmpty<Twelfth>())
            {
                return "NoGrowingPeriod".Translate();
            }
            if (list.Count == 12)
            {
                return "GrowYearRound".Translate();
            }
            return "PeriodDays".Translate(list.Count * 5 + "/" + 60) + " (" + QuadrumUtility.QuadrumsRangeLabel(list) + ")";
        }

        public override void AddCell(IntVec3 c)
        {
            base.AddCell(c);
            foreach (Thing item in base.Map.thingGrid.ThingsListAt(c))
            {
                Designator_PlantsHarvestWood.PossiblyWarnPlayerImportantPlantDesignateCut(item);
            }
        }

        [DebuggerHidden]
        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo g in base.GetGizmos())
            {
                yield return g;
            }

            yield return PlantToGrowSettableUtility.SetPlantToGrowCommand(this);
            yield return new Command_Toggle
            {
                defaultLabel = "CommandAllowSow".Translate(),
                defaultDesc = "CommandAllowSowDesc".Translate(),
                hotKey = KeyBindingDefOf.Command_ItemForbid,
                icon = TexCommand.ForbidOff,
                isActive = (() => this.allowSow),
                toggleAction = delegate
                {
                    this.allowSow = !this.allowSow;
                }
            };
            Command_Toggle command_Toggle2 = new Command_Toggle();
            command_Toggle2.defaultLabel = "CommandAllowCut".Translate();
            command_Toggle2.defaultDesc = "CommandAllowCutDesc".Translate();
            command_Toggle2.icon = Designator_PlantsCut.IconTex;
            command_Toggle2.isActive = (() => allowCut);
            command_Toggle2.toggleAction = delegate
            {
                allowCut = !allowCut;
            };
            yield return command_Toggle2;


        }

        [DebuggerHidden]
        public override IEnumerable<Gizmo> GetZoneAddGizmos()
        {
            yield return DesignatorUtility.FindAllowedDesignator<Designator_MushroomGrowingZone_Expand>();
        }

        public ThingDef GetPlantDefToGrow()
        {
            return plantDefToGrow;

        }

        public void SetPlantDefToGrow(ThingDef plantDef)
        {
            this.plantDefToGrow = plantDef;
        }

        public bool CanAcceptSowNow()
        {
            return true;
        }
    }
}

