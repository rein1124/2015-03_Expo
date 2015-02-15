using System;
using System.ComponentModel;
using System.Reactive.Subjects;

namespace Hdc.Mercury.Communication.OPC
{
    public class SimTag : ISimTag
    {
        protected readonly Subject<dynamic> Subject = new Subject<dynamic>();

        public string TagName { get; set; }

        public DeviceDataType DataType { get; set; }

        public ISimOpcServer Server { get; set; }

        private dynamic _value;
        public dynamic Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Subject.OnNext(value);
                InvokePropertyChanged("Value");
            }
        }

        public IObservable<dynamic> ValueChangedEvent { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}