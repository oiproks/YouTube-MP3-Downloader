using System.Collections.Generic;

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

        public static List<string> GetAllFormats()
        {
            return new List<string>
            {
                MP3.Value,
                AAC.Value,
                FLAC.Value,
                M4A.Value,
                WAV.Value
            };
        }

        public static AudioFormats GetAudioFormat(string format)
        {
            return new AudioFormats(format.ToLower());
        }
    }
}
