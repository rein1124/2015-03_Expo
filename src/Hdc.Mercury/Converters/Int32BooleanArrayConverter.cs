using System;
using System.Collections.Generic;
using Hdc.Collections.Generic;

namespace Hdc.Mercury
{
    public class Int32BooleanArrayConverter : DataConverter<Int32, IEnumerable<Boolean>>
    {
        #region Implementation of IDataConverter<int,IEnumerable<bool>>

        public override int Convert(IEnumerable<bool> t2)
        {
            return t2.ToInt32();
        }

        public override IEnumerable<bool> Convert(int t1)
        {
            return t1.ToBooleans();
        }

        #endregion
    }
}