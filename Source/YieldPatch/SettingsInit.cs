using System.Collections.Generic;
using System.Linq;
using Verse;

namespace YieldPatch;

[StaticConstructorOnStartup]
public static class SettingsInit
{
    static SettingsInit()
    {
        var thingsWithYield = from ThingDef thingDef in DefDatabase<ThingDef>.AllDefsListForReading
            where thingDef.building is { isResourceRock: true }
            orderby thingDef.label
            select thingDef;
        YieldPatchMod.Settings.ModifiedThings ??= new Dictionary<string, float>();

        YieldPatchMod.Settings.ModifiedMass ??= new Dictionary<string, float>();

        var tempList = new Dictionary<string, float>();
        var tempList2 = new Dictionary<string, float>();
        foreach (var thingDef in thingsWithYield)
        {
            tempList[thingDef.defName] =
                YieldPatchMod.Settings.ModifiedThings.GetValueOrDefault(thingDef.defName, YieldPatchSettings.BaseValue);

            tempList2[thingDef.building.mineableThing.defName] =
                YieldPatchMod.Settings.ModifiedMass.GetValueOrDefault(thingDef.building.mineableThing.defName,
                    YieldPatchSettings.BaseValue);
        }

        YieldPatchMod.Settings.ModifiedThings = tempList;
        YieldPatchMod.Settings.ModifiedMass = tempList2;
        Log.Message(
            $"[Ore Yields]: Found {thingsWithYield.Count()} minable things: {string.Join(", ", YieldPatchMod.Settings.ModifiedThings.Keys)}");
    }
}