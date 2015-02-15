namespace Hdc.Mercury
{
    public class Int32ByteConverter : DataConverter<int, byte>
    {
        public override int Convert(byte t2)
        {
            return t2;
        }

        public override byte Convert(int t1)
        {
            return (byte)t1;
        }
    }
}