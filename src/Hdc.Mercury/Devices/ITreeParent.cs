using System;
using System.Collections.Generic;
using Hdc.Mercury;

namespace Hdc.Mercury
{
    public interface IPress:IDeviceGroup
    {
        IList<ISide> Side { get; }
    }

    class Press :DeviceGroup, IPress
    {
        public IList<ISide> Side
        {
            get { throw  new NotImplementedException(); }
        }
    }

    public interface ISide:IDeviceGroup
    {
        
    }

    public interface IUnit : IDeviceGroup
    {
        
    }

    public interface IInkKey : IDeviceGroup
    {
        
    }
}