using RimWorld;
using RimWorld.Planet;
using System;
using UnityEngine;
using Verse;

namespace OreYields
{
  public class OreYieldsSettings : ModSettings
  {
    public static float multiplySilverYield = 1f;
    public static float multiplyGoldYield = 1f;
    public static float multiplySteelYield = 2f;
    public static float multiplyPlasteelYield = 1f;
    public static float multiplyUraniumYield = 1f;
    public static float multiplyJadeYield = 1f;
    public static float multiplyCompsYield = 2.5f;

    public static float multiplySilverMass = 0.5f;
    public static float multiplyGoldMass = 1f;
    public static float multiplySteelMass = 1f;
    public static float multiplyPlasteelMass = 1f;
    public static float multiplyUraniumMass = 1f;
    public static float multiplyJadeMass = 1f;
    public static float multiplyCompsMass = 1f;

    public static int yieldSilver;
    public static int yieldGold;
    public static int yieldSteel;
    public static int yieldPlasteel;
    public static int yieldUranium;
    public static int yieldJade;
    public static int yieldComps;

    public static float massSilver;
    public static float massGold;
    public static float massSteel;
    public static float massPlasteel;
    public static float massUranium;
    public static float massJade;
    public static float massComps;

    public override void ExposeData()
    {
      base.ExposeData();
      Scribe_Values.Look(ref OreYieldsSettings.multiplySilverYield, "OYAmountToMultiplySilver", 0.0f, true);
      Scribe_Values.Look(ref OreYieldsSettings.multiplyGoldYield, "OYAmountToMultiplyGold", 0.0f, true);
      Scribe_Values.Look(ref OreYieldsSettings.multiplySteelYield, "OYAmountToMultiplySteel", 0.0f, true);
      Scribe_Values.Look(ref OreYieldsSettings.multiplyPlasteelYield, "OYAmountToMultiplyPlasteel", 0.0f, true);
      Scribe_Values.Look(ref OreYieldsSettings.multiplyUraniumYield, "OYAmountToMultiplyUranium", 0.0f, true);
      Scribe_Values.Look(ref OreYieldsSettings.multiplyJadeYield, "OYAmountToMultiplyJade", 0.0f, true);
      Scribe_Values.Look(ref OreYieldsSettings.multiplyCompsYield, "OYAmountToMultiplyComps", 0.0f, true);

      Scribe_Values.Look(ref OreYieldsSettings.multiplySilverMass, "multiplySilverMass", 0.0f, true);
      Scribe_Values.Look(ref OreYieldsSettings.multiplyGoldMass, "multiplyGoldMass", 0.0f, true);
      Scribe_Values.Look(ref OreYieldsSettings.multiplySteelMass, "multiplySteelMass", 0.0f, true);
      Scribe_Values.Look(ref OreYieldsSettings.multiplyPlasteelMass, "multiplyPlasteelMass", 0.0f, true);
      Scribe_Values.Look(ref OreYieldsSettings.multiplyUraniumMass, "multiplyUraniumMass", 0.0f, true);
      Scribe_Values.Look(ref OreYieldsSettings.multiplyJadeMass, "multiplyJadeMass", 0.0f, true);
      Scribe_Values.Look(ref OreYieldsSettings.multiplyCompsMass, "multiplyCompsMass", 0.0f, true);
    }
  }

  public class OreYieldMod : Mod
  {
    public bool resetDefaults;
    public Vector2 scrollPosition;
    private OreYieldsSettings settings;

    public OreYieldMod(ModContentPack con) : base(con)
    {
      settings = GetSettings<OreYieldsSettings>();
    }

    public override void DoSettingsWindowContents(Rect canvas)
    {
      Listing_Standard lister = new Listing_Standard();

      // lister.ColumnWidth = canvas.width - 80f;
      float height = canvas.y + 800f; // set height here
      Rect viewRect = new Rect(0f, 0f, canvas.width - 260f, height);


      lister.Begin(canvas);
      lister.BeginScrollView(new Rect(120f, 0f, canvas.width - 240f, canvas.height), ref scrollPosition, ref viewRect);
      lister.Settings_Header("YieldsHeader".Translate(), Color.clear, GameFont.Medium, TextAnchor.MiddleLeft);
      lister.GapLine();
      lister.Gap(12f);
      lister.Settings_SliderLabeled(AddFlooredResultToLabel("OYMultiplyAmountLabelSilver".Translate(), OreYieldsSettings.multiplySilverYield, OreYieldsSettings.yieldSilver), "%", ref OreYieldsSettings.multiplySilverYield, 0.0f, 10f, 1f, 1);
      lister.Settings_SliderLabeled(AddFlooredResultToLabel("OYMultiplyAmountLabelGold".Translate(), OreYieldsSettings.multiplyGoldYield, OreYieldsSettings.yieldGold), "%", ref OreYieldsSettings.multiplyGoldYield, 0.0f, 10f, 1f, 1, -1f);
      lister.Settings_SliderLabeled(AddFlooredResultToLabel("OYMultiplyAmountLabelSteel".Translate(), OreYieldsSettings.multiplySteelYield, OreYieldsSettings.yieldSteel), "%", ref OreYieldsSettings.multiplySteelYield, 0.0f, 10f, 1f, 1);
      lister.Settings_SliderLabeled(AddFlooredResultToLabel("OYMultiplyAmountLabelPlasteel".Translate(), OreYieldsSettings.multiplyPlasteelYield, OreYieldsSettings.yieldPlasteel), "%", ref OreYieldsSettings.multiplyPlasteelYield, 0.0f, 10f, 1f, 1);
      lister.Settings_SliderLabeled(AddFlooredResultToLabel("OYMultiplyAmountLabelUranium".Translate(), OreYieldsSettings.multiplyUraniumYield, OreYieldsSettings.yieldUranium), "%", ref OreYieldsSettings.multiplyUraniumYield, 0.0f, 10f, 1f, 1);
      lister.Settings_SliderLabeled(AddFlooredResultToLabel("OYMultiplyAmountLabelJade".Translate(), OreYieldsSettings.multiplyJadeYield, OreYieldsSettings.yieldJade), "%", ref OreYieldsSettings.multiplyJadeYield, 0.0f, 10f, 1f, 1);
      lister.Settings_SliderLabeled(AddFlooredResultToLabel("OYMultiplyAmountLabelComps".Translate(), OreYieldsSettings.multiplyCompsYield, OreYieldsSettings.yieldComps), "%", ref OreYieldsSettings.multiplyCompsYield, 0.0f, 10f, 1f, 1);
        
      if (OreYieldsSettings.yieldSilver * OreYieldsSettings.multiplySilverYield >= 75.0 || OreYieldsSettings.yieldGold * OreYieldsSettings.multiplyGoldYield >= 75.0 || (OreYieldsSettings.yieldSteel * OreYieldsSettings.multiplySteelYield >= 75.0 || OreYieldsSettings.yieldPlasteel * OreYieldsSettings.multiplyPlasteelYield >= 75.0) || (OreYieldsSettings.yieldUranium * OreYieldsSettings.multiplyUraniumYield >= 75.0 || OreYieldsSettings.yieldJade * OreYieldsSettings.multiplyJadeYield >= 75.0) || OreYieldsSettings.yieldComps * OreYieldsSettings.multiplyCompsYield >= 75.0)
        lister.Label("OYStackWarning".Translate(), -1f, null);

      lister.Gap(36f);
      lister.Settings_Header("MassHeader".Translate(), Color.clear, GameFont.Medium, TextAnchor.MiddleLeft);
      lister.GapLine();
      lister.Gap(12);
      lister.Settings_SliderLabeled(AddResultToLabel("multiplySilverMass".Translate(), OreYieldsSettings.multiplySilverMass, OreYieldsSettings.massSilver), "%", ref OreYieldsSettings.multiplySilverMass, 0.0f, 3f, 1f, 1);
      lister.Settings_SliderLabeled(AddResultToLabel("multiplyGoldMass".Translate(), OreYieldsSettings.multiplyGoldMass, OreYieldsSettings.massGold), "%", ref OreYieldsSettings.multiplyGoldMass, 0.0f, 3f, 1f, 1);
      lister.Settings_SliderLabeled(AddResultToLabel("multiplySteelMass".Translate(), OreYieldsSettings.multiplySteelMass, OreYieldsSettings.massSteel), "%", ref OreYieldsSettings.multiplySteelMass, 0.0f, 3f, 1f, 1);
      lister.Settings_SliderLabeled(AddResultToLabel("multiplyPlasteelMass".Translate(), OreYieldsSettings.multiplyPlasteelMass, OreYieldsSettings.massPlasteel), "%", ref OreYieldsSettings.multiplyPlasteelMass, 0.0f, 3f, 1f, 1);
      lister.Settings_SliderLabeled(AddResultToLabel("multiplyUraniumMass".Translate(), OreYieldsSettings.multiplyUraniumMass, OreYieldsSettings.massUranium), "%", ref OreYieldsSettings.multiplyUraniumMass, 0.0f, 3f, 1f, 1);
      lister.Settings_SliderLabeled(AddResultToLabel("multiplyJadeMass".Translate(), OreYieldsSettings.multiplyJadeMass, OreYieldsSettings.massJade), "%", ref OreYieldsSettings.multiplyJadeMass, 0.0f, 3f, 1f, 1);
      lister.Settings_SliderLabeled(AddResultToLabel("multiplyCompsMass".Translate(), OreYieldsSettings.multiplyCompsMass, OreYieldsSettings.massComps), "%", ref OreYieldsSettings.multiplyCompsMass, 0.0f, 3f, 1f, 1);

      lister.Gap(24f);
      //resetDefaults = lister.ButtonText("ResetValues".Translate());

      lister.End();
      lister.EndScrollView(ref viewRect);

      base.DoSettingsWindowContents(canvas);
    }

    public override void WriteSettings()
    {
      if (resetDefaults)
      {
        OreYieldsSettings.multiplySilverYield = 1f;
        OreYieldsSettings.multiplyGoldYield = 1f;
        OreYieldsSettings.multiplySteelYield = 2f;
        OreYieldsSettings.multiplyPlasteelYield = 1f;
        OreYieldsSettings.multiplyUraniumYield = 1f;
        OreYieldsSettings.multiplyJadeYield = 1f;
        OreYieldsSettings.multiplyCompsYield = 2.5f;

        OreYieldsSettings.multiplySilverMass = 0.5f;
        OreYieldsSettings.multiplyGoldMass = 1f;
        OreYieldsSettings.multiplySteelMass = 1f;
        OreYieldsSettings.multiplyPlasteelMass = 1f;
        OreYieldsSettings.multiplyUraniumMass = 1f;
        OreYieldsSettings.multiplyJadeMass = 1f;
        OreYieldsSettings.multiplyCompsMass = 1f;
        resetDefaults = false;
      }
      OreYieldMod.UpdateAllChanges();
      base.WriteSettings();
    }

    public override string SettingsCategory()
    {
      return "OYTitle".Translate();
    }

    public string AddFlooredResultToLabel(string label, float multiplier, float mass)
    {
      return label + ": " + Math.Floor(mass * multiplier).ToString();
    }

    public string AddResultToLabel(string label, float multiplier, float mass)
    {
      return label + ": " + (mass * multiplier).ToString();
    }

    public static void UpdateAllChanges()
    {
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
