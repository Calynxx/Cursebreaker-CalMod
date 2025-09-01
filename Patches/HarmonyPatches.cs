using Handlers;
using HarmonyLib;
using System;
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

        [HarmonyPatch(typeof(AbilityHandler), nameof(AbilityHandler.ManualStart))]
        [HarmonyPostfix]
        static void ModifyAbilitiesPatch()
        {
            if (CalMod.BoundConfig.enableFastRefining.Value)
            {
                CalMod.Logger.LogInfo("Patching refining abilities...");
                AbilityHandlerUtil.ReduceTimeForRefineAbilities();
            }
            else
            {
                CalMod.Logger.LogInfo("Fast refining is disabled. Refining abilities unchanged.");
                return;
            }

            if (CalMod.BoundConfig.enableFastFilleting.Value)
            {
                CalMod.Logger.LogInfo("Patching fillet abilities...");
                AbilityHandlerUtil.ReduceTimeForFilletAbilities();
            }
            else
            {
                CalMod.Logger.LogInfo("Fast fillet is disabled. Fillet abilities unchanged.");
                return;
            }

            if (CalMod.BoundConfig.enableFastLogSplitting.Value)
            {
                CalMod.Logger.LogInfo("Patching log split abilities...");
                AbilityHandlerUtil.ReduceTimeForLogSplitAbilities();
            }
            else
            {
                CalMod.Logger.LogInfo("Fast log splitting is disabled. Log split abilities unchanged.");
                return;
            }
        }

        [HarmonyPatch(typeof(UI_Crafting), "Craft", new Type[] { typeof(object[]) })]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> ModifyCraftPatch(IEnumerable<CodeInstruction> instructions)
        {
            if (CalMod.BoundConfig.enableFastCrafting.Value)
            {
                CalMod.Logger.LogInfo("Patching crafting instructions...");
                return new CodeMatcher(instructions)
                .MatchForward(false,
                    new CodeMatch(OpCodes.Ldc_R4, 1f),
                    new CodeMatch(OpCodes.Stfld, AccessTools.Field(typeof(UI_Crafting), nameof(UI_Crafting.craftingTimer)))
                )
                .SetAndAdvance(OpCodes.Ldc_R4, 0.5f)
                .MatchForward(false,
                    new CodeMatch(OpCodes.Ldc_R4, 1f),
                    new CodeMatch(OpCodes.Callvirt, AccessTools.Method(typeof(UI_HUD), nameof(UI_HUD.ActivateActionTimer)))
                )
                .SetAndAdvance(OpCodes.Ldc_R4, 0.5f)
                .InstructionEnumeration();
            }
            else
            {
                CalMod.Logger.LogInfo("Fast crafting is disabled. Craft behaviour unchanged.");
                return instructions;
            }
        }

        [HarmonyPatch(typeof(UI_Crafting), "Update")]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> ModifyCraftUpdatePatch(IEnumerable<CodeInstruction> instructions)
        {
            if (CalMod.BoundConfig.enableFastCrafting.Value)
            {
                CalMod.Logger.LogInfo("Patching crafting update instructions...");
                return new CodeMatcher(instructions)
                .MatchForward(false,
                    new CodeMatch(OpCodes.Ldarg_0),
                    new CodeMatch(OpCodes.Ldc_R4, 1f),
                    new CodeMatch(OpCodes.Stfld, AccessTools.Field(typeof(UI_Crafting), nameof(UI_Crafting.craftingTimer)))
                )
                .RemoveInstructions(3)
                .InstructionEnumeration();
            }
            else
            {
                CalMod.Logger.LogInfo("Fast crafting is disabled. Craft update behaviour unchanged.");
                return instructions;
            }
        }

        [HarmonyPatch(typeof(PlayerLevels), nameof(PlayerLevels.AddXp))]
        [HarmonyPrefix]
        static void MultiplyExpPatch(SkillsHandler.SkillType t, ref float amount)
        {
            amount *= CalMod.BoundConfig.globalExperienceMultiplier.Value;
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
