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
        public static List<CmdBase> Translate(string[] lines)
        {
            List<CmdBase> list = new List<CmdBase>();
            bool inData = false;

            foreach (string instruction in lines)
            {
                //Console.WriteLine(instruction);
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

               
                //parsing code
                //comments 
                if (line.Contains("#"))
                {
                    int pos = line.Length - 1;
                    var chars = line.ToCharArray();
                    while (chars[pos] != '#')
                    {
                        pos --;
                    }
                    line = line.Substring(0, pos);
                }

                //regex for labels
                if (Regex.IsMatch(line, ".+:\\s.+"))
                {
                    //next:	addi $s0,$s0,4
                    while (!line.StartsWith(":"))
                    {
                        line = line.Substring(1);
                    }
                    line = line.Substring(1);
                }


                line = line.Trim();
                var nameMatch = Regex.Match(line, "[a-z]+");
                if (!nameMatch.Success) continue;
                var name = nameMatch.Value;


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
                    case CmdType.I:
                        formatter = new FormatI();
                        break;
                    case CmdType.J:
                        formatter = new FormatJ();
                        break;
                    default:
                        Console.WriteLine("Unkown cmd format {0}", info);
                        continue;
                }

                var cmd = formatter.GetCmd(line, info);
                Console.WriteLine("Parsed! {0}", cmd);
                list.Add(cmd);
            }
            return list;
        }
    }
}
