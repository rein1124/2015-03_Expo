using System.Linq;
using ODM.Domain.Schemas;

namespace ODM.Domain.Configs
{
    public static class MachineConfigExtensions
    {
        public static ParameterMetadata GetParameterMetadata(this MachineConfig machineConfig, string name)
        {
            return machineConfig.ParameterMetadatas.SingleOrDefault(x => x.Name == name);
        }

//        public static IParameterMetadataSetter SetParameterMetadata(this MachineConfig machineConfig,
//                                                                    ParameterName parameterName)
//        {
//            var pm = machineConfig.GetParameterMetadata(parameterName);
//            var setter = new ParameterMetadataSetter(pm);
//            return setter;
//        }
    }
}