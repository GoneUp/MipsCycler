using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Execution;

namespace MipsCounter.Commands.Instructions.R
{
    class CmdAddi: CmdI
    {
        public CmdAddi(CmdInfo cmdInfo, byte rs, byte rt, short im, string lineLabel) : base(cmdInfo, rs, rt, im, lineLabel)
        {
        }

        public override void execute(MipsMemory memory, MipsRegisters regs, Dictionary<string, int> linkerTable, Dictionary<string, int> dataTable)
        {
            switch (info.name)
            {
                case "addi":
                    regs.Registers[rt] = regs.Registers[rs] + immediate;
                    break;
            }
        }
    }
}
