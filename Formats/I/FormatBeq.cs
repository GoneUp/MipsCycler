using MipsCounter.Commands;
using MipsCounter.Commands.Base;
using MipsCounter.Commands.Instructions;

namespace MipsCounter.Formats.I
{
    class FormatBeq : ICmdFormatter
    {
        public CmdBase GetCmd(string instruction, CmdInfo info, string label)
        {

            //generic
            //addi $s1,$s1,4
            //lw $t2,0($s2)
            //bne $t3,$zero,nimm2 
            var split = instruction.Split(' ');
            var regSplit = split[1].Split(',');
            int rd = Register.Translate(regSplit[0]);
            int rs = Register.Translate(regSplit[1]);
            string jumpLabel = regSplit[2];

            CmdI cmd = new CmdBranch(info, (byte)rs, (byte)rd, jumpLabel, label);
            return cmd;
        }
    }
}
