using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Hdc.Mercury.Communication;
using Hdc.Mercury.Configs;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public class SimDevice : IDevice
    {
        private string _name;

        private dynamic _value;

        private bool isInitialized;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
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

        protected virtual void OnValueChanged(dynamic newValue)
        {
        }

        public dynamic Read()
        {
            return Value;
        }

        public Task<dynamic> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public void Write()
        {
            Write(Value);
        }

        public void Write(dynamic valueStage)
        {
            UpdateNewValue(valueStage);
        }

        public Task WriteAsync(dynamic valueStage)
        {
            return Task.Run(() => UpdateNewValue(valueStage));
        }

        public IObservable<ValueChangedEventArgs<dynamic>> AsyncRead()
        {
            var end = new Subject<ValueChangedEventArgs<dynamic>>();
            Parallel.Invoke(() =>
                                {
                                    Thread.Sleep(1);
                                    end.OnNext(UpdateNewValue(Value));
                                });

            return end.Take(1);
        }

        public IObservable<Unit> AsyncWrite(dynamic valueStage)
        {
            var end = new Subject<Unit>();
            Parallel.Invoke(() =>
                                {
                                    UpdateNewValue(valueStage);
                                    Thread.Sleep(1);
                                    end.OnNext(new Unit());
                                });

            return end.Take(1);
        }

        public void StartUpdate()
        {
        }

        public void StopUpdate()
        {
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

        public IAccessChannel Channel
        {
            get { throw new NotImplementedException(); }
        }

        public uint ClientAlias
        {
            get { throw new NotImplementedException(); }
        }

        public IAccessItemRegistration Registration { get; set; }

        public void Init(IAccessItemRegistration registration)
        {
            if (isInitialized)
            {
                throw new InvalidOperationException("device cannot initialize twice");
            }

            Name = registration.Config.Name;
            Registration = registration;
            Registration.Subscribe(x => { Value = x.Value; });
            isInitialized = true;
        }

        public void Init(IAccessChannel channel, IAccessItemRegistration registration)
        {
            throw new NotImplementedException();
        }
    }
}