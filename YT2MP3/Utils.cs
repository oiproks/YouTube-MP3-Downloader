using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YT2MP3
{
    static class Utils
    {
        static public string CleanTitle(string title)
        {
            return Regex.Replace(title, @"\t|\n|\r", ""); ;
        }

        static public string SetDestinationPath(string folderPath, out string destinationPath, bool save = true)
        {
            destinationPath = string.Empty;
            string folder = string.Empty;

            if (!string.IsNullOrEmpty(folderPath) && Directory.Exists(folderPath))
            {
                if (save)
                    SaveConfig(Settings.DownloadFolder, folderPath);

                destinationPath = folderPath;
                folder = destinationPath.Substring(destinationPath.LastIndexOf("\\") + 1);

                destinationPath = Path.Combine(destinationPath, "%(title)s.%(ext)s");
            }

            return folder;
        }

        static public void SaveConfig(string setting, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[setting].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }

    public enum ColourMode
    {
        Night,
        Day
    }

    static class OnTopMode
    {
        public const string False = "0";
        public const string True = "1";
    }

    static class Settings
    {
        public const string OnTop = "onTop";
        public const string DownloadFolder = "lastUsedFolder";
        public const string Interface = "interface";
    }
}
