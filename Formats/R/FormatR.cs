using MipsCounter.Commands;
using MipsCounter.Commands.Base;
using MipsCounter.Commands.Instructions;
using MipsCounter.Commands.Instructions.R;

namespace MipsCounter.Formats.R
{
    class FormatR : ICmdFormatter
    {
        public CmdBase GetCmd(string instruction, CmdInfo info, string label)
        {
            //specials
            switch (info.name)
            {
                case "sll":
                case "srl":
                case "sra":
                    return new FormatShift().GetCmd(instruction, info, label);
            }


            //generic
            //add $t7,$t3,$s0
            var split = instruction.Split(' ');
            var regSplit = split[1].Split(',');
            int rd = Register.Translate(regSplit[0]);
            int rs = Register.Translate(regSplit[1]);
            int rt = Register.Translate(regSplit[2]);

            CmdR cmd;

            switch (info.name)
            {
                case "add":
                case "sub":
                    cmd = new CmdAddR(info, (byte)rs, (byte)rt, (byte)rd, 0, info.funct, label);
                    break;
                case "slt":
                    cmd = new CmdSlt(info, (byte)rs, (byte)rt, (byte)rd, 0, info.funct, label);
                    break;
                default:
                    cmd = new CmdR(info, (byte)rs, (byte)rt, (byte)rd, 0, info.funct, label);
                    break;
            }
            return cmd;
        }
    }
}
