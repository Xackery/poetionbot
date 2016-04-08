using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poetionbot
{
    class pointerset
    {
        public string moduleName;
        public int baseOffset;

        //Base address is derived from moduleName+baseOffset
        public int baseAddress;
        public int[] offsets;
    }
}
