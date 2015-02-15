using System;
using System.Reactive;
using System.Threading.Tasks;
using Hdc.Mercury.Communication;
using Hdc.Mercury.Configs;

namespace Hdc.Mercury
{
    public interface IDevice
    {
        string Name { get; }

        dynamic Value { get; set; }

        dynamic Stage { get; set; }

        // Read
        dynamic Read();

        Task<dynamic> ReadAsync();

        IObservable<ValueChangedEventArgs<dynamic>> AsyncRead();

        // Write
        void Write();

        void Write(dynamic valueStage);

        Task WriteAsync(dynamic valueStage);

        IObservable<Unit> AsyncWrite(dynamic valueStage);

//        bool IsWritingFreezed { get; set; }

        IAccessItemRegistration Registration { get; }

        void Init(IAccessItemRegistration registration);
    }
}