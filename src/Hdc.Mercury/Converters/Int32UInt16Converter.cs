using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hdc.Mercury
{
    public class Int32UInt16Converter : DataConverter<Int32, UInt16>
    {
        public override int Convert(ushort t2)
        {
            return t2;
        }

        public override ushort Convert(int t1)
        {
            return (ushort)t1;
        }
    }
}
