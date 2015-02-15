using System;
using System.Collections.Generic;
using Hdc.Collections.Generic;

namespace Hdc.Mercury
{
    public class Int16BooleanArrayConverter : DataConverter<Int16, IEnumerable<Boolean>>
    {
        #region Implementation of IDataConverter<short,IEnumerable<bool>>

        public override short Convert(IEnumerable<bool> t2)
        {
            return t2.ToInt16();
        }

        public override IEnumerable<bool> Convert(short t1)
        {
            return t1.ToBooleans();
        }

        #endregion
    }
}
