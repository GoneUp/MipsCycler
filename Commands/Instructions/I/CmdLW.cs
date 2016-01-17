using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Execution;

namespace MipsCounter.Commands.Instructions.I
{
    class CmdLW : CmdI
    {
        private string pseudoLabel = "";
        public CmdLW(CmdInfo cmdInfo, byte rs, byte rt, short im, string pseudoLabel, string lineLabel)
            : base(cmdInfo, rs, rt, 0, lineLabel)
        {
            this.pseudoLabel = pseudoLabel;
        }

        public override void execute(MipsMemory memory, MipsRegisters regs, Dictionary<string, int> linkerTable, Dictionary<string, int> dataTable)
        {
            if (info.name == "la")
            {
                if (!dataTable.ContainsKey(pseudoLabel))
                    throw new ArgumentException("label not found! " + ToString());

                regs.Registers[rt] = dataTable[pseudoLabel];

            } 
            else if (info.name == "lw")
            {
                if (pseudoLabel != "")
                {
                    if (!dataTable.ContainsKey(pseudoLabel))
                        throw new ArgumentException("label not found! " + ToString());

                    regs.Registers[rt] = memory.CmdLoadWord(dataTable[pseudoLabel]);
                }
                else
                {
                     regs.Registers[rt] = memory.CmdLoadWord(rs + immediate);
                }
               
            }
            else if (info.name == "sw")
            {
                memory.CmdSaveWord(rs + immediate, rt);
            }
           
           
        }
    }
}
