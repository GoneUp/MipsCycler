using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Commands;
using MipsCounter.Commands.Base;

namespace MipsCounter.Formats
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
