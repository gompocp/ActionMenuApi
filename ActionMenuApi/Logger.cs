using MelonLoader;

namespace ActionMenuApi
{
    public static class Logger
    {
        public static void Log(string message)
        {
#if DEBUG
            MelonLogger.Log(message);
#endif            
        }
        public static void LogWarning(string message)
        {
#if DEBUG
            MelonLogger.LogWarning(message);
#endif            
        }
        public static void LogError(string message)
        {
#if DEBUG
            MelonLogger.LogError(message);
#endif            
        }

    }
}