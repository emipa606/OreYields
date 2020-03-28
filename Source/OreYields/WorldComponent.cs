using RimWorld.Planet;
using System;
using Verse;

namespace OreYields
{
  internal class OreYieldsWorldComp : WorldComponent
  {
    public OreYieldsWorldComp(World world) : base(world)
    {}

    public override void FinalizeInit()
    {
      base.FinalizeInit();

      if (!(OreYieldsSettings.OrigAmtSilver > 0) && !(OreYieldsSettings.OrigAmtGold > 0) && !(OreYieldsSettings.OrigAmtSteel > 0) && 
            !(OreYieldsSettings.OrigAmtPlasteel > 0) && !(OreYieldsSettings.OrigAmtUranium > 0) && !(OreYieldsSettings.OrigAmtJade > 0) && 
            !(OreYieldsSettings.OrigAmtComps > 0))
      {
        OreYieldsSettings.OrigAmtSilver = DefDatabase<ThingDef>.GetNamed("MineableSilver").building.mineableYield;
        OreYieldsSettings.OrigAmtGold = DefDatabase<ThingDef>.GetNamed("MineableGold").building.mineableYield;
        OreYieldsSettings.OrigAmtSteel = DefDatabase<ThingDef>.GetNamed("MineableSteel").building.mineableYield;
        OreYieldsSettings.OrigAmtPlasteel = DefDatabase<ThingDef>.GetNamed("MineablePlasteel").building.mineableYield;
        OreYieldsSettings.OrigAmtUranium = DefDatabase<ThingDef>.GetNamed("MineableUranium").building.mineableYield;
        OreYieldsSettings.OrigAmtJade = DefDatabase<ThingDef>.GetNamed("MineableJade").building.mineableYield;
        OreYieldsSettings.OrigAmtComps = DefDatabase<ThingDef>.GetNamed("MineableComponentsIndustrial").building.mineableYield;

        DefDatabase<ThingDef>.GetNamed("MineableSilver").building.mineableYield = (int)Math.Floor(OreYieldsSettings.OrigAmtSilver * OreYieldsSettings.multiplySilver);
        DefDatabase<ThingDef>.GetNamed("MineableGold").building.mineableYield = (int)Math.Floor(OreYieldsSettings.OrigAmtGold * OreYieldsSettings.multiplyGold);
        DefDatabase<ThingDef>.GetNamed("MineableSteel").building.mineableYield = (int)Math.Floor(OreYieldsSettings.OrigAmtSteel * OreYieldsSettings.multiplySteel);
        DefDatabase<ThingDef>.GetNamed("MineablePlasteel").building.mineableYield = (int)Math.Floor(OreYieldsSettings.OrigAmtPlasteel * OreYieldsSettings.multiplyPlasteel);
        DefDatabase<ThingDef>.GetNamed("MineableUranium").building.mineableYield = (int)Math.Floor(OreYieldsSettings.OrigAmtUranium * OreYieldsSettings.multiplyUranium);
        DefDatabase<ThingDef>.GetNamed("MineableJade").building.mineableYield = (int)Math.Floor(OreYieldsSettings.OrigAmtJade * OreYieldsSettings.multiplyJade);
        DefDatabase<ThingDef>.GetNamed("MineableComponentsIndustrial").building.mineableYield = (int)Math.Floor(OreYieldsSettings.OrigAmtComps * OreYieldsSettings.multiplyComps);
      }
    }
  }
}
