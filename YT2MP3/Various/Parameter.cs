using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YT2MP3.Various
{
    public class Parameter
    {
        public AudioFormats audioFormat { get; set; }
        
        public VideoFormats videoFormat { get; set; }

        public int quality { get; set; }

        public string destination { get; set; }

        public Parameter(VideoFormats value)
        {
            videoFormat = value;
        }

        public Parameter(AudioFormats value)
        {
            audioFormat = value;
        }

        public Parameter(int value)
        {
            quality = value;
        }

        public Parameter(string value)
        {
            destination = value;
        }
    }
}
