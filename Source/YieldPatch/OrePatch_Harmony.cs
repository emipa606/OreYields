using System;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace YieldPatch;

[StaticConstructorOnStartup]
internal class OrePatch_Harmony
{
    static OrePatch_Harmony()
    {
        new Harmony("Mlie.OreYields").PatchAll();
    }

    [HarmonyPatch(typeof(BuildingProperties), "get_EffectiveMineableYield")]
    public static class BuildingProperties_EffectiveMineableYield
    {
        private static void Postfix(ref int __result, BuildingProperties __instance)
        {
            var thingParent =
                (from ThingDef thingDef in DefDatabase<ThingDef>.AllDefsListForReading
                    where thingDef.building != null && thingDef.building.mineableThing == __instance.mineableThing
                    select thingDef).FirstOrDefault();
            if (thingParent == null ||
                !YieldPatchMod.settings.modifiedThings.TryGetValue(thingParent.defName, out var thing))
            {
                return;
            }

            __result = (int)Math.Round(__result * thing, 1);
        }
    }

    [HarmonyPatch(typeof(ThingDef), "get_BaseMass")]
    public static class ThingDef_BaseMass
    {
        private static void Postfix(ref float __result, ThingDef __instance)
        {
            if (!YieldPatchMod.settings.modifiedMass.TryGetValue(__instance.defName, out var value))
            {
                return;
            }

            __result *= value;
        }
    }

    [HarmonyPatch(typeof(StatExtension), "GetStatValue", typeof(Thing), typeof(StatDef), typeof(bool), typeof(int))]
    public static class StatExtension_GetStatValue
    {
        private static void Postfix(Thing thing, StatDef stat, ref float __result)
        {
            if (stat != StatDefOf.Mass)
            {
                return;
            }

            if (!YieldPatchMod.settings.modifiedMass.TryGetValue(thing.def.defName, out var value))
            {
                return;
            }

            __result *= value;
        }
    }
}