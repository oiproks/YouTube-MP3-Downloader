using System;
using System.Collections.Generic;
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
