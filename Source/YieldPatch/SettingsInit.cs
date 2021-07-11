using System.Collections.Generic;
using System.Linq;
using Verse;

namespace YieldPatch
{
    [StaticConstructorOnStartup]
    public static class SettingsInit
    {
        static SettingsInit()
        {
            var thingsWithYield = from ThingDef thingDef in DefDatabase<ThingDef>.AllDefsListForReading
                where thingDef.building != null && thingDef.building.isResourceRock
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
                if (YieldPatchMod.settings.modifiedThings.ContainsKey(thingDef.defName))
                {
                    tempList[thingDef.defName] = YieldPatchMod.settings.modifiedThings[thingDef.defName];
                }
                else
                {
                    tempList[thingDef.defName] = YieldPatchSettings.baseValue;
                }

                if (YieldPatchMod.settings.modifiedMass.ContainsKey(thingDef.building.mineableThing.defName))
                {
                    tempList2[thingDef.building.mineableThing.defName] =
                        YieldPatchMod.settings.modifiedMass[thingDef.building.mineableThing.defName];
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
}