using System;

namespace RSL
{
    public class Updater
    {
        public readonly static string CurrentVersion = "0.0.0";
        public static string LatestVersion = "";
        readonly static string Username = "PaulIRL";
        readonly static string RepoName = "Rookie-s-Sideloader-Rewritten";
        readonly static string VersionURL = $"https://raw.githubusercontent.com/{Username}/{RepoName}/master/Version";

        public static bool IsUpdateAvailable()
        {
            string result = string.Empty;
            using (var webClient = new System.Net.WebClient())
            {
                result = webClient.DownloadString(VersionURL);
            }
            Console.WriteLine(result);
            return false;
        }
    }
}