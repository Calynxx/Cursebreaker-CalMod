using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CalMod.Helpers
{
    public static class FieldFinder
    {

        public static List<LootHandler.DropTableItem> GetDropTableEntriesForDropTable(LootHandler.DropTable dropTable)
        {
            if (dropTable == null)
            {
                CalMod.Logger.LogError("Drop table instance uninitialised. Cannot get drop table entries.");
                return null;
            }

            FieldInfo[] fields = typeof(LootHandler.DropTable).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>) && fieldInfo.FieldType.GetGenericArguments()[0] == typeof(LootHandler.DropTableItem))
                {
                    List<LootHandler.DropTableItem> result = (List<LootHandler.DropTableItem>)fieldInfo.GetValue(dropTable);
                    CalMod.Logger.LogInfo("LootTablesEntries found.");
                    return result;
                }
            }

            CalMod.Logger.LogError("Could not find DropTableEntries");
            return null;
        }

        public static void SetDropTableEntriesForDropTable(LootHandler.DropTable dropTable, List<LootHandler.DropTableItem> newValue)
        {
            if (dropTable == null)
            {
                CalMod.Logger.LogError("Drop table instance uninitialised. Cannot get drop table entries.");
                return;
            }

            FieldInfo[] fields = typeof(LootHandler.DropTable).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>) && fieldInfo.FieldType.GetGenericArguments()[0] == typeof(LootHandler.DropTableItem))
                {
                    fieldInfo.SetValue(dropTable, newValue);
                    CalMod.Logger.LogInfo("Set value for LootTablesEntries.");
                    return;
                }
            }

            CalMod.Logger.LogError("Could not find DropTableEntries");
            return;
        }

        public static void SetDropRateForDropTableEntry(LootHandler.DropTableItem dropTableEntry, int newValue)
        {
            if (dropTableEntry == null)
            {
                CalMod.Logger.LogError("Drop table entry instance uninitialised. Cannot set drop rate.");
                return;
            }

            FieldInfo[] fields = typeof(LootHandler.DropTableItem).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.FieldType == typeof(int) && fieldInfo.Name == "dropRate")
                {
                    fieldInfo.SetValue(dropTableEntry, newValue);
                    CalMod.Logger.LogInfo("Set drop rate.");
                    return;
                }
            }

            CalMod.Logger.LogError("Could not find drop rate.");
            return;
        }
    }
}
