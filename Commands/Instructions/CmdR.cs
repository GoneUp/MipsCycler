using System;
using System.Collections.Generic;
using MipsCounter.Commands.Base;

namespace MipsCounter.Commands.Instructions
{
    class CmdR : CmdBase
    {
        public readonly byte rs, rt, rd, shift, funct;

        public CmdR(byte opc, byte rs, byte rt, byte rd, byte shift, byte funct) : base(opc)
        {
            this.rs = rs;
            this.rt = rt;
            this.rd = rd;
            this.shift = shift;
            this.funct = funct;
        }
        public override string ToString()
        {
            return String.Format("CmdR rs {0}, rt {1}, rd {2}, shift {3}, shamt {4}; {5}", rs, rt, rd, shift, funct, base.ToString());
        }

        public override void execute()
        {
            throw new NotImplementedException();
        }

        public override List<byte> getSourceRegisters()
        {
            return new List<byte>(new Byte[]{rs, rt, rd});
        }
    }
}
