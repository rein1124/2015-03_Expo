using System.Linq;
using Microsoft.Practices.Unity;
using ODM.Domain.Configs;

namespace ODM.Infrastructure
{
    using Hdc;
    using Hdc.IO;
    using Hdc.Reflection;
    using Hdc.Serialization;
    using PPH;

    public class XamlMachineConfigSaveLoadService : IMachineConfigSaveLoadService
    {
        private string GetConfigFileName()
        {
            var filePath = GetType()
                .Assembly
                .GetAssemblyDirectoryPath()
                .Combine(FileNames.Config + ".xaml");

            return filePath;
        }

        public void Input(MachineConfig data)
        {
            var fileName = GetConfigFileName();
            data.SerializeToXamlFile(fileName);
        }

        public MachineConfig Output()
        {
            var fileName = GetConfigFileName();


            if (fileName.IsFileNotExist())
            {
                var defaultPressConfig = MachineConfigFactory.GetDefaultConfig();
                Input(defaultPressConfig);
                return defaultPressConfig;
            }

            var machineConfig = fileName.DeserializeFromXamlFile<MachineConfig>();

            if (CheckContainsAllParameters(machineConfig))
                Input(machineConfig);

            return machineConfig;
        }

        private bool CheckContainsAllParameters(MachineConfig machineConfig)
        {
            var ps = ParameterMetadataSchema.CreateDefaultSchema();

            bool isContains = true;

            foreach (var parameterMetadata in ps.ParameterMetadatas)
            {
                var pe = machineConfig.ParameterMetadatas.SingleOrDefault(x => x.Name == parameterMetadata.Name);

                if (pe != null)
                    continue;

                machineConfig.ParameterMetadatas.Add(parameterMetadata);
                isContains = false;
            }

            return isContains;
        }
    }
}