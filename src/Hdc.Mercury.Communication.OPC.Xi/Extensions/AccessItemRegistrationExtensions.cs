using Advosol.Paxi;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public static class AccessItemRegistrationExtensions
    {
        public static ListInstanceDef ToListInstanceDef(this AccessItemRegistration registration)
        {
            var listInstanceDef = new ListInstanceDef(registration.Config.Tag, registration.ClientAlias, true);
            return listInstanceDef;
        }
    }
}