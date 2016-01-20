using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MipsCounter.Commands;
using MipsCounter.Commands.Base;
using MipsCounter.Commands.Instructions;
using MipsCounter.Formats;
using MipsCounter.Formats.I;
using MipsCounter.Formats.J;
using MipsCounter.Formats.R;

namespace MipsCounter.Logic
{
    class ProgramParser
    {
        public static List<CmdBase> cmds;
        public static List<DataBase> dataCmds;

        public static void Translate(string[] lines)
        {
            cmds = new List<CmdBase>();
            dataCmds = new List<DataBase>();
            bool inData = false;

            foreach (string instruction in lines)
            {
                //Console.WriteLine(instruction);
                var line = instruction;
                String lineLabel = "";
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
                    int index = line.IndexOf(':');
                    lineLabel = line.Substring(0, index);
                    line = line.Remove(0, index + 1);
                }


                if (inData)
                {
                    var dataMatch = Regex.Match(line, ".[a-z]+");
                    if (!dataMatch.Success) continue;
                    dataCmds.Add(new DataFormater().GetCmd(line, lineLabel));
                    continue;
                }

                line = line.Trim();
                var nameMatch = Regex.Match(line, "[a-z]+");
                if (!nameMatch.Success)
                {
                    if (lineLabel != "")
                        cmds.Add(new CmdBubble(lineLabel));
                    continue;
                }

                var info = CommandList.GetInfo(nameMatch.Value);
                if (info == null)
                {
                    Console.WriteLine("Unkown cmd {0}", line);
                    continue;
                }

                info.inputLine = instruction;
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

                var cmd = formatter.GetCmd(line, info, lineLabel);
                Console.WriteLine("Parsed! {0}", cmd);
                cmds.Add(cmd);
            }
        }
    }
}
