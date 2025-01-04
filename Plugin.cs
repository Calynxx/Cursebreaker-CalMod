using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using CalMod.Patches;
using HarmonyLib;
using System.ComponentModel;

namespace CalMod;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class CalMod : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    internal static ModConfig BoundConfig { get; private set; } = null!;

    private readonly Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
    private static CalMod Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        BoundConfig = new ModConfig(base.Config);

        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        Logger.LogInfo("Patching...");
        Harmony.CreateAndPatchAll(typeof(HarmonyPatches));
    }
}
