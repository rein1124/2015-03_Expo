using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Hdc.Serialization;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Configs
{
    public class ProjectDeviceGroupProvider :IDeviceGroupProvider
    {
        private IDeviceGroup _deviceGroup;

        private object locker = new object();

        private const string FileName = "DeviceGroup";
        private const string FileExtensionName = ".xaml";
        private const string FileFullName = FileName + FileExtensionName;

        [Dependency]
        public IDeviceGroupFactory DeviceGroupFactory { get; set; }

        public IDeviceGroup RootDeviceGroup
        {
            get
            {
                lock (locker)
                {
                    if (_deviceGroup == null)
                    {
                        var fileName = GetDeviceConfigFileName();
//                        try
//                        {
                            var dgc = fileName.DeserializeFromXamlFile<DeviceConfigSchema>();
                            Debug.WriteLine(dgc);
                            var deviceGroupConfig = dgc;// DeviceGroupConfigRepository; DeviceGroupConfigRepository.Load();

                            _deviceGroup = DeviceGroupFactory.Create(deviceGroupConfig.RootGroupConfig);
//                        }
//                        catch (Exception e)
//                        {
//                            throw new DeviceConfigSchemaFileNotFoundException("DeviceConfigs is not found", e);
//                        }
                        
                    }
                    return _deviceGroup;
                }
            }
        }

        private static string GetDeviceConfigFileName()
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            var dcFileNames = dir.GetFiles().Where(fi => fi.Name.StartsWith(FileName));
            var dcFileName = dcFileNames.FirstOrDefault();

            //            var fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileFullName);
            return dcFileName == null ? null : dcFileName.FullName;
        }
    }
}