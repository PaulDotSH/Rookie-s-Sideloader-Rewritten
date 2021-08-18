using System;
using System.Diagnostics;
using System.IO;

namespace RSL
{
    public class ADB
    {
        public Process Process;
        
        //If there is a command running, locked is set to true
        static bool Locked = false;
        
        //ADB Constructor
        public ADB()
        {
            Process = new Process();
            Process.StartInfo.FileName = "adb";
            Process.StartInfo.RedirectStandardInput = true;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.RedirectStandardError = true;
            if (!Program.settings.DebugMode)
            {
                Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process.StartInfo.CreateNoWindow = true;
            }
        }

        //Checks if ADB is properly installed on the current machine
        public bool DoesExist()
        {
            var output = RunCommand("version");
            return output.Output.Contains("Android Debug Bridge");
        }
        
        //Run an adb command
        public ProcessOutput RunCommand(string command)
        {
            if (Locked) throw new Exception(LockedMessage);
            Locked = true;

            Process.StartInfo.Arguments = command;

            Process.Start();
            Process.WaitForExit();
            ProcessOutput output = new ProcessOutput(Process.StandardOutput.ReadToEnd(), Process.StandardError.ReadToEnd());
            Locked = false;
            if (Program.settings.DebugMode)
            {
                Console.WriteLine($"Running command {command}");
                Console.WriteLine($"stdout: {output.Output}\nstderr:{output.Error}");
            }
            return output;
        }
        string LockedMessage = "Error, the current adb thread is locked";
    }
}