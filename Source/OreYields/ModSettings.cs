using RimWorld;
using RimWorld.Planet;
using System;
using UnityEngine;
using Verse;

namespace OreYields
{
  public class OreYieldsSettings : ModSettings
  {
    public static float multiplyMineableSilver => multiplySilver;
    public static float multiplyMineableGold => multiplyGold;
    public static float multiplyMineableSteel => multiplySteel;
    public static float multiplyMineablePlasteel => multiplyPlasteel;
    public static float multiplyMineableUranium => multiplyUranium;
    public static float multiplyMineableJade => multiplyJade;
    public static float multiplyMineableComps => multiplyComps;

    public static int OrigAmtSilver;
    public static int OrigAmtGold;
    public static int OrigAmtSteel;
    public static int OrigAmtPlasteel;
    public static int OrigAmtUranium;
    public static int OrigAmtJade;
    public static int OrigAmtComps;

    // Internal reference only. DO NOT call outside of this class.
    public static float multiplySilver = 1f;
    public static float multiplyGold = 1f;
    public static float multiplySteel = 1f;
    public static float multiplyPlasteel = 1f;
    public static float multiplyUranium = 1f;
    public static float multiplyJade = 1f;
    public static float multiplyComps = 1f;
    // End warning

    public override void ExposeData()
    {
      base.ExposeData();
      Scribe_Values.Look(ref multiplySilver, "OYAmountToMultiplySilver");
      Scribe_Values.Look(ref multiplyGold, "OYAmountToMultiplyGold");
      Scribe_Values.Look(ref multiplySteel, "OYAmountToMultiplySteel");
      Scribe_Values.Look(ref multiplyPlasteel, "OYAmountToMultiplyPlasteel");
      Scribe_Values.Look(ref multiplyUranium, "OYAmountToMultiplyUranium");
      Scribe_Values.Look(ref multiplyJade, "OYAmountToMultiplyJade");
      Scribe_Values.Look(ref multiplyComps, "OYAmountToMultiplyComps");
    }
  }

  public class OreYieldMod : Mod
  {
    OreYieldsSettings settings;
    public OreYieldMod(ModContentPack con) : base(con)
    {
      this.settings = GetSettings<OreYieldsSettings>();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
      Listing_Standard listing = new Listing_Standard();
      listing.Begin(inRect);

      if (Current.ProgramState == ProgramState.Playing)
      {
        listing.Label("OYMultiplyAmountLabelSilver".Translate() + ": [" + OreYieldsSettings.OrigAmtSilver.ToString() + " x " + (OreYieldsSettings.multiplySilver * 100).ToString() + "%] = " + (OreYieldsSettings.OrigAmtSilver * OreYieldsSettings.multiplySilver).ToString());
        OreYieldsSettings.multiplySilver = RoundToNearestHalf(listing.Slider(OreYieldsSettings.multiplySilver, 0f, 20f));

        listing.Label("OYMultiplyAmountLabelGold".Translate() + ": [" + OreYieldsSettings.OrigAmtGold.ToString() + " x " + (OreYieldsSettings.multiplyGold * 100).ToString() + "%] = " + (OreYieldsSettings.OrigAmtGold * OreYieldsSettings.multiplyGold).ToString());
        OreYieldsSettings.multiplyGold = RoundToNearestHalf(listing.Slider(OreYieldsSettings.multiplyGold, 0f, 20f));

        listing.Label("OYMultiplyAmountLabelSteel".Translate() + ": [" + OreYieldsSettings.OrigAmtSteel.ToString() + " x " + (OreYieldsSettings.multiplySteel * 100).ToString() + "%] = " + (OreYieldsSettings.OrigAmtSteel * OreYieldsSettings.multiplySteel).ToString());
        OreYieldsSettings.multiplySteel = RoundToNearestHalf(listing.Slider(OreYieldsSettings.multiplySteel, 0f, 20f));

        listing.Label("OYMultiplyAmountLabelPlasteel".Translate() + ": [" + OreYieldsSettings.OrigAmtPlasteel.ToString() + " x " + (OreYieldsSettings.multiplyPlasteel * 100).ToString() + "%] = " + (OreYieldsSettings.OrigAmtPlasteel * OreYieldsSettings.multiplyPlasteel).ToString());
        OreYieldsSettings.multiplyPlasteel = RoundToNearestHalf(listing.Slider(OreYieldsSettings.multiplyPlasteel, 0f, 20f));

        listing.Label("OYMultiplyAmountLabelUranium".Translate() + ": [" + OreYieldsSettings.OrigAmtUranium.ToString() + " x " + (OreYieldsSettings.multiplyUranium * 100).ToString() + "%] = " + (OreYieldsSettings.OrigAmtUranium * OreYieldsSettings.multiplyUranium).ToString());
        OreYieldsSettings.multiplyUranium = RoundToNearestHalf(listing.Slider(OreYieldsSettings.multiplyUranium, 0f, 20f));

        listing.Label("OYMultiplyAmountLabelJade".Translate() + ": [" + OreYieldsSettings.OrigAmtJade.ToString() + " x " + (OreYieldsSettings.multiplyJade * 100).ToString() + "%] = " + (OreYieldsSettings.OrigAmtJade * OreYieldsSettings.multiplyJade).ToString());
        OreYieldsSettings.multiplyJade = RoundToNearestHalf(listing.Slider(OreYieldsSettings.multiplyJade, 0f, 20f));

        listing.Label("OYMultiplyAmountLabelComps".Translate() + ": [" + OreYieldsSettings.OrigAmtComps.ToString() + " x " + (OreYieldsSettings.multiplyComps * 100).ToString() + "%] = " + (OreYieldsSettings.OrigAmtComps * OreYieldsSettings.multiplyComps).ToString());
        OreYieldsSettings.multiplyComps = RoundToNearestHalf(listing.Slider(OreYieldsSettings.multiplyComps, 0f, 20f));
      }
      else
      {
        listing.Label("OYOnlyInGame".Translate());
      }

      if (OreYieldsSettings.OrigAmtSilver * OreYieldsSettings.multiplySilver >= 75)
      {
        listing.Label("OYStackWarning".Translate());
        listing.Gap(36);
      }

      listing.End();

      if (Current.ProgramState == ProgramState.Playing)
        UpdateAllChanges();

      base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
      return "OYTitle".Translate();
    }

    private float RoundToNearestHalf(float val)
    { 
      return (float)Math.Round(val * 2, MidpointRounding.AwayFromZero) / 2;
    }

    public static void UpdateAllChanges()
    {
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
