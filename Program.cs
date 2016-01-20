using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Commands;
using MipsCounter.Commands.Base;
using MipsCounter.Logic;

namespace MipsCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mips Cycle Count Parser");
            Console.WriteLine("Henry Strobel ## github.com/GoneUp\n");

            if (args.Length == 0)
            {
                Console.WriteLine("Usage for Counter Mode");
                Console.WriteLine("Usage: PATH");
                Console.WriteLine("Usage: PATH Forwarding(false/true) HazardBubbels JumpBubbels BranchBubbels perCycleOutput(false/true)");
                Console.WriteLine("Default: PATH 0 2 0 1 1");
                Console.WriteLine("Usage for Execution Mode");
                Console.WriteLine("Usage: PATH -EX perCycleOutput(false/true)");

                Console.Read();
                return;
            }

            try
            {
                CommandList.Init();
                var path = args[0];
                var lines = File.ReadAllLines(path, Encoding.Default);

                ProgramParser.Translate(lines);
                //ProgramParser.cmds.Reverse();



                bool forwarding = false;
                int hazardBubbels = 2;
                int jumpBubbels = 0;
                int branchBubbels = 3;
                bool proCyOutput = true;
                if (args.Length == 6)
                {
                    forwarding = Convert.ToBoolean(args[1]);
                    hazardBubbels = Convert.ToInt32(args[2]);
                    jumpBubbels = Convert.ToInt32(args[3]);
                    branchBubbels = Convert.ToInt32(args[4]);
                    proCyOutput = Convert.ToBoolean(args[5]);
                }


                if (args.Length == 3 && args[1] == "-EX")
                {
                    proCyOutput = Convert.ToBoolean(args[2]);

                    Console.WriteLine("Starting Exec");
                    Execution.Execute.Exec(ProgramParser.cmds, ProgramParser.dataCmds, proCyOutput, true);
                }
                else
                {
                    Counter.PerTypeStats(ProgramParser.cmds);
                    Console.WriteLine("Starting Counter");
                    Console.WriteLine("Settings: FWD {0}, HB {1}, JB {2}, BB {3}, DebugOutput {4}", 
                        forwarding, hazardBubbels, jumpBubbels, branchBubbels, proCyOutput);
                    ProgramParser.cmds.Reverse();
                    Counter.Count(new Stack<CmdBase>(ProgramParser.cmds), 
                        forwarding, hazardBubbels, jumpBubbels, branchBubbels, proCyOutput);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Program stopped");
            }

            Console.Read();
        }


    }
}
