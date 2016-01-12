using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MipsCounter.Commands.Base
{
    abstract class CmdBase
    {
        public readonly byte opcode;

        public CmdBase(byte opc)
        {
            opcode = opc;
        }

     

        public override string ToString()
        {
            return "Opcode: " + opcode;
        }

        public abstract void execute();
        public abstract List<byte> getSourceRegisters();
    }
}
