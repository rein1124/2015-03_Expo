using System;
using System.Collections.Generic;
using System.Linq;
using Hdc.Serialization;
using ODM.Domain.Schemas;
using Shared;

namespace ODM.Domain.Configs
{
    public partial class MachineConfigFactory
    {
        public static MachineConfig GetDefaultConfig()
        {
            var config = new MachineConfig
            {
                // General
                General_LocalDbInstanceName = "v12.0",

                // PLC
                PLC_SimulationAccessChannelEnabled = false,
                PLC_OpcXiServerConfig_ServerUrl = "da:Takebishi.Dxp.1",
                PLC_OpcXiServerConfig_UserName = "",
                PLC_OpcXiServerConfig_Password = "",

                // MV
                MV_AcquisitionCountPerWorkpiece = 2,
                MV_LineScanFrameWidth = 8192,
                MV_LineScanFrameHeight = 500,
                MV_LineScanMosaicCount = 25,
                MV_SimulationAcquisitionEnabled = true,
                MV_SimulationInspectorEnabled = true,

                // Reporting
                Reporting_ImageStorePath = "ImageStore",
                Reporting_StoreAcceptedImageEnabled = true,
                Reporting_StoreRejectedImageEnabled = true,
            };
            config.MV_SimulationImageFileNames.Add(@"sample\SurfaceFront_720x1280.bmp");
            config.MV_SimulationImageFileNames.Add(@"sample\SurfaceBack_720x1280.bmp");

            InitParametersFromSchema(config);

            // TODO, could be removed, rein 2013-09-12
//            InitParametersFromEnum(config);


            return config;
        }

        private static void InitParametersFromSchema(MachineConfig config)
        {
            var schema = ParameterMetadataSchema.CreateDefaultSchema();

            foreach (var p in schema.ParameterMetadatas)
            {
                config.ParameterMetadatas.Add(p);
            }
        }

//        private static void InitParametersFromEnum(MachineConfig config)
//        {
//            var pms = config.ParameterMetadatas;
//
//            foreach (ParameterName value in Enum.GetValues(typeof(ParameterName)))
//            {
//                var p = pms.SingleOrDefault(x => x.Name == value);
//
//                if (p != null)
//                    continue;
//
//                pms.Add(new ParameterMetadata()
//                            {
//                                CatalogName = "Default",
//                                GroupName = "Default",
//                                Name = value,
//                                Description = value.ToString(),
//                                Maximum = int.MaxValue,
//                                Minimum = int.MinValue,
//                                DefaultValue = default(int),
//                            });
//            }
//        }
    }
}