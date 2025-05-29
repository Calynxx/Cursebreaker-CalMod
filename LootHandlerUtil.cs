using System;
using System.Collections.Generic;
using CalMod.Helpers;

namespace CalMod
{
    public static class LootHandlerUtil
    {
        private static LootHandler _LootHandler;
        private static List<LootHandler.DropTable> _dropTables;

        public static void SetAllDropRatesToOneHundredPercent()
        {
            _LootHandler = LootHandler.instance;
            _dropTables = _LootHandler?.dropTables;
            if (_LootHandler == null || _dropTables == null)
            {
                CalMod.Logger.LogError("Instance or fields not initialised. Cannot set drop rates.");
                return;
            }

            foreach (LootHandler.DropTable dropTable in _dropTables)
            {
                var dropTableEntries = FieldFinder.GetDropTableEntriesForDropTable(dropTable);
                if (dropTableEntries == null)
                {
                    CalMod.Logger.LogError($"Could not get drop table entries for one of the tables. Skipping...");
                    continue;
                }

                foreach(LootHandler.DropTableItem dropTableEntry in dropTableEntries)
                {
                    try
                    {
                        FieldFinder.SetDropRateForDropTableEntry(dropTableEntry, 1);
                    }
                    catch (Exception ex)
                    {
                        CalMod.Logger.LogError(ex.Message);
                        return;
                    }
                }

                FieldFinder.SetDropTableEntriesForDropTable(dropTable, dropTableEntries);
            }

            _LootHandler.dropTables = _dropTables;
        }
    }
}
