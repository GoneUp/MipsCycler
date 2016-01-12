using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Commands;

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
                Console.WriteLine("Usage: use the path to an asm/mips code file as argument");
            }

            try
            {
                CommandList.Init();
                var path = args[0];
                var lines = File.ReadAllLines(path);

                var cmds = ProgramParser.Translate(lines);
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
