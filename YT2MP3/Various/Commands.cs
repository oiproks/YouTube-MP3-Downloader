using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YT2MP3.Various;

namespace YT2MP3.Statics
{
    public class Commands
    {
        public enum Com
        {
            NoPlaylist,
            ExtractAudio,
            AudioFormat,
            AudioQuality,
            Output,
            VideoFormat
        };

        internal static string BuildCommandLine(string execPath, CommandList commandList, string URL)
        {
            string line = $"/c {Utils.AddQuotesIfRequired(execPath)}";
            foreach (Tuple<Com, Parameter> command in commandList.GetCommands())
            {
                switch (command.Item1)
                {
                    case Com.AudioFormat:
                        line = string.Concat(line, AudioFormat(command.Item2.audioFormat));
                        break;
                    case Com.ExtractAudio:
                        line = string.Concat(line, ExtractAudio);
                        break;
                    case Com.AudioQuality:
                        line = string.Concat(line, AudioQuality(command.Item2.quality));
                        break;
                    case Com.NoPlaylist:
                        line = string.Concat(line, NoPlaylist);
                        break;
                    case Com.VideoFormat:
                        line = string.Concat(line, VideoFormat(command.Item2.videoFormat));
                        break;
                    case Com.Output:
                        line = string.Concat(line, Output(command.Item2.destination));
                        break;
                    default:
                        break;
                }
            }

            return line += $" {URL}";
        }

        internal static string NoPlaylist { 
            get { return " --no-playlist"; }
        }

        internal static string ExtractAudio { 
            get { return " --extract-audio"; }
        }
        
        /// <summary>
        /// Select audio conversion format
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        internal static string AudioFormat(AudioFormats format) {
            return $" --audio-format {format.Value}";
        }
        
        /// <summary>
        /// Select conversion quality.
        /// </summary>
        /// <param name="quality">int from 0 (high quality) to 9 (low quality)</param>
        /// <returns></returns>
        internal static string AudioQuality(int quality) {
            return $" --audio-quality {quality}";
        }

        /// <summary>
        /// Select video conversion format
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        internal static string VideoFormat(VideoFormats format) {
            return $" -f \"bestvideo[height<=?{format.Value}]+bestaudio/best\"";
        }

        /// <summary>
        /// Pass the filename with path where to save the file.
        /// </summary>
        /// <param name="fileName">filename with path</param>
        /// <returns></returns>
        internal static string Output(string fileName) {
            return $" -o \"{fileName}\"";
        }

        /// <summary>
        /// Download Playlist.
        /// </summary>
        /// <returns></returns>
        internal static string Playlist() {
            return $" --yes-playlist";
        }
    }
}
