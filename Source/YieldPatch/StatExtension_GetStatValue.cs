using HarmonyLib;
using RimWorld;
using Verse;

namespace YieldPatch;

[HarmonyPatch(typeof(StatExtension), nameof(StatExtension.GetStatValue), typeof(Thing), typeof(StatDef), typeof(bool),
    typeof(int))]
public static class StatExtension_GetStatValue
{
    public static void Postfix(Thing thing, StatDef stat, ref float __result)
    {
        if (stat != StatDefOf.Mass)
        {
            return;
        }

        if (!YieldPatchMod.Settings.ModifiedMass.TryGetValue(thing.def.defName, out var value))
        {
            return;
        }

        __result *= value;
    }
}