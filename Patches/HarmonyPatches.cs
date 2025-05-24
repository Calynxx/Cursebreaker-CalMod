using HarmonyLib;

namespace CalMod.Patches
{
    class HarmonyPatches
    {
        // This obfuscated reference is kind of a pain, because Scr_ItemHandler doesn't have a Start method
        // To find its name you'll have to find the public List<Scr_ItemHandler.Item> under Scr_ItemHandler, 
        // then search through methods that reference it until you find the one that loads the items list from XML,
        // it starts by clearing the all the lists then does Resources.Load("XML\Items"), you'll know it when you see it
        [HarmonyPatch(typeof(ItemHandler), nameof(ItemHandler.LoadItems))]
        [HarmonyPostfix]
        static void ModifyStackLimitPatch()
        {
            if (CalMod.BoundConfig.enableStackMod.Value)
            {
                CalMod.Logger.LogInfo("Patching item stack limits...");
                ItemHandlerUtil.SetAllItemsStackSize();
            }
            else
            {
                CalMod.Logger.LogInfo("Stack limit mod is disabled. Stack limits unchanged.");
                return;
            }
        }

        [HarmonyPatch(typeof(LootHandler), "Start")]
        [HarmonyPostfix]
        static void ModifyDropRatesPatch()
        {
            if (CalMod.BoundConfig.enableDropRateMod.Value)
            {
                CalMod.Logger.LogInfo("Patching drop rates...");
                LootHandlerUtil.SetAllDropRatesToOneHundredPercent();
            }
            else
            {
                CalMod.Logger.LogInfo("Drop rate mod is disabled. Drop rates unchanged.");
                return;
            }
        }

        [HarmonyPatch(typeof(OgreMountainBanditsHandler), "Start")]
        [HarmonyPostfix]
        static void RemoveBanditAmbushPatch(OgreMountainBanditsHandler __instance)
        {
            if (CalMod.BoundConfig.enableBanditMod.Value)
            {
                CalMod.Logger.LogInfo("Removing bandit ambushes...");
                // To get the name for this this reference,
                // look inside Scr_OgreMountainBanditsHandler and there should be a public List<Scr_OgreMountainBanditsHandler.Gang>
                // Basically this holds a list of the 3(?) or 4(?) bandit squads inside it and is only populated on initialisation,
                // so once cleared the bandit ambushes are effectively removed
                __instance.gangs.Clear();
            }
            else
            {
                CalMod.Logger.LogInfo("Bandit ambush mod is disabled. Ambushes unchanged.");
                return;
            }
        }
    }
}
