using System;
using MipsCounter.Commands;
using MipsCounter.Commands.Base;
using MipsCounter.Commands.Instructions;
using MipsCounter.Commands.Instructions.I;

namespace MipsCounter.Formats.I
{
    class FormatLW : ICmdFormatter
    {
        public CmdBase GetCmd(string instruction, CmdInfo info, string label)
        {
            //lw $t2,0($s2)
            var split = instruction.Split(' ');
            var regSplit = split[1].Split(',');
            int rt = Register.Translate(regSplit[0]);

            if (instruction.Contains("("))
            {
                int posBOpen = 0;
                int posBClose = 0;
                var chars = regSplit[1].ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    if (chars[i] == '(') posBOpen = i;
                    if (chars[i] == ')') posBClose = i;
                }

                string num = regSplit[1].Substring(0, posBOpen);
                int value = Convert.ToInt32(num);
                string bracket = regSplit[1].Substring(posBOpen + 1, posBClose - (posBOpen + 1));
                int rs = Register.Translate(bracket);


                return new CmdLW(info, (byte)rs, (byte)rt, (short)value,"",  label);
            }

            //lw $s3, n		# n in $s3 laden 
            //pseudo instruct
            return new CmdLW(info, 0, (byte)rt, 0, split[2], label);
        }
    }
}
