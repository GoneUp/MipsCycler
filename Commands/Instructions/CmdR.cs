using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MipsCounter.Commands.Base
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

        public override void execute()
        {
            throw new NotImplementedException();
        }
    }
}
