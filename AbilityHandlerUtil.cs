using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalMod
{
    public static class AbilityHandlerUtil
    {
        private static AbilityHandler _instance;
        private static List<AbilityHandler.Ability> _abilities;

        public static void ReduceTimeForRefineAbilties()
        {
            _instance = AbilityHandler.instance;
            _abilities = _instance?.abilities;

            if (_instance == null || _abilities == null)
            {
                CalMod.Logger.LogError("Instance or fields not initialised. Cannot modify abilities.");
                return;
            }

            foreach (var ability in _abilities)
            {
                if (ability.name.Contains("Refine", StringComparison.InvariantCultureIgnoreCase))
                {
                    ability.attackDelay = 0.5f;
                    ability.attackTime = 1f;
                }
            }

            _instance.abilities = _abilities;
        }
    }
}
