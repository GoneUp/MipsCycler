using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MipsCounter.Commands.Instructions
{
    class CmdBranch : CmdI
    {
        public string label;
        public CmdBranch(byte opc, byte rs, byte rt, string label) : base(opc, rs, rt, 0)
        {
            this.label = label;
        }

        public override string ToString()
        {
            return String.Format("CmdBranch lbl {0}; {1}", label, base.ToString());
        }
    }
}
