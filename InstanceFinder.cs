using System;
using System.Linq;
using System.Reflection;

namespace CalMod.Helpers
{
    public static class InstanceFinder
    {
        public static Scr_ItemHandler GetScrItemHandlerInstance()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Scr_ItemHandler));
            Type scrItemHandlerType = assembly.GetTypes().FirstOrDefault((Type t) => t.Name == "Scr_ItemHandler");
            if (scrItemHandlerType == null)
            {
                CalMod.Logger.LogError("Could not find type for Scr_ItemHandler");
                return null;
            }

            FieldInfo fieldInfo = scrItemHandlerType.GetFields(BindingFlags.Static | BindingFlags.Public).FirstOrDefault((FieldInfo f) => f.FieldType == scrItemHandlerType);
            if (fieldInfo == null)
            {
                CalMod.Logger.LogError("Could not find instance of Scr_ItemHandler");
                return null;
            }

            return (Scr_ItemHandler)fieldInfo.GetValue(null);
        }

        public static Scr_LootHandler GetScrLootHandlerInstance()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Scr_LootHandler));
            Type scrLootHandlerType = assembly.GetTypes().FirstOrDefault((Type t) => t.Name == "Scr_LootHandler");
            if (scrLootHandlerType == null)
            {
                CalMod.Logger.LogError("Could not find type for Scr_LootHandler");
                return null;
            }

            FieldInfo fieldInfo = scrLootHandlerType.GetFields(BindingFlags.Static | BindingFlags.Public).FirstOrDefault((FieldInfo f) => f.FieldType == scrLootHandlerType);
            if (fieldInfo == null)
            {
                CalMod.Logger.LogError("Could not find instance of Scr_LootHandler");
                return null;
            }

            return (Scr_LootHandler)fieldInfo.GetValue(null);
        }
    }
}
