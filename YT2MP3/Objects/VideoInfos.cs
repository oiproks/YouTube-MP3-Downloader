﻿namespace YT2MP3
{
    public class VideoInfos
    {
        public string Title { get; set; }
        public string URL { get; set; }

        public VideoInfos(string title, string url)
        {
            Title = Utils.CleanTitle(title);
            URL = url;
        }
    }
}
