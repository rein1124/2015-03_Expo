using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBCD10 = System.Int16;

namespace Hdc.Mercury
{
    public class SBCD10Int32Converter : DataConverter<short, Int32>
    {
        public override int Convert(short t1)
        {
            return SBCDHelper.ConvertFrom(t1, 15, 10);
        }

        public override short Convert(int t2)
        {
            return SBCDHelper.ConvertTo(t2, 15);
        }
    }

    //public struct SBCD10
    //{
    //    public int Value { get; set; }
    //}
}
