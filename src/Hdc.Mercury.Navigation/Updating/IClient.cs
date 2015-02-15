using System.Collections.Generic;
using Hdc.Mercury;

namespace Hdc.Mercury.Navigation
{
    public interface IClient
    {
        void AddUpdatingDevices(IEnumerable<IDevice> updatingDevices);
        void ClearUpdatingDevices();
    }
}