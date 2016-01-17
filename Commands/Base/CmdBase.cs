using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Execution;

namespace MipsCounter.Commands.Base
{
    abstract class CmdBase
    {
        public readonly string label;
        public CmdInfo info;

        public CmdBase(CmdInfo cmdInfo, string lineLabel)
        {
            info = cmdInfo;
            label = lineLabel;
        }

     

        public override string ToString()
        {
            return "Opcode: " + info.name;
        }

        public abstract void execute(MipsMemory memory, MipsRegisters regs, Dictionary<string, int> linkerTable, Dictionary<string, int> dataTable);
        public abstract List<byte> getSourceRegisters();
        public abstract List<byte> getDstRegisters();
    }
}
