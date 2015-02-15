namespace Hdc.Mercury.Communication
{
    public static class WriteDataExtensions
    {
//        public static AccessItemIdentifier GetIdentifier(this WriteData data)
//        {
//            return data.Registration.Identifier;
//        }

        public static dynamic GetConvertBackValue(this WriteData writeData)
        {
            return writeData.Value.ConvertBack(writeData.Registration.Config);
        }

    }
}