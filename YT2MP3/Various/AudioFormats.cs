using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT2MP3.Various
{
    public class AudioFormats
    {
        private AudioFormats(string value) { Value = value; }

        public string Value { get; set; }

        public static AudioFormats MP3 { get { return new AudioFormats("mp3"); } }

        public static AudioFormats AAC { get { return new AudioFormats("aac"); } }

        public static AudioFormats FLAC { get { return new AudioFormats("flac"); } }

        public static AudioFormats M4A { get { return new AudioFormats("m4a"); } }

        public static AudioFormats WAV { get { return new AudioFormats("wav"); } }

        public static AudioFormats BEST { get { return new AudioFormats("best"); } }
    }
}
