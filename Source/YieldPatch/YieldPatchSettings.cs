using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace YieldPatch;

/// <summary>
///     Definition of the settings for the mod
/// </summary>
internal class YieldPatchSettings : ModSettings
{
    private const float MaxYieldValue = 20f;
    private const float MaxMassValue = 5f;

    public const float BaseValue = 1f;
    private const float LowValue = 0.01f;
    private static Vector2 scrollPosition = Vector2.zero;
    public Dictionary<string, float> ModifiedMass = new();
    private List<string> modifiedMassKeys;
    private List<float> modifiedMassValues;
    public Dictionary<string, float> ModifiedThings = new();

    private List<string> modifiedThingsKeys;
    private List<float> modifiedThingsValues;
    public float UraniumMultiplier = 1f;

    /// <summary>
    ///     Saving and loading the values
    /// </summary>
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look(ref ModifiedThings, "modifiedThings", LookMode.Value, LookMode.Value,
            ref modifiedThingsKeys, ref modifiedThingsValues);
        Scribe_Collections.Look(ref ModifiedMass, "modifiedMass", LookMode.Value, LookMode.Value,
            ref modifiedMassKeys, ref modifiedMassValues);
    }

    public void DoSettingsWindowContents(Rect inRect)
    {
        var keys = ModifiedThings.Keys.ToList();
        keys.Reverse();
        var rect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
        var rect2 = new Rect(0f, 0f, inRect.width - 30f, 500 + (keys.Count * 60));
        Widgets.BeginScrollView(rect, ref scrollPosition, rect2);
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(rect2);
        listingStandard.Label("OYTitleOne".Translate());
        listingStandard.Label("OYTitleTwo".Translate());
        listingStandard.Label("OYTitleThree".Translate());
        listingStandard.Gap();
        if (listingStandard.ButtonTextLabeled("OYResetLabel".Translate(), "Reset".Translate()))
        {
            for (var num = keys.Count - 1; num >= 0; num--)
            {
                ModifiedThings[keys[num]] = BaseValue;
            }
        }

        if (listingStandard.ButtonTextLabeled("OYIncreaseByTenLabel".Translate(), "OYIncreaseByTen".Translate()))
        {
            for (var num = keys.Count - 1; num >= 0; num--)
            {
                ModifiedThings[keys[num]] *= 1.1f;
                if (ModifiedThings[keys[num]] > MaxYieldValue)
                {
                    ModifiedThings[keys[num]] = MaxYieldValue;
                }
            }
        }

        if (listingStandard.ButtonTextLabeled("OYDecreaseByTenLabel".Translate(), "OYDecreaseByTen".Translate()))
        {
            for (var num = keys.Count - 1; num >= 0; num--)
            {
                ModifiedThings[keys[num]] *= 0.9f;
            }
        }

        for (var num = keys.Count - 1; num >= 0; num--)
        {
            var thing = DefDatabase<ThingDef>.GetNamed(keys[num]);
            var value = ModifiedThings[keys[num]];
            value = Widgets.HorizontalSlider(listingStandard.GetRect(35f), value, 0, MaxYieldValue, false,
                $"{thing.label.CapitalizeFirst()}: {Math.Round(thing.building.mineableYield * value, 0)}", null, null,
                0.01f);
            ModifiedThings[keys[num]] = value;
        }

        listingStandard.GapLine();
        listingStandard.Label("OYTitleFour".Translate());

        listingStandard.Gap();
        keys = ModifiedMass.Keys.ToList();
        keys.Reverse();
        if (listingStandard.ButtonTextLabeled("OYResetLabel".Translate(), "Reset".Translate()))
        {
            for (var num = keys.Count - 1; num >= 0; num--)
            {
                ModifiedMass[keys[num]] = BaseValue;
            }
        }

        if (listingStandard.ButtonTextLabeled("OYIncreaseByTenLabel".Translate(), "OYIncreaseByTen".Translate()))
        {
            for (var num = keys.Count - 1; num >= 0; num--)
            {
                ModifiedMass[keys[num]] *= 1.1f;
                if (ModifiedMass[keys[num]] > MaxMassValue)
                {
                    ModifiedMass[keys[num]] = MaxMassValue;
                }
            }
        }

        if (listingStandard.ButtonTextLabeled("OYDecreaseByTenLabel".Translate(), "OYDecreaseByTen".Translate()))
        {
            for (var num = keys.Count - 1; num >= 0; num--)
            {
                ModifiedMass[keys[num]] *= 0.9f;
                if (ModifiedMass[keys[num]] < LowValue)
                {
                    ModifiedMass[keys[num]] = LowValue;
                }
            }
        }

        for (var num = keys.Count - 1; num >= 0; num--)
        {
            var thing = DefDatabase<ThingDef>.GetNamed(keys[num]);
            var value = ModifiedMass[keys[num]];
            value = Widgets.HorizontalSlider(listingStandard.GetRect(35f), value, LowValue, MaxMassValue, false,
                $"{thing.label.CapitalizeFirst()}: {Math.Max(LowValue / 10, thing.BaseMass)}", null, null,
                0.01f);
            ModifiedMass[keys[num]] = value;
        }

        if (YieldPatchMod.CurrentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("OYModVersionLabel".Translate(YieldPatchMod.CurrentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
        Widgets.EndScrollView();
        Write();
    }
}