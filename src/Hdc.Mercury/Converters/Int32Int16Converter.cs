using System;

namespace Hdc.Mercury
{
    public class Int32Int16Converter : DataConverter<Int32, Int16>
    {
        #region Implementation of IDataConverter<int,short>

        public override int Convert(short t2)
        {
            return t2;
        }

        public override short Convert(int t1)
        {
            return (short) t1;
        }

        #endregion
    }
}