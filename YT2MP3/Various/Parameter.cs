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
        public AudioFormats format { get; set; }

        public int quality { get; set; }

        public string destination { get; set; }

        public Parameter(AudioFormats value)
        {
            format = value;
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
