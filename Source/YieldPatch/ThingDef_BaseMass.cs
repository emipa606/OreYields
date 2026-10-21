using HarmonyLib;
using Verse;

namespace YieldPatch;

[HarmonyPatch(typeof(ThingDef), nameof(ThingDef.BaseMass), MethodType.Getter)]
public static class ThingDef_BaseMass
{
    public static void Postfix(ref float __result, ThingDef __instance)
    {
        if (!YieldPatchMod.Settings.ModifiedMass.TryGetValue(__instance.defName, out var value))
        {
            return;
        }

        __result *= value;
    }
}