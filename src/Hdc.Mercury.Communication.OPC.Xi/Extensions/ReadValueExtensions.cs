using Advosol.Paxi;
using Hdc.Mercury.Configs;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public static class ReadValueExtensions
    {
//        private static dynamic ConvertReadData(AccessItemRegistration registerInfo, dynamic data)
//        {
//            return data;
//        }

/*        public static ReadData ToReadData(this ReadValue readData)
        {
            dynamic data = readData.DataValue.Value;

            var readData1 = new ReadData
                                {
                                    Value = data,
                                    ClientAlias = readData.ClientAlias,
                                };
            return readData1;
        }*/

        public static ReadData ToReadData(this ReadValue readData, IAccessItemRegistration reg)
        {
            dynamic data = readData.DataValue.Value;

            var readData1 = new ReadData
                                {
                                    Value = DeviceConfigExtensions.Convert(data, reg),
                                    ClientAlias = readData.ClientAlias
                                };
            return readData1;
        }
    }
}