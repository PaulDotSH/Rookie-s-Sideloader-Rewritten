using System;
using System.Diagnostics;
using System.IO;

namespace RSL
{
    public class ADB
    {
        public Process Process;
        static bool Locked = false;
        public ADB()
        {
            Process = new Process();
            Process.StartInfo.FileName = "adb";
            Process.StartInfo.RedirectStandardInput = true;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.RedirectStandardError = true;
            if (!Settings.DebugMode)
            {
                Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process.StartInfo.CreateNoWindow = true;
            }
        }

        public bool DoesExist()
        {
            var output = RunCommand("version");
            return output.Output.Contains("Android Debug Bridge");
        }
        public ProcessOutput RunCommand(string command)
        {
            if (Locked) throw new Exception(LockedMessage);
            Locked = true;

            Process.StartInfo.Arguments = command;

            Process.Start();
            Process.WaitForExit();
            ProcessOutput output = new ProcessOutput(Process.StandardOutput.ReadToEnd(), Process.StandardError.ReadToEnd());
            Locked = false;
            if (Settings.DebugMode)
                Console.WriteLine($"stdout: {output.Output}\nstderr:{output.Error}");
            return output;
        }
        string LockedMessage = "Error, the current adb thread is locked";
    }
}