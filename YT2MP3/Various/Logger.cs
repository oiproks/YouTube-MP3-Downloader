using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT2MP3.Various
{
    static class Logger
    {
        static private string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.dat");
        static private string today = string.Empty;

        static public void WriteError(string log, bool newDay = false)
        {
            if (string.IsNullOrEmpty(today) || !today.Equals(DateTime.Now.ToString("yyyy-MM-dd"))) {
                today = DateTime.Now.ToString("yyyy-MM-dd");
                WriteError(today + Environment.NewLine, true);
            }
            try
            {
                if (!newDay)
                    log = $"\t{DateTime.Now:hh:mm:ss}\t[Error] {log}{Environment.NewLine}";
                File.AppendAllText(path, log);
            }
            catch
            {
                File.Delete(path);
                File.Create(path).Close();
            }
        }

        static public void WriteSuccess(string log, bool newDay = false)
        {
            if (string.IsNullOrEmpty(today) || !today.Equals(DateTime.Now.ToString("yyyy-MM-dd")))
            {
                today = DateTime.Now.ToString("yyyy-MM-dd");
                WriteSuccess(today + Environment.NewLine, true);
            }
            try
            {
                if (!newDay)
                    log = $"\t{DateTime.Now:hh:mm:ss}\t[Downloaded] {log}{Environment.NewLine}";
                File.AppendAllText(path, log);
            }
            catch
            {
                File.Delete(path);
                File.Create(path).Close();
            }
        }
    }
}
