using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MipsCounter.Formats
{
    class Register
    {
        public static int Translate(string name)
        {
            if (!name.StartsWith("$")) throw new ArgumentException("register invalid " + name);

            name = name.Substring(1); //remove $
           
            

            if (name == "zero") return 0;
            if (name == "at") return 1;
            if (name == "gp") return 28;
            if (name == "sp") return 29;
            if (name == "fp") return 30;
            if (name == "ra") return 31;

            string type = name.Substring(0, 1);
            int num = Convert.ToByte(name.Substring(1, 1));
            if (type == "v") return 2 + num;
            if (type == "a") return 4 + num;
            if (type == "t" && num <= 7) return 8 + num;
            if (type == "s") return 16 + num;
            if (type == "t" && num > 7) return 24 + num;
            if (type == "k") return 26 + num;

            throw new ArgumentException("register not mapped " + name);
        }
    }
}
