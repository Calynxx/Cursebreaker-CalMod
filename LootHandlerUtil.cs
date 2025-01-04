using System;
using System.Collections.Generic;
using CalMod.Helpers;

namespace CalMod
{
    public static class LootHandlerUtil
    {
        private static Scr_LootHandler _LootHandler;
        private static List<Scr_LootHandler.BNDMKKHGBGO> _dropTables;

        public static void SetAllDropRatesToOneHundredPercent()
        {
            _LootHandler = InstanceFinder.GetScrLootHandlerInstance();
            _dropTables = FieldFinder.GetDropTableList();
            if (_LootHandler == null || _dropTables == null)
            {
                CalMod.Logger.LogError("Instance or fields not initialised. Cannot set drop rates.");
                return;
            }

            foreach (Scr_LootHandler.BNDMKKHGBGO dropTable in _dropTables)
            {
                var dropTableEntries = FieldFinder.GetDropTableEntriesForDropTable(dropTable);
                if (dropTableEntries == null)
                {
                    // This references the droptable property loaded from the xml property 'name', and there isn't much point in having yet another reference to update
                    //CalMod.Logger.LogError($"Could not get drop table entries for {dropTable.JIBIDNMCNAD}. Cannot set drop rates.");
                    CalMod.Logger.LogError($"Could not get drop table entries for one of the tables. Skipping...");
                    continue;
                }

                foreach(Scr_LootHandler.FLOHIKDBBNJ dropTableEntry in dropTableEntries)
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

            FieldFinder.SetDropTableList(_dropTables);
        }
    }
}
