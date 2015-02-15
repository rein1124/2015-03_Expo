using System;
using System.Linq;

namespace Hdc.Mercury.Converters
{
    public class Int16ArrayToInt32ArrayConverter : DataConverter<Int16[], Int32[]>
    {
        public override short[] Convert(int[] t2)
        {
            return t2.Select(System.Convert.ToInt16).ToArray();
        }

        public override int[] Convert(short[] t1)
        {
            return t1.Select(System.Convert.ToInt32).ToArray();
        }
    }
}