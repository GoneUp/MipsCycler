using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MipsCounter.Commands;
using MipsCounter.Commands.Base;
using MipsCounter.Formats;

namespace MipsCounter
{
    class ProgramParser
    {
        public List<CmdBase> Translate(string[] lines)
        {
            List<CmdBase> list = new List<CmdBase>();
            bool inData = false;

            foreach (string instruction in lines)
            {
                var line = instruction;
                //asm file 
                if (line.StartsWith(".data"))
                {
                    inData = true;
                    continue;
                } 
                if (line.StartsWith(".text"))
                {
                    inData = false;
                    continue;
                }

                if (inData) continue;

                //regex for labels
                if (Regex.IsMatch(line, ".+:\\s.+"))
                {
                    //next:	addi $s0,$s0,4
                    while (!line.StartsWith(":"))
                    {
                        line = line.Substring(1);
                    }
                    line = line.Substring(1);
                    line = line.Trim();
                }


                //parsing code
                var split = line.Split(' ');

                if (split.Length < 2) continue;
                var name = split[0];
                var info = CommandList.GetInfo(name);

                if (info == null)
                {
                    Console.WriteLine("Unkown cmd {0}", line);
                    continue;
                }

                ICmdFormatter formatter = null;
                switch (info.type)
                {
                    case CmdType.R:
                        formatter = new FormatR();
                        break;
                    default:
                        Console.WriteLine("Unkown cmd format {0}", info);
                        continue;
                }

                var cmd = formatter.GetCmd(line, info);
                list.Add(cmd);
            }
            return list;
        }
    }
}
