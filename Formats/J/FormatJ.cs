using MipsCounter.Commands;
using MipsCounter.Commands.Base;
using MipsCounter.Commands.Instructions;

namespace MipsCounter.Formats.J
{
    class FormatJ : ICmdFormatter
    {
        public CmdBase GetCmd(string instruction, CmdInfo info)
        {
            var split = instruction.Split(' ');
            CmdJ cmd = new CmdJ(info.opcode, 0);
            return cmd;
        }
    }
}
