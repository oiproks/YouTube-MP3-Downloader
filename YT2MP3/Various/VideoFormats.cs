using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT2MP3.Various
{
    public class VideoFormats
    {
        private VideoFormats(string value) { Value = value; }

        public string Value { get; set; }

        public static VideoFormats P2160 { get { return new VideoFormats("2160"); } }

        public static VideoFormats P1440 { get { return new VideoFormats("1440"); } }

        public static VideoFormats P1080 { get { return new VideoFormats("1080"); } }
        
        public static VideoFormats P720 { get { return new VideoFormats("720"); } }

        public static VideoFormats P480 { get { return new VideoFormats("480"); } }
    }
}
