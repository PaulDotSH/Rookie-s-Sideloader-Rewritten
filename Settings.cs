using System;
using System.IO;
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
            File.WriteAllText("settings.json",JsonConvert.SerializeObject(Program.settings, Formatting.Indented));
        }

        public void Load()
        {
            if (File.Exists("settings.json"))
                Program.settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("settings.json"));
        }
    }
}