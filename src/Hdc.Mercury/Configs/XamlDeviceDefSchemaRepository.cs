using System;
using System.ComponentModel.Composition;
using System.IO;
using Hdc.Mercury.Configs;
using Hdc.Serialization;

namespace Hdc.Mercury.Configs
{
    [Export("Xaml", typeof (IDeviceDefSchemaRepository))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class XamlDeviceDefSchemaRepository : IDeviceDefSchemaRepository
    {
        public DeviceDefSchema Load(string fileName)
        {
            var path = new FileInfo(fileName);

            try
            {
                var deviceDefSchema = path.FullName.DeserializeFromXamlFile<DeviceDefSchema>();
                return deviceDefSchema;
            }
            catch (Exception e)
            {
                throw new DeviceDefSchemaFileNotFoundException("DeviceDefSchema file not found", e)
                          {
                              FileName = path.FullName,
                          };
            }
        }

        public void Save(DeviceDefSchema deviceDefSchema, string fileName)
        {
            deviceDefSchema.SerializeToXamlFile(fileName);
        }
    }
}