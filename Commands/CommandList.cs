using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MipsCounter.Commands
{
    class CommandList
    {
        //Name --> FormatInfo

        public class FormatInfo
        {
            public string name, meaning;
            public CmdType type;
            public byte opcode, funct;

            public FormatInfo(string name, string meaning, CmdType type, byte opcode, byte funct)
            {
                this.name = name;
                this.meaning = meaning;
                this.type = type;
                this.opcode = opcode;
                this.funct = funct;
            }
        }


        private static List<FormatInfo> cmdList  = new List<FormatInfo>();

        public static void Init()
        {
            string[] lines = File.ReadAllLines("list.csv");
            foreach (var line in lines)
            {
                string[] split = line.Split(';');
                if (split.Count() == 5)
                {
                    CmdType typ;

                    if (split[2] == "R") {
                        typ = CmdType.R;
                    } else if (split[2] == "I")  {
                        typ = CmdType.I;
                    } else if (split[2] == "J") {
                        typ = CmdType.J;
                    } else {
                        throw new Exception("WTF FORMAT");
                    }


                    cmdList.Add(new FormatInfo(split[0], split[1], typ, Convert.ToByte(split[3]), Convert.ToByte(split[4])));
                }
            }
        }

        public static FormatInfo GetInfo(string name)
        {
            return cmdList.FirstOrDefault(element => element.name == name);
        }
    }
}
