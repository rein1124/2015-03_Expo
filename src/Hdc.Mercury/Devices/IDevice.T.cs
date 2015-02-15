using System;
using System.Reactive;
using System.Threading.Tasks;
using Hdc.Reactive;

namespace Hdc.Mercury
{
    public interface IDevice<T> : IDevice, IValueObservable<T>
    {
        new T Value { get; set; }

        new T Stage { get; set; }
        
        //sync
        new T Read();

        void Write(T valueStage);

        Task WriteAsync(T valueStage);

        //asyc
        new IObservable<ValueChangedEventArgs<T>> AsyncRead();

        IObservable<Unit> AsyncWrite(T valueStage);
         
        //update
        //new IObservable<ValueChangedEventArgs<T>> ValueChangedEvent { get; } 
    }
}