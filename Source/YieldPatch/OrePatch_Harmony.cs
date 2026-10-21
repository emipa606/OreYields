using System.Reflection;
using HarmonyLib;
using Verse;

namespace YieldPatch;

[StaticConstructorOnStartup]
public static class OrePatch_Harmony
{
    static OrePatch_Harmony()
    {
        new Harmony("Mlie.OreYields").PatchAll(Assembly.GetExecutingAssembly());
    }
}