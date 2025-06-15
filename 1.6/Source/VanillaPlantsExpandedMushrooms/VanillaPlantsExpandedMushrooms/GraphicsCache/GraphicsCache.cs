using UnityEngine;
using Verse;

namespace VanillaPlantsExpandedMushrooms
{
    [StaticConstructorOnStartup]
    public static class GraphicsCache
    {

        public static Graphic HotMushroom = GraphicDatabase.Get<Graphic_Random>("Things/Plant/HeatingThermoregulus", ShaderDatabase.CutoutPlant, Vector2.one, Color.white);
        public static Graphic ColdMushroom = GraphicDatabase.Get<Graphic_Random>("Things/Plant/CoolingThermoregulus", ShaderDatabase.CutoutPlant, Vector2.one, Color.white);

    }
}