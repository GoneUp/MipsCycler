using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Execution;

namespace MipsCounter.Commands.Instructions.R
{
    class CmdSlt : CmdR
    {
        public CmdSlt(CmdInfo cmdInfo, byte rs, byte rt, byte rd, byte shift, byte funct, string lineLabel)
            : base(cmdInfo, rs, rt, rd, shift, funct, lineLabel)
        {
        }

        public override void execute(MipsMemory memory, MipsRegisters regs, Dictionary<string, int> linkerTable, Dictionary<string, int> dataTable)
        {
            switch (info.name)
            {
                case "slt":
                    regs.Registers[rd] = regs.Registers[rs] < regs.Registers[rt] ? 1 : 0;
                    break;

            }
        }
    }
}
