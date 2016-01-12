using MipsCounter.Commands;
using MipsCounter.Commands.Base;
using MipsCounter.Commands.Instructions;

namespace MipsCounter.Formats.R
{
    class FormatR : ICmdFormatter
    {
        public CmdBase GetCmd(string instruction, CmdInfo info)
        {
            //specials
            switch (info.name)
            {
                case "sll":
                case "srl":
                case "sra":
                    return new FormatShift().GetCmd(instruction, info);
            }


            //generic
            //add $t7,$t3,$s0
            var split = instruction.Split(' ');
            var regSplit = split[1].Split(',');
            int rd = Register.Translate(regSplit[0]);
            int rs = Register.Translate(regSplit[1]);
            int rt = Register.Translate(regSplit[2]);

            CmdR cmd = new CmdR(info.opcode, (byte)rs, (byte)rt, (byte)rd, 0, info.funct);
            return cmd;
        }
    }
}
