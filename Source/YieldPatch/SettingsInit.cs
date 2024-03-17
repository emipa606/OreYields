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
        if (YieldPatchMod.settings.modifiedThings == null)
        {
            YieldPatchMod.settings.modifiedThings = new Dictionary<string, float>();
        }

        if (YieldPatchMod.settings.modifiedMass == null)
        {
            YieldPatchMod.settings.modifiedMass = new Dictionary<string, float>();
        }

        var tempList = new Dictionary<string, float>();
        var tempList2 = new Dictionary<string, float>();
        foreach (var thingDef in thingsWithYield)
        {
            if (YieldPatchMod.settings.modifiedThings.TryGetValue(thingDef.defName, out var thing))
            {
                tempList[thingDef.defName] = thing;
            }
            else
            {
                tempList[thingDef.defName] = YieldPatchSettings.baseValue;
            }

            if (YieldPatchMod.settings.modifiedMass.TryGetValue(thingDef.building.mineableThing.defName, out var value))
            {
                tempList2[thingDef.building.mineableThing.defName] =
                    value;
            }
            else
            {
                tempList2[thingDef.building.mineableThing.defName] = YieldPatchSettings.baseValue;
            }
        }

        YieldPatchMod.settings.modifiedThings = tempList;
        YieldPatchMod.settings.modifiedMass = tempList2;
        Log.Message(
            $"[Ore Yields]: Found {thingsWithYield.Count()} minable things: {string.Join(", ", YieldPatchMod.settings.modifiedThings.Keys)}");
    }
}