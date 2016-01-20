using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MipsCounter.Commands
{
    public class CmdInfo
    {
        public string name, meaning;
        public CmdType type;
        public byte opcode, funct;

        public string inputLine;

        public CmdInfo(string name, string meaning, CmdType type, byte opcode, byte funct, string inputLine)
        {
            this.name = name;
            this.meaning = meaning;
            this.type = type;
            this.opcode = opcode;
            this.funct = funct;
            this.inputLine = inputLine;
        }

        public CmdInfo Clone()
        {
            return new CmdInfo(name, meaning, type, opcode, funct, inputLine);
        }

        public override string ToString()
        {
            return String.Format("FormatInfo Name:{0}, Typ {1}", name, type);
        }
    }

}
