using System;
using System.Reactive.Subjects;
using Hdc.Generators;
using Hdc.Mercury.Configs;

namespace Hdc.Mercury.Communication
{
    public class AccessItemRegistration : IAccessItemRegistration
    {
        private readonly ISubject<ReadData> _subject = new Subject<ReadData>();

        public DeviceConfig Config { get; private set; }

        public string ServerDataType { get; set; }

        public uint ClientAlias { get; private set; }

        public uint ServerAlias { get; set; }

        public IAccessChannel Channel { get; set; }

        private AccessItemRegistration()
        {
            ClientAlias = (uint) IdentityGeneratorServiceLocator.IdentityGenerator.Generate();
        }

//        public AccessItemRegistration(AccessItemConfig config) : this()
//        {
//            Config = config;
//        }

        public AccessItemRegistration(DeviceConfig config, IAccessChannel channel)
            : this()
        {
            Config = config;
            Channel = channel;
        }

        public IDisposable Subscribe(IObserver<ReadData> observer)
        {
            return _subject.Subscribe(observer);
        }

        public void OnNext(ReadData value)
        {
            _subject.OnNext(value);
        }

        public void OnError(Exception error)
        {
            _subject.OnError(error);
        }

        public void OnCompleted()
        {
            _subject.OnCompleted();
        }
    }
}