namespace Hdc.Mercury.Communication
{
    public class WriteData
    {
        public IAccessItemRegistration Registration { get; set; }

        public uint ClientAlias
        {
            get { return Registration.ClientAlias; }
        }

        public dynamic Value { get; set; }

        public WriteData()
        {
        }

        public WriteData(IAccessItemRegistration registration)
        {
            Registration = registration;
        }

        public WriteData(IAccessItemRegistration registration, dynamic value)
        {
            Registration = registration;
            Value = value;
        }
    }
}