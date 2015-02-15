using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hdc.Mercury
{
    public class Int32DoubleConverter : DataConverter<Int32, double>
    {
        public override int Convert(double t2)
        {
            return (int) t2;
        }

        public override double Convert(int t1)
        {
            return t1;
        }
    }
}
