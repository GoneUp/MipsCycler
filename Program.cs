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
            Console.WriteLine("Henry Strobel ## github.com//GoneUp\n");

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: PATH");
                Console.WriteLine("Usage: PATH Forwarding(0/1) HazardBubbels JumpBubbels BranchBubbels perCycleOutput(0/1)");
                Console.WriteLine("Default: PATH 0 2 1 1 1");
                Console.Read();
                return;
            }

            try
            {
                CommandList.Init();
                var path = args[0];
                var lines = File.ReadAllLines(path);

                var cmds = ProgramParser.Translate(lines);
                cmds.Reverse();

                Console.WriteLine("Starting Counter");

                bool forwarding = false;
                int hazardBubbels = 2;
                int jumpBubbels = 1;
                int branchBubbels = 1;
                bool proCyOutput = true;
                if (args.Length == 6)
                {
                    forwarding = Convert.ToBoolean(args[1]);
                    hazardBubbels = Convert.ToInt32(args[2]);
                    jumpBubbels = Convert.ToInt32(args[3]);
                    branchBubbels = Convert.ToInt32(args[4]);
                    proCyOutput = Convert.ToBoolean(args[5]);
                }

                Counter.Count(new Stack<CmdBase>(cmds), forwarding, hazardBubbels, jumpBubbels, branchBubbels, proCyOutput);


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
