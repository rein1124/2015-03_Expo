using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Windows;
using Hdc.Linq;
using Hdc.Mercury.Communication;
using Hdc.Mercury.Configs;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public class Device : IDevice
    {
        private dynamic _value;

        private bool _isInitialized;

        [InjectionConstructor]
        public Device()
        {
        }

        public void Init(IAccessItemRegistration registration)
        {
            if (_isInitialized)
            {
                throw new InvalidOperationException("device cannot initialize twice");
            }

            Registration = registration;
            Registration.Subscribe(x => { Value = x.Value; });
            _isInitialized = true;
        }

        public dynamic Value
        {
            get { return _value; }
            set
            {
                if (Equals(_value, value))
                    return;

                var oldValue = _value;
                var newValue = value;
                _value = newValue;

                OnValueChanged(newValue);
            }
        }

        public dynamic Stage { get; set; }

        public string Name
        {
            get { return Registration.Config.Name; }
        }

        public IAccessItemRegistration Registration { get; set; }

        public IAccessChannel Channel
        {
            get { return Registration.Channel; }
        }

        public uint ClientAlias
        {
            get { return Registration.ClientAlias; }
        }

        public dynamic Read()
        {
            Channel.Read(Registration);
            return Value;
        }

        public async Task<dynamic> ReadAsync()
        {
            await Channel.ReadAsync(Registration);
            return Value;
        }

        public IObservable<ValueChangedEventArgs<dynamic>> AsyncRead()
        {
            var subject = new Subject<ValueChangedEventArgs<dynamic>>();

            Channel.DirectAsyncRead(Registration)
                .Subscribe(x => subject.OnNext(UpdateNewValue(x.Value)));

            return subject.Take(1);
        }

        public void Write()
        {
            Write(Value);
        }

        public void Write(dynamic valueStage)
        {
            var writeData = new WriteData(Registration, valueStage);
            Channel.Write(writeData);
        }

        public Task WriteAsync(dynamic valueStage)
        {
            var writeData = new WriteData(Registration, valueStage);
            return Channel.WriteAsync(writeData);
        }

        public IObservable<Unit> AsyncWrite(dynamic valueStage)
        {
            var writeData = new WriteData(Registration, valueStage);
            return Channel.AsyncWrite(writeData);
        }

        private ValueChangedEventArgs<dynamic> UpdateNewValue(dynamic newValue)
        {
            var oldValue = Value;
            Value = newValue;

            var arg = new ValueChangedEventArgs<dynamic>()
                      {
                          OldValue = oldValue,
                          NewValue = newValue,
                      };

            return arg;
        }

        protected virtual void OnValueChanged(dynamic newValue)
        {
        }
    }
}