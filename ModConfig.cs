using BepInEx.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace CalMod
{
    public class ModConfig
    {
        
        public readonly ConfigEntry<bool> enableStackMod;
        //public readonly ConfigEntry<bool> enableStorageMod; This was a feature from the mod this is based on but I didn't bother implementing it
        public readonly ConfigEntry<bool> enableDropRateMod;
        //public readonly ConfigEntry<bool> enableBanditMod; This is obsolete with the update to the barbarian ambush system
        public readonly ConfigEntry<bool> enableFastRefining;
        public readonly ConfigEntry<bool> enableFastFilleting;
        public readonly ConfigEntry<bool> enableFastLogSplitting;
        public readonly ConfigEntry<bool> enableFastCrafting;
        public readonly ConfigEntry<float> globalExperienceMultiplier;

        public ModConfig(ConfigFile cfg)
        {
            cfg.SaveOnConfigSet = false;

            enableStackMod = cfg.Bind("Requires loading from the main menu to take effect", "All items stackable", true, "Set all items (excluding container items like bags) to stack to the game's internal max limit (2100000000)");
            //enableStorageMod = cfg.Bind("Requires loading from the main menu to take effect", "Increased bag capacity", false, "Increase the number of slots of all container items.");
            enableDropRateMod = cfg.Bind("Requires loading from the main menu to take effect", "Set all NPC drop rates to 100%", false, "Sets the drop rates for all mobs to be 100%, can result in stupidly large numbers of items on the ground.");
            //enableBanditMod = cfg.Bind("Requires loading from the main menu to take effect", "Disable barbarian ambushes", false, "Disables the barbarian ambushes in the Amarien's Mountains area. Note that there are some items that only they can drop.");
            enableFastRefining = cfg.Bind("Requires loading from the main menu to take effect", "Fast ore refining", true, "Makes ore refining (using the chisel and relevant mining traits from trainers) much faster.");
            enableFastFilleting = cfg.Bind("Requires loading from the main menu to take effect", "Fast fish filleting", true, "Makes filleting fish (using the fillet knife and relevant fishing traits from trainers) much faster.");
            enableFastLogSplitting = cfg.Bind("Requires loading from the main menu to take effect", "Fast log splitting", true, "Makes log splitting (using the splitting axe and relevant woodsman traits from trainers) much faster.");
            enableFastCrafting = cfg.Bind("Requires restarting the game to take effect", "Fast crafting", true, "Make crafting items using workbenches faster.");
            globalExperienceMultiplier = cfg.Bind("Can be changed at any time", "Global experience multiplier", 1f, new ConfigDescription("Multiplies all experience gained by this number", new AcceptableValueRange<float>(0f, 100f)));

            ClearOrphanedEntries(cfg);
            cfg.Save();
            cfg.SaveOnConfigSet = true;
            cfg.SettingChanged += Cfg_SettingChanged;
        }

        private void Cfg_SettingChanged(object sender, SettingChangedEventArgs e)
        {
            // You can put code here that will run whenever the settings are changed, possibly allowing changing of some settings without reloading,
            // but due to the way the patches are coded it'll only work when enabling, not disabling, so I'm just leaving this blank
            //ItemHandlerUtil.SetAllItemsStackSize();
        }

        private static void ClearOrphanedEntries(ConfigFile cfg)
        {
            // Find the private property `OrphanedEntries` from the type `ConfigFile`
            PropertyInfo orphanedEntriesProp = AccessTools.Property(typeof(ConfigFile), "OrphanedEntries");
            // And get the value of that property from our ConfigFile instance
            var orphanedEntries = (Dictionary<ConfigDefinition, string>)orphanedEntriesProp.GetValue(cfg);
            // And finally, clear the `OrphanedEntries` dictionary
            orphanedEntries.Clear();
        }
    }

    public enum OverwriteType
    {
        [Description("Max value (2100000000)")]
        MaxValue,
        [Description("Fixed value (user defined)")]
        FixedValue,
    }
}
