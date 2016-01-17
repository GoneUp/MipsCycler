using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Commands.Base;
using MipsCounter.Execution;

namespace MipsCounter.Commands.Instructions
{
    class CmdBubble : CmdBase
    {
        public CmdBubble(string label)
            : base(new CmdInfo("", "", 0, 0, 0), label)
        {
        }

        public override string ToString()
        {
            return String.Format("CmdBubble");
        }

        public override void execute(MipsMemory memory, MipsRegisters regs, Dictionary<string, int> linkerTable, Dictionary<string, int> dataTable)
        {}

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
