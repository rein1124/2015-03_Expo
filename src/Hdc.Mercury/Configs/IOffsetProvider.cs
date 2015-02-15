using System.Collections.Generic;

namespace Hdc.Mercury.Configs
{
    public interface IOffsetProvider
    {
        int GetOffset(DeviceDef deviceDef, int globalIndex);

        IList<int> GetOffsets(DeviceDef deviceDef);

        void Init(DeviceDefSchema schema);
    }
}