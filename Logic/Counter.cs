﻿using System;
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
            List<CmdBase> controlHazards = new List<CmdBase>();
            List<CmdBase> dataHazards = new List<CmdBase>();

            int cycle = 0;
            while (instructions.Count != 0 || stages.Count > 0)
            {       
                cycle++;
                if (perCycleOutput) printStages(stages, cycle - 1);

                if (stages.Count > 5 || instructions.Count == 0) 
                    stages.Dequeue(); //remove last wb stage
                if (stages.Count > 5) 
                    continue; //bubbels

                if (instructions.Count == 0) continue; //skip enqueue part

                CmdBase nextCmd = instructions.Pop();
               
                //Data Hazards, let our cmd WAIT if we need from a cmd before
                var array = stages.ToArray();
                var usedRegs = new List<byte>();
                if (array.Length > 0)
                {
                    usedRegs.AddRange(array[array.Length - 1].getDstRegisters());
                    if (array.Length > 1)
                    {
                        usedRegs.AddRange(array[array.Length - 2].getDstRegisters());
                    }
                }
                var actualRegs = nextCmd.getSourceRegisters();

                bool match = false;
                actualRegs.ForEach(ourReg => usedRegs.ForEach(prevReg => { if (ourReg == prevReg) match = true; }));

                if (match && !(forwarding))
                {
                    //hazard dete
                    for (int i = 0; i < hazardBubbels; i++)
                    {
                        stages.Enqueue(new CmdBubble(""));
                    }
                    dataHazards.Add(nextCmd);
                }

                stages.Enqueue(nextCmd);

                //let the next instruction AFTER our wait
                if (nextCmd is CmdBranch && instructions.Count > 0)
                {
                    //branch penalty
                    for (int i = 0; i < branchBubbels; i++)
                    {
                        stages.Enqueue(new CmdBubble(""));
                    }
                    controlHazards.Add(nextCmd);
                }
                else if (nextCmd is CmdJ && instructions.Count > 0)
                {
                    //Jump penealty
                    for (int i = 0; i < jumpBubbels; i++)
                    {
                        stages.Enqueue(new CmdBubble(""));
                    }
                    controlHazards.Add(nextCmd);
                }
            }


            Console.WriteLine("=======================");
            Console.WriteLine("Data Hazards: ");
            dataHazards.ForEach(e => Console.WriteLine("At: " + e));
            Console.WriteLine("Control Hazards: ");
            controlHazards.ForEach(e => Console.WriteLine("At: " + e));
            Console.WriteLine("=======================");
            Console.WriteLine("Cycle count: " + cycle);
        }

        private static void printStages(Queue<CmdBase> stages, int cycle)
        {
            CmdBase[] array = stages.ToArray();
            Console.WriteLine("=======================");
            Console.WriteLine("Cycle: " + cycle);
            foreach (CmdBase t in array)
            {
                Console.WriteLine(t);
            }
        }

        public static void PerTypeStats(List<CmdBase> cmds)
        {
            int cmdI = 0;
            int cmdR = 0;
            int cmdJ = 0;
            int cmdBranch = 0;
            foreach (var cmd in cmds)
            {
                if (cmd is CmdBranch) cmdBranch++;
                if (cmd is CmdI) cmdI++;
                if (cmd is CmdR) cmdR++;
                if (cmd is CmdJ) cmdJ++;
            }

            Console.WriteLine("=======================");
            Console.WriteLine("cmdI: {0}, cmdR: {1}, cmdJ: {2}, cmdBranch: {3}", cmdI, cmdR, cmdJ, cmdBranch);
        }
    }
}
