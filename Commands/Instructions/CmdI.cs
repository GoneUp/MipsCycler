using System;
using System.Collections.Generic;
using MipsCounter.Commands.Base;
using MipsCounter.Execution;

namespace MipsCounter.Commands.Instructions
{
    class CmdI : CmdBase
    {
        public readonly byte rs, rt;
        public readonly short immediate;

        public CmdI(CmdInfo cmdInfo, byte rs, byte rt, short im, string lineLabel)
            : base(cmdInfo, lineLabel)
        {
            this.rs = rs;
            this.rt = rt;
            this.immediate = im;
        }

        public override string ToString()
        {
            return String.Format("CmdI rs {0}, rd {1}, immediate {2}; {3}", rs, rt, immediate, base.ToString());
        }

        public override void execute(MipsMemory memory, MipsRegisters regs, Dictionary<string, int> linkerTable, Dictionary<string, int> dataTable)
        {
            throw new NotImplementedException();
        }

        public override List<byte> getSourceRegisters()
        {
            return new List<byte>(new Byte[] { rt });
        }

        public override List<byte> getDstRegisters()
        {
            return new List<byte>(new Byte[] { rt });
        }
    }
}
