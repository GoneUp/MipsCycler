using System;
using MipsCounter.Commands;
using MipsCounter.Commands.Base;
using MipsCounter.Commands.Instructions;
using MipsCounter.Commands.Instructions.R;

namespace MipsCounter.Formats.I
{
    class FormatI : ICmdFormatter
    {
        public CmdBase GetCmd(string instruction, CmdInfo info, string label)
        {
            //specials
            switch (info.name)
            {
                case "la":
                case "lw":
                case "sw":
                    return new FormatLW().GetCmd(instruction, info, label);
                case "beq":
                case "bne":
                    return new FormatBeq().GetCmd(instruction, info, label);
            };


            //generic
            //addi $s1,$s1,4
            //lw $t2,0($s2)
            //bne $t3,$zero,nimm2 
            var split = instruction.Split(' ');
            var regSplit = split[1].Split(',');
            int rd = Register.Translate(regSplit[0]);
            int rs = Register.Translate(regSplit[1]);
            int value = Convert.ToInt32(regSplit[2]);

            CmdI cmd;
            switch (info.name)
            {
                case "addi":
                    cmd = new CmdAddi(info, (byte)rs, (byte)rd, (short)value, label);
                    break;
                default:
                    cmd = new CmdI(info, (byte)rs, (byte)rd, (short)value, label);
                    break;
            }

            return cmd;
        }
    }
}
