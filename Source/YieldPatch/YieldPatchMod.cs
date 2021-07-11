using UnityEngine;
using Verse;

namespace YieldPatch
{
    [StaticConstructorOnStartup]
    internal class YieldPatchMod : Mod
    {
        /// <summary>
        ///     The instance of the settings to be read by the mod
        /// </summary>
        public static YieldPatchMod instance;

        /// <summary>
        ///     The private settings
        /// </summary>
        public static YieldPatchSettings settings;

        /// <summary>
        ///     Cunstructor
        /// </summary>
        /// <param name="content"></param>
        public YieldPatchMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<YieldPatchSettings>();
        }

        /// <summary>
        ///     The title for the mod-settings
        /// </summary>
        /// <returns></returns>
        public override string SettingsCategory()
        {
            return "Ore Yields";
        }

        /// <summary>
        ///     The settings-window
        ///     For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
        /// </summary>
        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            settings.DoSettingsWindowContents(inRect);
        }
    }
}