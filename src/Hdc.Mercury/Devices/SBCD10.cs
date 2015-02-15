namespace Hdc.Mercury
{
    public class SBCD10
    {
        public short Value { get; set; }

        public static implicit operator SBCD10(short a)
        {
            return new SBCD10(a);
        }

        public SBCD10()
        {
        }

        public SBCD10(short value)
        {
            Value = value;
        }
    }
}