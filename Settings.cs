using static RSL.Objects;
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
        public static bool ADBExists = false;
        public static OSType OS = OSType.Unknown;

        public void Initialise()
        {
            //Check OS
        }
    }
}