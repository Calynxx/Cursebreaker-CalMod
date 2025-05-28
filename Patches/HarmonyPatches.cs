using Handlers;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CalMod.Patches
{
    class HarmonyPatches
    {
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

        [HarmonyPatch(typeof(SalvagingHandler), nameof(SalvagingHandler.SalvageItemSlot))]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> ModifyStackSalvagePatch(IEnumerable<CodeInstruction> instructions)
        {
            if (CalMod.BoundConfig.enableStackMod.Value)
            {
                CalMod.Logger.LogInfo("Patching stacked item salvage instructions...");

                return new CodeMatcher(instructions)
                .MatchForward(true,
                    new CodeMatch(OpCodes.Ldloc_0),
                    new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(ItemHandler.Item), nameof(ItemHandler.Item.level)))
                )
                .Repeat(matchAction: matcher =>
                {
                    matcher.Advance(1);
                    matcher.InsertAndAdvance(
                        new CodeInstruction(OpCodes.Ldarg_1),
                        new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(UI_Inventory.ItemSlot), nameof(UI_Inventory.ItemSlot.itemAmount))),
                        new CodeInstruction(OpCodes.Mul)
                    );
                })
                .InstructionEnumeration();
            }
            else
            {
                CalMod.Logger.LogInfo("Stack limit mod is disabled. Salvage behaviour unchanged.");
                return instructions;
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

        /*[HarmonyPatch(typeof(OgreMountainBanditsHandler), "Start")]
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
        }*/
    }
}
