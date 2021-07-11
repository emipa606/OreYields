using System;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace YieldPatch
{
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
                if (thingParent == null || !YieldPatchMod.settings.modifiedThings.ContainsKey(thingParent.defName))
                {
                    return;
                }

                __result = (int) Math.Round(__result * YieldPatchMod.settings.modifiedThings[thingParent.defName], 1);
            }
        }

        [HarmonyPatch(typeof(ThingDef), "get_BaseMass")]
        public static class ThingDef_BaseMass
        {
            private static void Postfix(ref float __result, ThingDef __instance)
            {
                if (!YieldPatchMod.settings.modifiedMass.ContainsKey(__instance.defName))
                {
                    return;
                }

                __result *= YieldPatchMod.settings.modifiedMass[__instance.defName];
            }
        }

        [HarmonyPatch(typeof(StatExtension), "GetStatValue", typeof(Thing), typeof(StatDef), typeof(bool))]
        public static class StatExtension_GetStatValue
        {
            private static void Postfix(Thing thing, StatDef stat, ref float __result)
            {
                if (stat != StatDefOf.Mass)
                {
                    return;
                }

                if (!YieldPatchMod.settings.modifiedMass.ContainsKey(thing.def.defName))
                {
                    return;
                }

                __result *= YieldPatchMod.settings.modifiedMass[thing.def.defName];
            }
        }
    }
}