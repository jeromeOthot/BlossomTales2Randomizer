using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace BlossomTales2.Randomizer.mm
{
    public static class GameLogger
    {
        private static MethodInfo _cachedLogInfoMethod;

        public static void LogInfo(string message)
        {
            if(_cachedLogInfoMethod == null)
            {
                Type loggerClass = typeof(Game1).Assembly.GetType("BlossomTales2.Logger");
                _cachedLogInfoMethod = loggerClass.GetMethod("LogInfo", BindingFlags.Static | BindingFlags.Public);
            }
            _cachedLogInfoMethod.Invoke(null, new object[] { message });
        }
    }
}
