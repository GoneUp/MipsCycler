using System;
using System.Collections.Generic;
using MipsCounter.Commands.Base;

namespace MipsCounter.Commands.Instructions
{
    class CmdI : CmdBase
    {
        public readonly byte rs, rt;
        public readonly short immediate;

        public CmdI(byte opc, byte rs, byte rt, short im) : base(opc)
        {
            this.rs = rs;
            this.rt = rt;
            this.immediate = im;
        }

        public override string ToString()
        {
            return String.Format("CmdI rs {0}, rd {1}, immediate {2}; {3}", rs, rt, immediate, base.ToString());
        }

        public override void execute()
        {
            throw new NotImplementedException();
        }

        public override List<byte> getSourceRegisters()
        {
            return new List<byte>(new Byte[] { rs, rt });
        }
    }
}
