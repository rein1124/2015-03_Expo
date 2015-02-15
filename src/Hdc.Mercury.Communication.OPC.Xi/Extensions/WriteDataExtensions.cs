using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Advosol.Paxi;
using Hdc.Mercury.Configs;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Mercury.Communication
{
    public static class WriteDataExtensions
    {
        private static readonly IDictionary<string, Type> TypeInstances = new ConcurrentDictionary<string, Type>();

        private static IServiceLocator ServiceLocator
        {
            get { return Microsoft.Practices.ServiceLocation.ServiceLocator.Current; }
        }

        public static DataValue ToDataValue(this WriteData writeData)
        {
            var xiDataValue = new DataValue(
                0,
                ConvertValueToServerType(writeData.Registration, writeData.Value));
            return xiDataValue;
        }


        public static WriteValue ToWriteValue(this WriteData writeData)
        {
            var xiWriteData = new WriteValue
                                  {
                                      DataValue = writeData.ToDataValue(),
                                      ServerAlias = writeData.Registration.ServerAlias,
                                  };
            return xiWriteData;
        }

        public static IEnumerable<WriteValue> ToWriteValues(this IEnumerable<WriteData> writeDatas)
        {
            return writeDatas.Select(wd => wd.ToWriteValue());
        }

        private static dynamic ConvertValueToServerType(IAccessItemRegistration reg, dynamic data)
        {
            var convertedValue = DeviceConfigExtensions.Convert(data, reg);
            var type1 = reg.Config.DataType.ToAccessDataType().ToString();
            var type2 = reg.ServerDataType;

            if (type2 == null)
                throw new Exception("registerInfo.ServerDataType==null");

            if (type2 == "System.Void")
            {
                throw new InvalidOperationException(string.Format("Device:{0}, registerInfo.ServerDataType is void",
                                                                  type2));
            }

            if (type1 == type2)
                return convertedValue;

            string converterTypeName = type1 + type2;

            if (!TypeInstances.ContainsKey(type1 + type2))
            {
                var converterGenericType = typeof (IDataConverter<,>);
                var t1 = Type.GetType(type1);
                var t2 = Type.GetType(type2);
                var converterType = converterGenericType.MakeGenericType(t1, t2);
                TypeInstances.Add(converterTypeName, converterType);
            }

            var converterType2 = TypeInstances[converterTypeName];
            dynamic converterInstance = ServiceLocator.GetInstance(converterType2);
            return converterInstance.Convert(convertedValue);
        }
    }
}