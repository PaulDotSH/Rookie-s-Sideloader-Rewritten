using System;
using static RSL.Objects;
using static RSL.Utilities;
using Newtonsoft.Json;

namespace RSL
{
    public class Settings
    {
#if DEBUG
        public bool DebugMode = true;
#else
        public static bool DebugMode = false;
#endif
        public bool FirstRun = true;
        public bool ADBExists = false;
        public bool RcloneExists = false;
        public OSType OS = OSType.Unknown;

        public void Initialise()
        {
            ADBExists = new ADB().DoesExist();
            RcloneExists = new Rclone().DoesExist();
        }

        public void Save()
        {
            Console.WriteLine(JsonConvert.SerializeObject(Program.settings));
        }

        public void Load()
        {
            
        }
    }
}