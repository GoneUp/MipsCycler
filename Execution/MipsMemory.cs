using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MipsCounter.Execution
{
    class MipsMemory
    {
        private byte[] memory;
        private int pointer = 0;
        private BinaryWriter writer;

        public MipsMemory(int size)
        {
            memory = new byte[size];
            writer = new BinaryWriter(new MemoryStream(memory));
        }

        public int CmdLoadWord(int address)
        {
            if (address < 0 || address + 4 > memory.Length)
                throw new IndexOutOfRangeException();
            return BitConverter.ToInt32(memory, address);
        }
        public void CmdSaveWord(int address, int value)
        {
            if (address < 0 || address + 4 > memory.Length)
                throw new IndexOutOfRangeException();
            writer.Seek(address, SeekOrigin.Begin);
            writer.Write(value);
        }
        public int SaveWords(int[] words)
        {
            int usedBytes = words.Length*4;
            if (usedBytes + pointer > memory.Length)
                throw new IndexOutOfRangeException();

            writer.Seek(pointer, SeekOrigin.Begin);
            foreach (var word in words)
                writer.Write(word);
            pointer += usedBytes;

            return pointer - usedBytes;
        }

        public int SaveHalfs(short[] halfs)
        {
            int usedBytes = halfs.Length * 4;
            if (usedBytes + pointer > memory.Length)
                throw new IndexOutOfRangeException();

            writer.Seek(pointer, SeekOrigin.Begin);
            foreach (var word in halfs)
                writer.Write(word);
            pointer += usedBytes;

            return pointer - usedBytes;
        }

        public int SaveBytes(byte[] values)
        {
            int usedBytes = values.Length * 1;
            if (usedBytes + pointer > memory.Length)
                throw new IndexOutOfRangeException();

            writer.Seek(pointer, SeekOrigin.Begin);
            writer.Write(values);
            pointer += usedBytes;

            return pointer - usedBytes;
        }
        public int ReserveBytes(int count)
        {
            int usedBytes = count;
            if (usedBytes + pointer > memory.Length)
                throw new IndexOutOfRangeException();

            writer.Seek(pointer, SeekOrigin.Begin);
            writer.Write(new byte[count]);
            pointer += usedBytes;

            return pointer - usedBytes;
        }
    }
}
