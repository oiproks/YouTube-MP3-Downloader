using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT2MP3
{
    class DownloadHistory
    {
        private string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "history.dat");

        public List<VideoList> HistoryList { get; }

        public DownloadHistory()
        {
            HistoryList = new List<VideoList>();

            if (File.Exists(path)) {
                try
                {
                    var lines = File.ReadLines(path);
                    foreach (var line in lines)
                    {
                        HistoryList.Add(new VideoList(
                            line.Split(new string[] { "|||" }, StringSplitOptions.None)[0],
                            line.Split(new string[] { "|||" }, StringSplitOptions.None)[1])
                        );
                    }
                } catch
                {
                    File.Delete(path);
                    File.Create(path);
                }
            }
        }

        public void AddToHistory(VideoList vl) 
        {
            HistoryList.Add(vl);

            string newLine = string.Concat(string.Format("{0}|||{1}", vl.Title, vl.URL), Environment.NewLine);

            File.AppendAllText(path, newLine);
        }
    }
}
