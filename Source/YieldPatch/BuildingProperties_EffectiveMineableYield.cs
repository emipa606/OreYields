using System;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace YieldPatch;

[HarmonyPatch(typeof(BuildingProperties), nameof(BuildingProperties.EffectiveMineableYield), MethodType.Getter)]
public static class BuildingProperties_EffectiveMineableYield
{
    public static void Postfix(ref int __result, BuildingProperties __instance)
    {
        var thingParent =
            (from ThingDef thingDef in DefDatabase<ThingDef>.AllDefsListForReading
                where thingDef.building != null && thingDef.building.mineableThing == __instance.mineableThing
                select thingDef).FirstOrDefault();
        if (thingParent == null ||
            !YieldPatchMod.Settings.ModifiedThings.TryGetValue(thingParent.defName, out var thing))
        {
            return;
        }

        __result = (int)Math.Round(__result * thing, 1);
    }
}