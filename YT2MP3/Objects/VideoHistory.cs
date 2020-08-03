using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace YT2MP3
{
    public class VideoHistory
    {
        private string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "history.dat");

        public List<VideoInfos> HistoryList { get; }

        public VideoHistory()
        {
            HistoryList = new List<VideoInfos>();

            if (File.Exists(path)) {
                try
                {
                    var lines = File.ReadLines(path);
                    foreach (var line in lines)
                    {
                        HistoryList.Add(new VideoInfos(
                            line.Split(new string[] { "|||" }, StringSplitOptions.None)[0],
                            line.Split(new string[] { "|||" }, StringSplitOptions.None)[1])
                        );
                    }
                } catch
                {
                    File.Delete(path);
                    File.Create(path).Close();
                }
            }
        }

        public void AddToHistory(VideoInfos vi) 
        {
            int counter = 1;
            while (true) {
                if (HistoryList.Find(x => x.Title == vi.Title) == null)
                {
                    HistoryList.Add(vi);
                    break;
                } else
                {
                    vi.Title += $"_{counter}";
                    counter++;
                }
            }

            File.AppendAllText(path, CreateLine(vi));
        }

        public void RemoveFromHistory(VideoInfos vi)
        {
            HistoryList.Remove(HistoryList.Find(x => x.Title == vi.Title && x.URL == vi.URL));

            string history = string.Empty;

            foreach (VideoInfos vis in HistoryList)
            {
                history += CreateLine(vis);
            }

            File.WriteAllText(path, string.Empty);
            File.AppendAllText(path, history);
        }

        private string CreateLine(VideoInfos vi)
        {
            return string.Concat(string.Format("{0}|||{1}", vi.Title, vi.URL), Environment.NewLine);
        }
    }
}
