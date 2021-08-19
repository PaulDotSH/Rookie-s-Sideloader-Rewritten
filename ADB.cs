using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RSL
{
    public class ADB
    {
        public Process Process;
        
        //If there is a command running, locked is set to true
        static bool Locked = false;
        public static List<AndroidDevice> Devices = new List<AndroidDevice>();
        public string[] GetAllDevices()
        {
            //Get all devices from adb, remove "List of devices attached, split by new line
            var devices = Utilities.RemoveEverythingBeforeFirst(RunCommand("devices").Output, "\n").Split('\n').ToList();
            //Remove any empty strings
            devices.RemoveAll(str => String.IsNullOrEmpty(str));
            //Clear previous adb device list
            Devices.Clear();
            
            foreach (var device in devices)
            {
                //First item is the device id, 2nd item is the status
                var temp = device.Split("\t");
                
                //Get device status, "device" or "offline"
                AndroidDevice.status status;
                if (temp[1] == "device")
                    status = AndroidDevice.status.device;
                else status = AndroidDevice.status.offline;
                Console.WriteLine($"Device with id {temp[0]} has the status {temp[1]}");
                //Add device to list
                Devices.Add(new AndroidDevice(temp[0],status));
            }
            return devices.ToArray();
        }
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