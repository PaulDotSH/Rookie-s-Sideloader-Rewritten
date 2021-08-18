using System;
using System.Diagnostics;
using System.IO;

namespace RSL
{
    public class ADB
    {
        static Process Process;
        static bool Locked = false;
        public ADB()
        {
            Process = new Process();
            Process.StartInfo.FileName = "adb";
            Process.StartInfo.RedirectStandardInput = true;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.RedirectStandardError = true;
        }
        
        public ProcessOutput RunCommand(string command)
        {
            Locked = true;
            Console.WriteLine("entered");
            Process.StartInfo.Arguments = command;
            if (File.Exists(Process.StartInfo.FileName))
                Console.WriteLine("Exists");
            Process.Start();
            Process.WaitForExit();
            ProcessOutput output = new ProcessOutput(Process.StandardOutput.ReadToEnd(), Process.StandardError.ReadToEnd());
            Locked = false;
            if (Settings.DebugMode)
                Console.WriteLine($"stdout: {output.Output}\nstderr:{output.Error}");
            return output;
        }
        string LockedMessage = "Error, the current adb thread is locked";
        
        public bool CheckAdb()
        {
            if (Locked) throw new Exception(LockedMessage);
            try
            {
                Process.StartInfo.Arguments = "version";
                Process.Start();
            }
            catch { return false; }
            return true;
        }
    }
}