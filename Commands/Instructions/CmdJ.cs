using System;
using System.Collections.Generic;
using MipsCounter.Commands.Base;
using MipsCounter.Execution;

namespace MipsCounter.Commands.Instructions
{
    class CmdJ : CmdBase
    {
        public string jumpLabel;

        public CmdJ(CmdInfo info1, string jumpLabel, string lineLabel)
            : base(info1, lineLabel)
        {
            this.jumpLabel = jumpLabel;
        }

        public override string ToString()
        {
            return String.Format("CmdJ jump {0}; {1}", jumpLabel, base.ToString());
        }


        public override void execute(MipsMemory memory, MipsRegisters regs, Dictionary<string, int> linkerTable, Dictionary<string, int> dataTable)
        {
            if (!linkerTable.ContainsKey(jumpLabel)) 
                throw new ArgumentException("jump label not found! " + ToString());

            regs.PC = linkerTable[jumpLabel];
        }

        public override List<byte> getSourceRegisters()
        {
            return new List<byte>();
        }

        public override List<byte> getDstRegisters()
        {
            return new List<byte>();
        }
    }
}
