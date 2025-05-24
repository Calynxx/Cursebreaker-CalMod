using CalMod.Helpers;
using System.Collections.Generic;

namespace CalMod
{
    public static class ItemHandlerUtil
    {
        private static ItemHandler _itemHandler;
        private static List<ItemHandler.Item> _itemList;
        private static List<int> _stackablesList;
        private static int _maxStack;

        public static void SetAllItemsStackSize()
        {
            if (!CalMod.BoundConfig.enableStackMod.Value)
            {
                CalMod.Logger.LogInfo($"Stack limit mod is disabled. Stack limits unchanged.");
                return;
            }

            _itemHandler = ItemHandler.instance;
            _itemList = _itemHandler.items;
            _stackablesList = _itemHandler.stackableItemIds;
            _maxStack = ItemHandler.MaxStack;
            if (_itemHandler == null || _itemList == null || _stackablesList == null || _maxStack == 0)
            {
                CalMod.Logger.LogError("Instance or fields not initialised. Cannot set stack limit.");
                return;
            }

            foreach (ItemHandler.Item item in _itemList)
            {
                // Don't mess with the null item !
                if (item.typeId == 0)
                    continue;

                // Don't reduce stack limit of items that are already maxed out
                if (item.maxStack < _maxStack)
                {
                    // Don't make bags stackable
                    if (item.storageSize != 0)
                    {
                        CalMod.Logger.LogInfo($"{item.itemName} is a storage container, skipping...");
                        continue;
                    }

                    if (!_stackablesList.Contains(item.typeId))
                        _stackablesList.Add(item.typeId);

                    item.maxStack = _maxStack;
                }
                else
                {
                    CalMod.Logger.LogInfo($"{item.itemName} already has max stack limit, skipping...");
                }
            }

            _itemHandler.stackableItemIds = _stackablesList;
            _itemHandler.items = _itemList;
        }
    }
}
