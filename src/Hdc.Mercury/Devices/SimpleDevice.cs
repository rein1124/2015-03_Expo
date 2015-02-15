using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Hdc.Mercury.Communication;
using Hdc.Mercury.Configs;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public class SimpleDevice : IDevice
    {
        private string _name;

        private dynamic _value;

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
            throw new NotSupportedException();
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
            Value = valueStage;
        }

        public Task WriteAsync(dynamic valueStage)
        {
           return Task.Run(() => { Value = valueStage; });
        }

        public IObservable<ValueChangedEventArgs<dynamic>> AsyncRead()
        {
            throw new NotSupportedException();
        }

        public IObservable<Unit> AsyncWrite(dynamic valueStage)
        {
            throw new NotSupportedException();
        }

        public void StartUpdate()
        {
            throw new NotSupportedException();
        }

        public void StopUpdate()
        {
            throw new NotSupportedException();
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
            throw new NotImplementedException();
        }

        public void Init(IAccessChannel channel, IAccessItemRegistration registration)
        {
            throw new NotImplementedException();
        }
    }
}