using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Hdc.Mercury.Configs;

namespace Hdc.Mercury.Communication
{
    public interface IAccessChannel
    {
        void Start();

        Task StartAsync();

        void Stop();

        IEnumerable<IDevice> Register(IEnumerable<DeviceConfig> configs);

        // AddToUpdateList
        void AddToUpdateList(IEnumerable<uint> clientAliases);

        Task AddToUpdateListAsync(IEnumerable<uint> clientAliases);

        // RemoveFromUpdateList
        void RemoveFromUpdateList(IEnumerable<uint> clientAliases);

        // Read
        void Read(IEnumerable<uint> serverAliases);

        Task ReadAsync(IEnumerable<uint> serverAliases);

        // Directly Read
        ReadData DirectRead(IAccessItemRegistration reg);

        IObservable<IList<ReadData>> DirectAsyncRead(IEnumerable<uint> serverAliases);

        IEnumerable<ReadData> DirectReadBlock(IEnumerable<IAccessItemRegistration> regs);

        // Write
        void Write(IEnumerable<WriteData> writeDatas);

        Task WriteAsync(IEnumerable<WriteData> writeDatas);

        IObservable<Unit> AsyncWrite(WriteData writeData);

        IObservable<Unit> AsyncWriteBlock(IEnumerable<WriteData> writeDatas);
    }
}