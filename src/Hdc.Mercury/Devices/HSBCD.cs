using System;

namespace Hdc.Mercury
{
    public class HSBCD
    {
        public short Value { get; set; }

        public static implicit operator HSBCD(short a)
        {
            return new HSBCD(a);
        }

        public HSBCD()
        {
        }

        public HSBCD(short value)
        {
            Value = value;
        }
    }
}