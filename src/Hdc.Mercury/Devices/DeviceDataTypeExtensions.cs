using System;
using System.Collections;

namespace Hdc.Mercury
{
    public static class DeviceDataTypeExtensions
    {
        public static Type ToAccessDataType(this DeviceDataType dataType)
        {
            Type type = null;
            Switch.On(dataType)
                .Case(DeviceDataType.Int16, () => { type = typeof (Int16); })
                .Case(DeviceDataType.Int32, () => { type = typeof (Int32); })
                .Case(DeviceDataType.Int64, () => { type = typeof (Int64); })
                .Case(DeviceDataType.UInt16, () => { type = typeof (UInt16); })
                .Case(DeviceDataType.UInt32, () => { type = typeof (UInt32); })
                .Case(DeviceDataType.UInt64, () => { type = typeof (UInt64); })
                .Case(DeviceDataType.Boolean, () => { type = typeof (Boolean); })
                .Case(DeviceDataType.Byte, () => { type = typeof (Byte); })
                .Case(DeviceDataType.SByte, () => { type = typeof (SByte); })
                .Case(DeviceDataType.Decimal, () => { type = typeof (Decimal); })
                .Case(DeviceDataType.Double, () => { type = typeof (Double); })
                .Case(DeviceDataType.HSBCD, () => { type = typeof (HSBCD); })
                .Case(DeviceDataType.SBCD10, () => { type = typeof (SBCD10); })
                .Case(DeviceDataType.BooleanList, () => { type = typeof(BooleanList); })
                .Case(DeviceDataType.String, () => { type = typeof(String); })
                .Case(DeviceDataType.BitArray, () => { type = typeof(BitArray); })
                .Case(DeviceDataType.Int16Array, () => { type = typeof(Int16[]); })
                .Case(DeviceDataType.UInt16Array, () => { type = typeof(UInt16[]); })
                .Case(DeviceDataType.Int32Array, () => { type = typeof(Int32[]); })
                .Case(DeviceDataType.UInt32Array, () => { type = typeof(UInt32[]); })
                .Default(() => { type = typeof (UnknownDateType); });
            return type;
        }
    }
}