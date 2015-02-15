using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Markup;
using Hdc;
using Hdc.Mv.Halcon;
using ODM.Domain.Schemas;

namespace ODM.Domain.Configs
{
    using System.Collections.ObjectModel;

    // ReSharper disable InconsistentNaming
    //[ContentProperty("Units")]
    [ContentProperty("ParameterMetadatas")]
    public partial class MachineConfig
    {
        public MachineConfig()
        {
            ParameterMetadatas = new Collection<ParameterMetadata>();
            MV_SimulationImageFileNames = new Collection<string>();
        }

        public string General_LocalDbInstanceName { get; set; }

        // PLC
        public bool PLC_SimulationAccessChannelEnabled { get; set; }
        public string PLC_OpcXiServerConfig_UserName { get; set; }
        public string PLC_OpcXiServerConfig_Password { get; set; }
        public string PLC_OpcXiServerConfig_ServerUrl { get; set; }

        // MV
        public int MV_AcquisitionCountPerWorkpiece { get; set; }
        public int MV_LineScanFrameWidth { get; set; }
        public int MV_LineScanFrameHeight { get; set; }
        public int MV_LineScanMosaicCount { get; set; }
        public bool MV_SimulationAcquisitionEnabled { get; set; }
        public bool MV_SimulationCalibrationEnabled { get; set; }
        public Interpolation MV_CalibrationInterpolation { get; set; }
        public Collection<string> MV_SimulationImageFileNames { get; set; }
        public bool MV_SimulationInspectorEnabled { get; set; }

        // Reporting
        public string Reporting_ImageStorePath { get; set; }
        public bool Reporting_StoreAcceptedImageEnabled { get; set; }
        public bool Reporting_StoreRejectedImageEnabled { get; set; }

        // ParameterMetadatas
        public Collection<ParameterMetadata> ParameterMetadatas { get; set; }

    }

    // ReSharper restore InconsistentNaming
}