using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Commands;
using MipsCounter.Commands.Base;

namespace MipsCounter.Formats
{
    class FormatR : ICmdFormatter
    {
        public CmdBase GetCmd(string instruction, CommandList.FormatInfo info)
        {
            //add $t7,$t3,$s0
            var split = instruction.Split(' ');
            string name = split[0];
            var regSplit = split[1].Split(',');
            int rd = Register.Translate(regSplit[0]);
            int rs = Register.Translate(regSplit[0]);
            int rt = Register.Translate(regSplit[0]);

            CmdR cmd = new CmdR(info.opcode, (byte)rs, (byte)rt, (byte)rd, 0, info.funct);
            return cmd;
        }
    }
}
