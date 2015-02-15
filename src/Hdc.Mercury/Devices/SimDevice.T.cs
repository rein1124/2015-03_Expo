using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Hdc.Mercury
{
    public class SimDevice<T> : SimDevice, ISimDevice<T>
    {
        private readonly ISubject<T> _subject = new Subject<T>();

        public new T Value
        {
            get { return base.Value ?? default(T); }
            set { base.Value = value; }
        }

        public new T Stage
        {
            get { return base.Stage ?? default(T); }
            set { base.Stage = value; }
        }

        public new T Read()
        {
            base.Read();
            return Value;
        }

        public void Write(T valueStage)
        {
            base.Write(valueStage);
        }

        public Task WriteAsync(T valueStage)
        {
            return base.WriteAsync(valueStage);
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

        public IObservable<Unit> AsyncWrite(T valueStage)
        {
            return base.AsyncWrite(valueStage);
        }

//        public new IObservable<ValueChangedEventArgs<T>> ValueChangedEvent
//        {
//            get
//            {
//                return base.ValueChangedEvent
//                    .Select(x => new ValueChangedEventArgs<T>
//                                     {
//                                         NewValue = x.NewValue ?? default(T),
//                                         OldValue = x.OldValue ?? default(T),
//                                         Sender = this,
//                                     });
//            }
//        }

        protected override void OnValueChanged(dynamic newValue)
        {
//            base.OnValueChanged(newValue);
            T value = newValue ?? default(T);
            _subject.OnNext(value);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _subject.Subscribe(observer);
        }
    }
}