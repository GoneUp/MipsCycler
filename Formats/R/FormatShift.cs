using System;
using MipsCounter.Commands;
using MipsCounter.Commands.Base;
using MipsCounter.Commands.Instructions;
using MipsCounter.Commands.Instructions.R;

namespace MipsCounter.Formats.R
{
    class FormatShift : ICmdFormatter
    {
        public CmdBase GetCmd(string instruction, CmdInfo info, string label)
        {
            //add $t7,$t3,$s0
            var split = instruction.Split(' ');
            var regSplit = split[1].Split(',');
            int rd = Register.Translate(regSplit[0]);
            int rt = Register.Translate(regSplit[1]);
            int shift = Convert.ToInt32(regSplit[2]);

            CmdR cmd = new CmdShift(info, 0, (byte)rt, (byte)rd, (byte)shift, info.funct, label);
            return cmd;
        }
    }
}
