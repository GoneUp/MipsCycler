using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MipsCounter.Commands.Base
{
    class CmdI : CmdBase
    {
        public readonly byte rs, rd;
        public readonly short immediate;

        public CmdI(byte opc, byte rs, byte rd, short im) : base(opc)
        {
            this.rs = rs;
            this.rd = rd;
            this.immediate = im;
        }

        public override string ToString()
        {
            return String.Format("CmdI rs {0}, rd {1}, immediate {2}; {3}", rs, rd, immediate, base.ToString());
        }

        public override void execute()
        {
            throw new NotImplementedException();
        }
    }
}
