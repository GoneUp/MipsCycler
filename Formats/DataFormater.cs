using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MipsCounter.Commands.Base;

namespace MipsCounter.Formats
{
    class DataFormater
    {
        public DataBase GetCmd(string instruction, string lbl)
        {
            var dataMatch = Regex.Match(instruction, ".[a-z]+");
         
            string args = instruction.Remove(0, dataMatch.Length + dataMatch.Index);
            args = args.Trim();
            args = args.Trim('\t');

            return new DataBase(lbl, dataMatch.Value, args);
        }
    }
}
