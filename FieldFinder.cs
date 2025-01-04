using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CalMod.Helpers
{
    public static class FieldFinder
    {
        public const int GoldId = 131; // Used to tell apart the stackable item list and the storable item list (gold is stackable and is not a container)

        public static int GetItemMaxStackValue()
        {
            return 2100000000; // This is the value they use internally
            //return Scr_ItemHandler.NNEMDDGKOMD; // Alternatively, we could reference the constant defined in the ItemHandler
        }

        public static List<Scr_ItemHandler.Item> GetItemList()
        {
            Type type = Assembly.GetAssembly(typeof(Scr_ItemHandler)).GetTypes().FirstOrDefault((Type t) => t.Name == "Scr_ItemHandler");
            if (type == null)
            {
                CalMod.Logger.LogWarning("Could not get type for Scr_ItemHandler.");
                return null;
            }

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>) && fieldInfo.FieldType.GetGenericArguments()[0] == typeof(Scr_ItemHandler.Item))
                {
                    UnityEngine.Object @object = UnityEngine.Object.FindObjectOfType(type);
                    if (@object == null)
                    {
                        CalMod.Logger.LogError("Could not find instance of Scr_ItemHandler");
                        return null;
                    }

                    List<Scr_ItemHandler.Item> result = (List<Scr_ItemHandler.Item>)fieldInfo.GetValue(@object);
                    CalMod.Logger.LogInfo("ItemList found");
                    return result;
                }
            }

            CalMod.Logger.LogError("Could not find ItemList");
            return null;
        }

        public static void SetItemList(List<Scr_ItemHandler.Item> newValue)
        {
            Type type = Assembly.GetAssembly(typeof(Scr_ItemHandler)).GetTypes().FirstOrDefault((Type t) => t.Name == "Scr_ItemHandler");
            if (type == null)
            {
                CalMod.Logger.LogWarning("Could not get type for Scr_ItemHandler.");
                return;
            }

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>) && fieldInfo.FieldType.GetGenericArguments()[0] == typeof(Scr_ItemHandler.Item))
                {
                    UnityEngine.Object @object = UnityEngine.Object.FindObjectOfType(type);
                    if (@object == null)
                    {
                        CalMod.Logger.LogError("Could not find instance of Scr_ItemHandler");
                        return;
                    }

                    fieldInfo.SetValue(@object, newValue);
                    CalMod.Logger.LogInfo("Successfully set ItemList");
                    return;
                }
            }

            CalMod.Logger.LogError("Could not find ItemList");
            return;
        }

        public static List<int> GetStackableItemList()
        {
            Type type = Assembly.GetAssembly(typeof(Scr_ItemHandler)).GetTypes().FirstOrDefault((Type t) => t.Name == "Scr_ItemHandler");
            if (type == null)
            {
                CalMod.Logger.LogWarning("Could not get type for Scr_ItemHandler.");
                return null;
            }

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>) && fieldInfo.FieldType.GetGenericArguments()[0] == typeof(int))
                {
                    UnityEngine.Object @object = UnityEngine.Object.FindObjectOfType(type);
                    if (@object == null)
                    {
                        CalMod.Logger.LogError("Could not find instance of Scr_ItemHandler");
                        return null;
                    }

                    List<int> result = (List<int>)fieldInfo.GetValue(@object);
                    if (result.Contains(GoldId))
                    {
                        CalMod.Logger.LogInfo("StackablesList found");
                        return result;
                    }
                }
            }

            CalMod.Logger.LogError("Could not find StackablesList");
            return null;
        }

        public static void SetStackableItemList(List<int> newValue)
        {
            Type type = Assembly.GetAssembly(typeof(Scr_ItemHandler)).GetTypes().FirstOrDefault((Type t) => t.Name == "Scr_ItemHandler");
            if (type == null)
            {
                CalMod.Logger.LogWarning("Could not get type for Scr_ItemHandler.");
                return;
            }

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>) && fieldInfo.FieldType.GetGenericArguments()[0] == typeof(int))
                {
                    UnityEngine.Object @object = UnityEngine.Object.FindObjectOfType(type);
                    if (@object == null)
                    {
                        CalMod.Logger.LogError("Could not find instance of Scr_ItemHandler");
                        return;
                    }

                    List<int> result = (List<int>)fieldInfo.GetValue(@object);
                    if (result.Contains(GoldId))
                    {
                        CalMod.Logger.LogInfo("StackablesList found");
                        fieldInfo.SetValue(@object, newValue);
                        return;
                    }
                }
            }

            CalMod.Logger.LogError("Could not find StackablesList");
            return;
        }

        public static List<Scr_LootHandler.BNDMKKHGBGO> GetDropTableList()
        {
            // To update these obfuscated references look inside Scr_LootHandler.Start() and then inside the method it calls,
            // this method will populate the loot tables by looping through an XML file, and you'll be able to see the names of the loot table class,
            // then inside another loop you'll see the class name for the loot table entries

            Type type = Assembly.GetAssembly(typeof(Scr_LootHandler)).GetTypes().FirstOrDefault((Type t) => t.Name == "Scr_LootHandler");
            if (type == null)
            {
                CalMod.Logger.LogWarning("Could not get type for Scr_LootHandler.");
                return null;
            }

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>) && fieldInfo.FieldType.GetGenericArguments()[0] == typeof(Scr_LootHandler.BNDMKKHGBGO))
                {
                    UnityEngine.Object @object = UnityEngine.Object.FindObjectOfType(type);
                    if (@object == null)
                    {
                        CalMod.Logger.LogError("Could not find instance of Scr_LootHandler");
                        return null;
                    }

                    List<Scr_LootHandler.BNDMKKHGBGO> result = (List<Scr_LootHandler.BNDMKKHGBGO>)fieldInfo.GetValue(@object);
                    CalMod.Logger.LogInfo("LootTables found");
                    return result;
                }
            }

            CalMod.Logger.LogError("Could not find DropTables");
            return null;
        }

        public static void SetDropTableList(List<Scr_LootHandler.BNDMKKHGBGO> newValue)
        {
            Type type = Assembly.GetAssembly(typeof(Scr_LootHandler)).GetTypes().FirstOrDefault((Type t) => t.Name == "Scr_LootHandler");
            if (type == null)
            {
                CalMod.Logger.LogWarning("Could not get type for Scr_LootHandler.");
                return;
            }

            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>) && fieldInfo.FieldType.GetGenericArguments()[0] == typeof(Scr_LootHandler.BNDMKKHGBGO))
                {
                    UnityEngine.Object @object = UnityEngine.Object.FindObjectOfType(type);
                    if (@object == null)
                    {
                        CalMod.Logger.LogError("Could not find instance of Scr_LootHandler");
                        return;
                    }

                    fieldInfo.SetValue(@object, newValue);
                    CalMod.Logger.LogInfo("Successfully set DropTable");
                    return;
                }
            }

            CalMod.Logger.LogError("Could not find DropTable");
            return;
        }

        public static List<Scr_LootHandler.FLOHIKDBBNJ> GetDropTableEntriesForDropTable(Scr_LootHandler.BNDMKKHGBGO dropTable)
        {
            if (dropTable == null)
            {
                CalMod.Logger.LogError("Drop table instance uninitialised. Cannot get drop table entries.");
                return null;
            }

            FieldInfo[] fields = typeof(Scr_LootHandler.BNDMKKHGBGO).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>) && fieldInfo.FieldType.GetGenericArguments()[0] == typeof(Scr_LootHandler.FLOHIKDBBNJ))
                {
                    List<Scr_LootHandler.FLOHIKDBBNJ> result = (List<Scr_LootHandler.FLOHIKDBBNJ>)fieldInfo.GetValue(dropTable);
                    CalMod.Logger.LogInfo("LootTablesEntries found.");
                    return result;
                }
            }

            CalMod.Logger.LogError("Could not find DropTableEntries");
            return null;
        }

        public static void SetDropTableEntriesForDropTable(Scr_LootHandler.BNDMKKHGBGO dropTable, List<Scr_LootHandler.FLOHIKDBBNJ> newValue)
        {
            if (dropTable == null)
            {
                CalMod.Logger.LogError("Drop table instance uninitialised. Cannot get drop table entries.");
                return;
            }

            FieldInfo[] fields = typeof(Scr_LootHandler.BNDMKKHGBGO).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>) && fieldInfo.FieldType.GetGenericArguments()[0] == typeof(Scr_LootHandler.FLOHIKDBBNJ))
                {
                    fieldInfo.SetValue(dropTable, newValue);
                    CalMod.Logger.LogInfo("Set value for LootTablesEntries.");
                    return;
                }
            }

            CalMod.Logger.LogError("Could not find DropTableEntries");
            return;
        }

        public static void SetDropRateForDropTableEntry(Scr_LootHandler.FLOHIKDBBNJ dropTableEntry, int newValue)
        {
            if (dropTableEntry == null)
            {
                CalMod.Logger.LogError("Drop table entry instance uninitialised. Cannot set drop rate.");
                return;
            }

            FieldInfo[] fields = typeof(Scr_LootHandler.FLOHIKDBBNJ).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo fieldInfo in fields)
            {
                // !!! REMEMBER TO UPDATE THIS HARDCODED STRING REFERENCE !!!
                // We have to get the drop rate property using reflection by name (yucky) because it uses the 'internal' access level,
                // which means we can't reference it directly like we did for other classes and properties,
                // and it has no distinguishing features other than its name within its class (it's 1 of 4 internal ints)

                // You can get this name from the same place you get the droptableentry class name (described above in GetDropTableList()),
                // it uses the xml property 'rate', so it shouldn't be hard to find
                if (fieldInfo.FieldType == typeof(int) && fieldInfo.Name == "ACGDBAEBGHI")
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
