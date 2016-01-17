using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MipsCounter.Execution;

namespace MipsCounter.Commands.Base
{
    class DataBase
    {
        public string label;
        private string instruction;
        private string args;

        public DataBase(string lbl, string inst, string args)
        {
            label = lbl;
            this.instruction = inst;
            this.args = args;
        }


        public void execute(MipsMemory memory, Dictionary<string, int> dataTable)
        {
   
            string[] split = args.Split(' ');
            int pointer = 0;

            switch (instruction)
            {
                case ".word":
                    int[] intArray = new int[split.Length];
                    for (int i = 0; i < split.Length; i++)
                    {
                        intArray[i] = Convert.ToInt32(split[i]);
                    }
                    pointer = memory.SaveWords(intArray);
                    break;
                case ".half":
                    short[] halfArray = new short[split.Length];
                    for (int i = 0; i < split.Length; i++)
                    {
                        halfArray[i] = Convert.ToInt16(split[i]);
                    }
                    pointer = memory.SaveHalfs(halfArray);
                    break;

                case ".byte":
                    byte[] byteArray = new byte[split.Length];
                    for (int i = 0; i < split.Length; i++)
                    {
                        byteArray[i] = Convert.ToByte(split[i]);
                    }
                    pointer = memory.SaveBytes(byteArray);
                    break;

                case ".space":
                    int count = Convert.ToInt32(split[0]);
                    pointer = memory.ReserveBytes(count);
                    break;
            }


            if (label != "")
                dataTable.Add(label, pointer);
        }
    }
}
