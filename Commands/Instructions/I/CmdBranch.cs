using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Execution;

namespace MipsCounter.Commands.Instructions
{
    class CmdBranch : CmdI
    {
        public string bLabel;
        public CmdBranch(CmdInfo cmdInfo, byte rs, byte rt, string branchBLabel, string lineLabel)
            : base(cmdInfo, rs, rt, 0, lineLabel)
        {
            this.bLabel = branchBLabel;
        }

        public override string ToString()
        {
            return String.Format("CmdBranch lbl {0}; {1}", bLabel, base.ToString());
        }

        public override void execute(MipsMemory memory, MipsRegisters regs, Dictionary<string, int> linkerTable, Dictionary<string, int> dataTable)
        {
            if (!linkerTable.ContainsKey(bLabel))
                throw new ArgumentException("branch label not found! " + ToString());

            if (info.name == "beq" && regs.Registers[rs] == regs.Registers[rt])
                regs.PC = linkerTable[bLabel];

            if (info.name == "bne" && regs.Registers[rs] != regs.Registers[rt])
                regs.PC = linkerTable[bLabel];
        }
    }
}
