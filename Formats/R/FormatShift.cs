using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Commands;
using MipsCounter.Commands.Base;

namespace MipsCounter.Formats
{
    class FormatShift : ICmdFormatter
    {
        public CmdBase GetCmd(string instruction, CmdInfo info)
        {
            //add $t7,$t3,$s0
            var split = instruction.Split(' ');
            var regSplit = split[1].Split(',');
            int rd = Register.Translate(regSplit[0]);
            int rt = Register.Translate(regSplit[1]);
            int shift = Convert.ToInt32(regSplit[2]);

            CmdR cmd = new CmdR(info.opcode, 0, (byte)rt, (byte)rd, (byte)shift, info.funct);
            return cmd;
        }
    }
}
