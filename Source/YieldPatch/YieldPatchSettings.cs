using System;
using System.Collections.Generic;
using System.Linq;
using SettingsHelper;
using UnityEngine;
using Verse;

namespace YieldPatch
{
    /// <summary>
    ///     Definition of the settings for the mod
    /// </summary>
    internal class YieldPatchSettings : ModSettings
    {
        private static Vector2 scrollPosition = Vector2.zero;

        private static readonly float maxYieldValue = 20f;
        private static readonly float maxMassValue = 5f;

        public static readonly float baseValue = 1f;
        private static readonly float lowValue = 0.01f;
        public Dictionary<string, float> modifiedMass = new Dictionary<string, float>();
        private List<string> modifiedMassKeys;
        private List<float> modifiedMassValues;
        public Dictionary<string, float> modifiedThings = new Dictionary<string, float>();

        private List<string> modifiedThingsKeys;
        private List<float> modifiedThingsValues;
        public float UraniumMultiplier = 1f;

        /// <summary>
        ///     Saving and loading the values
        /// </summary>
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref modifiedThings, "modifiedThings", LookMode.Value, LookMode.Value,
                ref modifiedThingsKeys, ref modifiedThingsValues);
            Scribe_Collections.Look(ref modifiedMass, "modifiedMass", LookMode.Value, LookMode.Value,
                ref modifiedMassKeys, ref modifiedMassValues);
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            var keys = modifiedThings.Keys.ToList();
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
                    modifiedThings[keys[num]] = baseValue;
                }
            }

            if (listingStandard.ButtonTextLabeled("OYIncreaseByTenLabel".Translate(), "OYIncreaseByTen".Translate()))
            {
                for (var num = keys.Count - 1; num >= 0; num--)
                {
                    modifiedThings[keys[num]] *= 1.1f;
                    if (modifiedThings[keys[num]] > maxYieldValue)
                    {
                        modifiedThings[keys[num]] = maxYieldValue;
                    }
                }
            }

            if (listingStandard.ButtonTextLabeled("OYDecreaseByTenLabel".Translate(), "OYDecreaseByTen".Translate()))
            {
                for (var num = keys.Count - 1; num >= 0; num--)
                {
                    modifiedThings[keys[num]] *= 0.9f;
                }
            }

            for (var num = keys.Count - 1; num >= 0; num--)
            {
                var thing = DefDatabase<ThingDef>.GetNamed(keys[num]);
                var value = modifiedThings[keys[num]];
                listingStandard.AddLabeledSlider(
                    $"{thing.label.CapitalizeFirst()}: {Math.Round(thing.building.mineableYield * value, 0)}",
                    ref value, 0, maxYieldValue, value.ToString(), null, 0.01f);
                modifiedThings[keys[num]] = value;
            }

            listingStandard.GapLine();
            listingStandard.Label("OYTitleFour".Translate());

            listingStandard.Gap();
            keys = modifiedMass.Keys.ToList();
            keys.Reverse();
            if (listingStandard.ButtonTextLabeled("OYResetLabel".Translate(), "Reset".Translate()))
            {
                for (var num = keys.Count - 1; num >= 0; num--)
                {
                    modifiedMass[keys[num]] = baseValue;
                }
            }

            if (listingStandard.ButtonTextLabeled("OYIncreaseByTenLabel".Translate(), "OYIncreaseByTen".Translate()))
            {
                for (var num = keys.Count - 1; num >= 0; num--)
                {
                    modifiedMass[keys[num]] *= 1.1f;
                    if (modifiedMass[keys[num]] > maxMassValue)
                    {
                        modifiedMass[keys[num]] = maxMassValue;
                    }
                }
            }

            if (listingStandard.ButtonTextLabeled("OYDecreaseByTenLabel".Translate(), "OYDecreaseByTen".Translate()))
            {
                for (var num = keys.Count - 1; num >= 0; num--)
                {
                    modifiedMass[keys[num]] *= 0.9f;
                    if (modifiedMass[keys[num]] < lowValue)
                    {
                        modifiedMass[keys[num]] = lowValue;
                    }
                }
            }

            for (var num = keys.Count - 1; num >= 0; num--)
            {
                var thing = DefDatabase<ThingDef>.GetNamed(keys[num]);
                var value = modifiedMass[keys[num]];
                listingStandard.AddLabeledSlider(
                    $"{thing.label.CapitalizeFirst()}: {Math.Max(lowValue / 10, thing.BaseMass)}", ref value, lowValue,
                    maxMassValue, value.ToString(), null, 0.01f);
                modifiedMass[keys[num]] = value;
            }

            listingStandard.End();
            Widgets.EndScrollView();
            Write();
        }
    }
}