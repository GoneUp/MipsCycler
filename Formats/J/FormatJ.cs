using MipsCounter.Commands;
using MipsCounter.Commands.Base;
using MipsCounter.Commands.Instructions;

namespace MipsCounter.Formats.J
{
    class FormatJ : ICmdFormatter
    {
        public CmdBase GetCmd(string instruction, CmdInfo info, string label)
        {
            var split = instruction.Split(' ');
            CmdJ cmd = new CmdJ(info, split[1], label);
            return cmd;
        }
    }
}
