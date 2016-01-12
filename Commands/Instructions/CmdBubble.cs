using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Commands.Base;

namespace MipsCounter.Commands.Instructions
{
    class CmdBubble : CmdBase
    {
        public CmdBubble() : base(0)
        {
        }

        public override string ToString()
        {
            return String.Format("CmdBubble");
        }

        public override void execute()
        {
            throw new NotImplementedException();
        }

        public override List<byte> getSourceRegisters()
        {
            return new List<byte>();
        }
    }
}
