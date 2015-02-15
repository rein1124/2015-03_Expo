using System;

namespace Hdc.Mercury
{
    public class Int32BooleanConverter : DataConverter<Int32, Boolean>
    {
        #region Implementation of IDataConverter<int,bool>

        public override int Convert(bool t2)
        {
            return t2 ? 1 : 0;
        }

        public override bool Convert(int t1)
        {
            return t1 == 0 ? false : true;
        }

        #endregion
    }
}
