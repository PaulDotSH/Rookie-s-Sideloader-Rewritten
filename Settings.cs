namespace RSL
{
    class Settings
    {
#if DEBUG
        public static bool DebugMode = true;
#else
        public static bool DebugMode = false;
#endif
        public static bool FirstRun = true;
    }
}