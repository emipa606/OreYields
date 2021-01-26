using RimWorld;
using RimWorld.Planet;
using System;
using System.Linq;
using UnityEngine;
using Verse;

namespace OreYields
{
    [StaticConstructorOnStartup]
    public static class Startup
    {
        static Startup()
        {
            OreYieldsSettings.yieldSilver = DefDatabase<ThingDef>.GetNamed("MineableSilver").building.mineableYield;
            OreYieldsSettings.yieldGold = DefDatabase<ThingDef>.GetNamed("MineableGold").building.mineableYield;
            OreYieldsSettings.yieldSteel = DefDatabase<ThingDef>.GetNamed("MineableSteel").building.mineableYield;
            OreYieldsSettings.yieldPlasteel = DefDatabase<ThingDef>.GetNamed("MineablePlasteel").building.mineableYield;
            OreYieldsSettings.yieldUranium = DefDatabase<ThingDef>.GetNamed("MineableUranium").building.mineableYield;
            OreYieldsSettings.yieldJade = DefDatabase<ThingDef>.GetNamed("MineableJade").building.mineableYield;
            OreYieldsSettings.yieldComps = DefDatabase<ThingDef>.GetNamed("MineableComponentsIndustrial").building.mineableYield;

            OreYieldsSettings.massSilver = DefDatabase<ThingDef>.GetNamed("Silver").statBases.Where((StatModifier statBase) => statBase.stat == StatDefOf.Mass).First().value;
            OreYieldsSettings.massGold = DefDatabase<ThingDef>.GetNamed("Gold").statBases.Where((StatModifier statBase) => statBase.stat == StatDefOf.Mass).First().value;
            OreYieldsSettings.massSteel = DefDatabase<ThingDef>.GetNamed("Steel").statBases.Where((StatModifier statBase) => statBase.stat == StatDefOf.Mass).First().value;
            OreYieldsSettings.massPlasteel = DefDatabase<ThingDef>.GetNamed("Plasteel").statBases.Where((StatModifier statBase) => statBase.stat == StatDefOf.Mass).First().value;
            OreYieldsSettings.massUranium = DefDatabase<ThingDef>.GetNamed("Uranium").statBases.Where((StatModifier statBase) => statBase.stat == StatDefOf.Mass).First().value;
            OreYieldsSettings.massJade = DefDatabase<ThingDef>.GetNamed("Jade").statBases.Where((StatModifier statBase) => statBase.stat == StatDefOf.Mass).First().value;
            OreYieldsSettings.massComps = DefDatabase<ThingDef>.GetNamed("ComponentIndustrial").statBases.Where((StatModifier statBase) => statBase.stat == StatDefOf.Mass).First().value;

            DefDatabase<ThingDef>.GetNamed("MineableSilver").building.mineableYield = (int)Math.Floor(OreYieldsSettings.yieldSilver * OreYieldsSettings.multiplySilverYield);
            DefDatabase<ThingDef>.GetNamed("MineableGold").building.mineableYield = (int)Math.Floor(OreYieldsSettings.yieldGold * OreYieldsSettings.multiplyGoldYield);
            DefDatabase<ThingDef>.GetNamed("MineableSteel").building.mineableYield = (int)Math.Floor(OreYieldsSettings.yieldSteel * OreYieldsSettings.multiplySteelYield);
            DefDatabase<ThingDef>.GetNamed("MineablePlasteel").building.mineableYield = (int)Math.Floor(OreYieldsSettings.yieldPlasteel * OreYieldsSettings.multiplyPlasteelYield);
            DefDatabase<ThingDef>.GetNamed("MineableUranium").building.mineableYield = (int)Math.Floor(OreYieldsSettings.yieldUranium * OreYieldsSettings.multiplyUraniumYield);
            DefDatabase<ThingDef>.GetNamed("MineableJade").building.mineableYield = (int)Math.Floor(OreYieldsSettings.yieldJade * OreYieldsSettings.multiplyJadeYield);
            DefDatabase<ThingDef>.GetNamed("MineableComponentsIndustrial").building.mineableYield = (int)Math.Floor(OreYieldsSettings.yieldComps * OreYieldsSettings.multiplyCompsYield);
        }
    }
}
