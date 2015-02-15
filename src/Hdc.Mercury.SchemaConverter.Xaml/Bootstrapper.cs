using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Hdc.Mercury.Configs;

namespace Hdc.Mercury.SchemaConverter.Xaml
{
    internal class Bootstrapper
    {
        private CompositionContainer _container;

        public Bootstrapper()
        {
            var cc = new AggregateCatalog(
                new List<ComposablePartCatalog>
                    {
                        new AssemblyCatalog("Hdc.Mercury.dll"),
                    }
                );

            _container = new CompositionContainer(cc);
        }

        public void Convert(string deviceDefSchemaFileName, string deviceGroupConfigFileName)
        {
            var deviceDefSchemaRepository = _container.GetExport<IDeviceDefSchemaRepository>("Xaml").Value;

            var deviceDefSchema = deviceDefSchemaRepository.Load(deviceDefSchemaFileName);
            GenerateDeviceConfigs(deviceDefSchema);
        }

        private void GenerateDeviceConfigs(DeviceDefSchema deviceDefSchema)
        {
//            var deviceGroupConfig = deviceDefToConfigConverter.Convert(deviceDefSchema);
            //            deviceGroupConfigRepository.Save(deviceGroupConfig);
            var deviceGroupConfigRepository = _container.GetExport<IDeviceGroupConfigRepository>("Xaml").Value;
            var deviceDefToConfigConverter = _container.GetExport<IDeviceDefSchemaToConfigConverter>().Value;

            if (deviceDefSchema.GenerationCount == 0)
            {
                var configs = deviceDefToConfigConverter.Convert(deviceDefSchema);
                var deviceGroupConfig = configs.First();
                var schema = new DeviceConfigSchema() { RootGroupConfig = deviceGroupConfig };
                deviceGroupConfigRepository.Save(schema);
            }
            else
            {
                var configs = deviceDefToConfigConverter.Convert(deviceDefSchema).ToList();
                var schemas = configs.Select(x => new DeviceConfigSchema() {RootGroupConfig = x});
                deviceGroupConfigRepository.Save(schemas);
            }
        }

        public void Convert(IEnumerable<string> deviceDefSchemaFileNames, string deviceGroupConfigFileName)
        {
            var deviceDefSchemaRepository = _container.GetExport<IDeviceDefSchemaRepository>().Value;
            var deviceDefSchemaCompositor = _container.GetExport<IDeviceDefSchemaCompositor>().Value;

            var deviceDefSchemas = deviceDefSchemaFileNames.Select(deviceDefSchemaRepository.Load);
            var allSchema = deviceDefSchemaCompositor.Composite(deviceDefSchemas);

            GenerateDeviceConfigs(allSchema);
        }
    }
}