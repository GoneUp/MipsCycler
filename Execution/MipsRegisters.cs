using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MipsCounter.Execution
{
    class MipsRegisters
    {
        public int[] Registers;
        public int PC;

        public MipsRegisters()
        {
            Registers = new int[32];
        }

    }
}
