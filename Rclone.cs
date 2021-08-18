using System;
using System.Diagnostics;

namespace RSL
{
    public class Rclone
    {
        public Process Process;

        //Rclone constructor
        public Rclone()
        {
            Process = new Process();
            Process.StartInfo.FileName = "rclone";
            Process.StartInfo.RedirectStandardInput = true;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.RedirectStandardError = true;
            if (!Program.settings.DebugMode)
            {
                Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process.StartInfo.CreateNoWindow = true;
            }
        }
        //Checks if rclone is properly installed
        public bool DoesExist()
        {
            var output = RunCommand("--version");
            return output.Output.Contains("os/version");
        }

        //Run an rclone command
        public ProcessOutput RunCommand(string command)
        {
            Process.StartInfo.Arguments = command;

            Process.Start();
            Process.WaitForExit();
            ProcessOutput output = new ProcessOutput(Process.StandardOutput.ReadToEnd(), Process.StandardError.ReadToEnd());

            if (Program.settings.DebugMode)
            {
                Console.WriteLine($"Running command {command}");
                Console.WriteLine($"stdout: {output.Output}\nstderr:{output.Error}");
            }
            return output;
        }
    }
}