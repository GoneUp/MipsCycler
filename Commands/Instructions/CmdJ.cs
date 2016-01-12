﻿using System;
using System.Collections.Generic;
using MipsCounter.Commands.Base;

namespace MipsCounter.Commands.Instructions
{
    class CmdJ : CmdBase
    {
        public readonly int jumpAdress;
       
        public CmdJ(byte opc, int address) : base(opc)
        {
            jumpAdress = address;
        }

        public override string ToString()
        {
            return String.Format("CmdJ jump {0}; {1}", jumpAdress, base.ToString());
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
