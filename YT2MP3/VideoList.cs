using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT2MP3
{
    class VideoList
    {
        public string Title { get; set; }
        public string URL { get; set; }

        public VideoList (string title, string url)
        {
            Title = Utils.CleanTitle(title);
            URL = url;
        }
    }
}
