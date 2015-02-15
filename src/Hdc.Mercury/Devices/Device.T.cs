using System;
using System.Collections;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public class Device<T> : Device, IDevice<T>
    {
        private const double DoubleToIntFixed = 0.000001;

        private readonly ISubject<T> _subject = new Subject<T>();

        [InjectionConstructor]
        public Device()
        {
        }

        public new T Value
        {
            get
            {
                if (base.Value == null)
                {
                    return default(T);
                }

                var oriValue = base.Value; 
                T typedValue = (T) base.Value; 
                dynamic dynamicValue = typedValue;

                var finalValue = MyConvert(oriValue, dynamicValue);

                
//                var finalValue = (T) base.Value;
                return finalValue;
            }
            set { base.Value = value; }
        }

        public new T Stage
        {
            get
            {
                if (base.Stage == null)
                {
                    return default(T);
                }

                var oriValue = base.Stage;
                T typedValue = (T)base.Stage; 
                dynamic dynamicValue = typedValue;

                var finalValue = MyConvert(oriValue, dynamicValue);
                 
                return finalValue;
            }
            set { base.Stage = value; }
        }

        private dynamic MyConvert(double input, double output)
        {
            return input;
        }

        private dynamic MyConvert(double input, int output)
        {
            output = (int) (input + DoubleToIntFixed);
            return output;
        }

        private dynamic MyConvert(double input, uint output)
        {
            output = (uint)(input + DoubleToIntFixed);
            return output;
        }

        private dynamic MyConvert(double input, short output)
        {
            output = (short)(input + DoubleToIntFixed);
            return output;
        }
        private dynamic MyConvert(double input, ushort output)
        {
            output = (ushort)(input + DoubleToIntFixed);
            return output;
        }

        private dynamic MyConvert(bool input, bool output)
        {
            output = Convert.ToBoolean(input);
            return output;
        }

        private dynamic MyConvert(int input, int output)
        {
            return input;
        }

        private dynamic MyConvert(uint input, int output)
        {
            output = (int)(input);
            return output;
        }

        private dynamic MyConvert(ushort input, int output)
        {
            output = (int)(input);
            return output;
        }

        private dynamic MyConvert(ushort input, ushort output)
        {
            return input;
        }

        private dynamic MyConvert(BitArray input, BitArray output)
        {
            return input;
        }

        private dynamic MyConvert(short[] input, short[] output)
        {
            return input;
        }

        private dynamic MyConvert(short[] input, int[] output)
        {
            var ints = input.Select(Convert.ToInt32).ToArray();
            return ints;
        }

        private dynamic MyConvert(ushort[] input, ushort[] output)
        {
            return input;
        }

        private dynamic MyConvert(int[] input, int[] output)
        {
            return input;
        }

        private dynamic MyConvert(uint[] input, uint[] output)
        {
            return input;
        }

        public new T Read()
        {
            dynamic read = base.Read();

            if (read == null)
            {
                return default(T);
            }

            return read;
        }

        public new IObservable<ValueChangedEventArgs<T>> AsyncRead()
        {
            return base.AsyncRead()
                .Select(x => new ValueChangedEventArgs<T>
                             {
                                 NewValue = x.NewValue ?? default(T),
                                 OldValue = x.OldValue ?? default(T),
                                 Sender = this,
                             });
        }

        public void Write(T valueStage)
        {
            base.Write(valueStage);
        }

        public Task WriteAsync(T valueStage)
        {
            return base.WriteAsync(valueStage);
        }

        public IObservable<Unit> AsyncWrite(T valueStage)
        {
            return base.AsyncWrite(valueStage);
        }

        protected override void OnValueChanged(dynamic newValue)
        {
//            base.OnValueChanged(newValue);

            var value = newValue ?? default(T);
            T tv = (T) value;
            _subject.OnNext(tv);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _subject.Subscribe(observer);
        }
    }
}