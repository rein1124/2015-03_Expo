using System;
using HSBCD = System.Int16;

namespace Hdc.Mercury
{
    public class HSBCDInt32Converter : DataConverter<short, Int32>
    {
        public override int Convert(short t1)
        {
            return SBCDHelper.ConvertFrom(t1, 7, 7);
        }

        public override short Convert(int t2)
        {
            return SBCDHelper.ConvertTo(t2, 7);
        }
    }
}
