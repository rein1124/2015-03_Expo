using System;
using System.ComponentModel;

namespace Hdc.Mercury.Communication.OPC
{
    public interface ISimTag : INotifyPropertyChanged
    {
        string TagName { get; set; }

        DeviceDataType DataType { get; set; }

        ISimOpcServer Server { get; set; }

        dynamic Value { get; set; }

        IObservable<dynamic> ValueChangedEvent { get; set; }
    }
}
