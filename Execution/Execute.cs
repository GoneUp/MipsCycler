using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Commands.Base;
using MipsCounter.Commands.Instructions;

namespace MipsCounter.Execution
{
    class Execute
    {
        public static void Exec(List<CmdBase> instructions, List<DataBase> dataCmds, bool perCycleOutput, bool forwarding)
        {
            //Pipline Stages: IF, ID, EX, MEM, WB
            /*
             * Ansatz:
             * Stack der die fünf Stages darstellen soll. solange ausführen bis initale Liste leer is, runden zurückgeben
             */
            List<CmdBase> history = new List<CmdBase>();
            MipsMemory mem = new MipsMemory((int)Math.Pow(2,16));
            MipsRegisters registers = new MipsRegisters();
            Dictionary<String, int> linkTable = CreateLinkTable(instructions);
            Dictionary<String, int> dataTable = new Dictionary<string, int>();

            foreach (var dataCmd in dataCmds)
            {
                dataCmd.execute(mem, dataTable);
            }


            int cycle = 0;
            registers.PC = 0;          
            
            while (true)
            {           
                //One time: Linker? Prepare Memory?
                //Load next Operation
                //Check Conditions (Hazard, Stalling)
                //Execute (Operations, load data, etc...) --> next pc

                if (registers.PC == instructions.Count) 
                    break; //end of program

                cycle++;

                var nextCmd = instructions[registers.PC];

                //Data Hazards, let our cmd WAIT if we need from a cmd before
                var usedRegs = new List<byte>();
                if (history.Count > 0)
                {
                    usedRegs.AddRange(history[history.Count - 1].getDstRegisters());
                    if (history.Count > 1)
                    {
                        usedRegs.AddRange(history[history.Count - 2].getDstRegisters());
                    }
                }
                var actualRegs = nextCmd.getSourceRegisters();

                bool match = false;
                actualRegs.ForEach(ourReg => usedRegs.ForEach(prevReg => { if (ourReg == prevReg) match = true; }));

                if (match && !(forwarding))
                {
                    history.Add(new CmdBubble(""));
                    continue; //hazard found
                }

                int initalPC = registers.PC;
                nextCmd.execute(mem, registers, linkTable, dataTable); 
                history.Add(nextCmd);

                if (registers.PC == initalPC) registers.PC += 1; //normal operations, only branch and jump modify it in the execute cmd
                if (perCycleOutput) Console.WriteLine("{0}: Execution: {1}, NextPC {2}", initalPC, nextCmd, registers.PC);
            }


            Console.WriteLine("=======================");
            Console.WriteLine("Cycle count: " + cycle);
        }

        private static Dictionary<String, int> CreateLinkTable(List<CmdBase> instructions)
        {
            Dictionary<String, int> linkTable = new Dictionary<string, int>();

            for (int i = 0; i < instructions.Count; i++)
            {
                if (instructions[i].label != "" && instructions[i].label != null)
                {
                    if (linkTable.ContainsKey(instructions[i].label))
                    {
                        Console.WriteLine("LINK DOUBLE " + instructions[i]);
                        continue;
                    }
                    linkTable.Add(instructions[i].label, i);
                }
                    
            }
            return linkTable;
        } 

    }
}
