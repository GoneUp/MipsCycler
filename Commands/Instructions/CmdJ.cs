using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MipsCounter.Commands.Base
{
    class CmdJ : CmdBase
    {
        public readonly int jumpAdress;
       
        public CmdJ(byte opc, int address) : base(opc)
        {
            jumpAdress = address;
        }

        public override void execute()
        {
            throw new NotImplementedException();
        }
    }
}
