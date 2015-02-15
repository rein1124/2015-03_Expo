/*using System;
using ODM.Domain.Schemas;
using Shared;

namespace ODM.Domain
{
    public static class DeviceNamesExtensions
    {
        public static ParameterName ToParameterName(this DeviceNames deviceNames)
        {
            ParameterName pn;
            Enum.TryParse(deviceNames.ToString(), out pn);
            return pn;
        }

        public static DeviceNames ToDeviceName(this ParameterName parameterName)
        {
            DeviceNames pn;
            Enum.TryParse(parameterName.ToString(), out pn);
            return pn;
        }
    }
}*/