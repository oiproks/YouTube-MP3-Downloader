using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YT2MP3.Statics.Commands;

namespace YT2MP3.Various
{
    class CommandList
    {
        private List<Tuple<Com, Parameter>> list;

        public CommandList()
        {
            list = new List<Tuple<Com, Parameter>>();
        }

        public void AddCommand(Com command)
        {
            list.Add(new Tuple<Com, Parameter>(command, null));
        }

        public void AddCommand(Com command, Parameter parameter)
        {
            list.Add(new Tuple<Com, Parameter>(command, parameter));
        }

        public List<Tuple<Com, Parameter>> GetCommands()
        {
            return list;
        }
    }
}
