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
        private static List<CmdInfo> cmdList  = new List<CmdInfo>();

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

                    if (split[4] == "NA") split[4] = "0xFF";
                    cmdList.Add(new CmdInfo(split[0], split[1], typ, Convert.ToByte(split[3], 16), Convert.ToByte(split[4], 16), ""));
                }
            }
        }

        public static CmdInfo GetInfo(string name)
        {
            var obj = cmdList.FirstOrDefault(element => element.name == name);
            if (obj == null) return null;
            return obj.Clone();
        }

        public static CmdInfo GetInfo(int opcode)
        {
            var obj = cmdList.FirstOrDefault(element => element.opcode == opcode);
            if (obj == null) return null;
            return obj.Clone();
        }
    }
}
