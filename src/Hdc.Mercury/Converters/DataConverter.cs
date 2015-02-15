using System;
using System.ComponentModel;

namespace Hdc.Mercury
{
    public class DataConverter<T1, T2> : IDataConverter<T1, T2>
    {
        public virtual T1 Convert(T2 t2)
        {
            dynamic o = t2;
            return o;
        }

        public virtual T2 Convert(T1 t1)
        {
            dynamic o = t1;
            return o;
        }
    }
}
