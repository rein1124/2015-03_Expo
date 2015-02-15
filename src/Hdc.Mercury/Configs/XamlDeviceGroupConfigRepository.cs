using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using Hdc.Serialization;

namespace Hdc.Mercury.Configs
{
    [Export("Xaml", typeof (IDeviceGroupConfigRepository))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class XamlDeviceGroupConfigRepository : IDeviceGroupConfigRepository
    {
        private const string FileName = "DeviceConfigs";
        private const string FileExtensionName = ".xaml";
        private const string FileFullName = FileName + FileExtensionName;


        private static string GetDeviceConfigFileName()
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            var dcFileNames = dir.GetFiles().Where(fi => fi.Name.StartsWith(FileName));
            var dcFileName = dcFileNames.FirstOrDefault();

            //            var fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileFullName);
            return dcFileName == null ? null : dcFileName.FullName;
        }

        public DeviceConfigSchema Load()
        {
            var fileName = GetDeviceConfigFileName();
            try
            {
                var dgc = fileName.DeserializeFromXamlFile<DeviceConfigSchema>();
                return dgc;
            }
            catch (Exception e)
            {
                throw new DeviceConfigSchemaFileNotFoundException("DeviceConfigs is not found", e);
            }
        }

        public void Save(DeviceConfigSchema deviceGroupConfig)
        {
            deviceGroupConfig.SerializeToXamlFile(FileFullName);
        }

        public void Save(IEnumerable<DeviceConfigSchema> deviceGroupConfigs)
        {
            int index = 0;

            foreach (var deviceGroupConfig in deviceGroupConfigs)
            {
                Save(deviceGroupConfig, index);
                index++;
            }
        }

        public void Save(DeviceConfigSchema deviceGroupConfig, int generationIndex)
        {
            deviceGroupConfig.SerializeToXamlFile(FileName + "." + generationIndex + FileExtensionName);
        }
    }
}