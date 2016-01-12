using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MipsCounter.Commands.Base;
using MipsCounter.Commands.Instructions;

namespace MipsCounter.Logic
{
    class Counter
    {
        public static void Count(Stack<CmdBase> instructions, bool forwarding, int hazardBubbels, int jumpBubbels, int branchBubbels, bool perCycleOutput)
        {
            //Pipline Stages: IF, ID, EX, MEM, WB
            /*
             * Ansatz:
             * Stack der die fünf Stages darstellen soll. solange ausführen bis initale Liste leer is, runden zurückgeben
             */
            Queue<CmdBase> stages = new Queue<CmdBase>(); 
           

            int cycle = 0;
            while (instructions.Count != 0 || stages.Count > 0)
            {
                cycle++;

                if (stages.Count > 5 || instructions.Count == 0) 
                    stages.Dequeue(); //remove last wb stage
                if (stages.Count > 5) 
                    continue; //bubbels

                if (perCycleOutput) printStages(stages, cycle);
                if (instructions.Count == 0) continue;

                CmdBase nextCmd = instructions.Pop();
                if (nextCmd is CmdBranch)
                {
                    //branch penalty
                    for (int i = 0; i < branchBubbels; i++)
                    {
                        stages.Enqueue(new CmdBubble());
                    }
                } 
                else if (nextCmd is CmdJ)
                {
                    //Jump penealty
                    for (int i = 0; i < jumpBubbels; i++)
                    {
                        stages.Enqueue(new CmdBubble());
                    }
                }

                //Data Hazards
                var array = stages.ToArray();
                var usedRegs = new List<byte>();
                if (stages.Count > 0) usedRegs = array[stages.Count - 1].getSourceRegisters();
                var actualRegs = nextCmd.getSourceRegisters();

                bool match = false;
                actualRegs.ForEach(ourReg => usedRegs.ForEach(prevReg => { if (ourReg == prevReg) match = true; }));

                if (match && !(forwarding))
                {
                    //hazard dete
                    for (int i = 0; i < hazardBubbels; i++)
                    {
                        stages.Enqueue(new CmdBubble());
                    }
                }

                stages.Enqueue(nextCmd);
            }


            Console.WriteLine("=======================");
            Console.WriteLine("Cycle count: " + cycle);
        }

        private static void printStages(Queue<CmdBase> stages, int cycle)
        {
            var array = stages.ToArray().Reverse();
            Console.WriteLine("=======================");
            Console.WriteLine("Cycle: " + cycle);
            foreach (var cmdBase in array)
            {
                Console.WriteLine(cmdBase.ToString());
            }
        }
    }
}
