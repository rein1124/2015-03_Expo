using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Hdc;
using Hdc.Mercury.Communication;
using Hdc.Mercury.Communication.OPC;
using Hdc.Mercury.Configs;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Communication
{
    public class SimAccessChannel : IAccessChannel
    {
        [Dependency]
        public ISimOpcServer Server { get; set; }

        [Dependency]
        public ISimTagFactory SimTagFactory { get; set; }

        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        private readonly IDictionary<uint, AccessItemRegistration> _registrations =
            new ConcurrentDictionary<uint, AccessItemRegistration>();

        /// <summary>
        /// clientAlias, serverAlias
        /// </summary>
        private readonly Dictionary<uint, uint> _refreshList = new Dictionary<uint, uint>();

        public void Initialize(AccessChannelConfig accessChannelConfig)
        {
//            throw new NotImplementedException();
        }

        public void Start()
        {
        }

        public Task StartAsync()
        {
            return Task.Run(() => { });
        }

        public void Stop()
        {
        }

        public IEnumerable<IDevice> Register(IEnumerable<DeviceConfig> configs)
        {
            var ret = new List<IDevice>();

            foreach (var config in configs)
            {
                var reg = new AccessItemRegistration(config, this);
                _registrations.Add(reg.ClientAlias, reg);
                

                var tag = SimTagFactory.Build(config.DataType);
                tag.Server = Server;
                tag.TagName = config.Tag;
                tag.DataType = config.DataType;
                Server.Tags.Add(reg.ClientAlias, tag);


                var accessDataType = config.DataType.ToAccessDataType();
                var deviceType = typeof(ISimDevice<>).MakeGenericType(accessDataType);
                var dvc = ServiceLocator.GetInstance(deviceType) as IDevice;
                dvc.Init(reg);
                ret.Add(dvc);
            }

            return ret;
        }

        public void Read(IEnumerable<IAccessItemRegistration> regs)
        {
            foreach (var reg in regs)
            {
                var value = Server.Tags[reg.ClientAlias].Value;
                reg.OnNext(value);
            }
        }

        public Task ReadAsync(IEnumerable<IAccessItemRegistration> regs)
        {
            throw new NotImplementedException();
        }

//        public void RegisterAccessItems(
//            IEnumerable<Tuple<AccessItemConfig, Action<uint>>> accessItemConfigWithCallbacks)
//        {
//            foreach (var config in accessItemConfigWithCallbacks)
//            {
//                var registerInfo = new AccessItemRegistration(config.Item1, this);
//                _registrations.Add(registerInfo.ClientAlias, registerInfo);
//                var tag = SimTagFactory.Build(config.Item1.DataType);
//                tag.Server = Server;
//                tag.TagName = config.Item1.Tag;
//                tag.DataType = config.Item1.DataType;
//                Server.Tags.Add(registerInfo.ClientAlias, tag);
//                config.Item2(registerInfo.ClientAlias);
//            }
//        }

        public void Read(IAccessItemRegistration registration)
        {
            var value = Server.Tags[registration.ClientAlias].Value;
            registration.OnNext(value);
        }

        public void Read(IEnumerable<uint> serverAliases)
        {
            throw new NotImplementedException();
        }

        public Task ReadAsync(IEnumerable<uint> serverAliases)
        {
            throw new NotImplementedException();
        }

        public void ReadBlock(IEnumerable<IAccessItemRegistration> registrations)
        {
            foreach (var reg in registrations)
            {
                var value = Server.Tags[reg.ClientAlias].Value;
                reg.OnNext(value);
            }
        }

        public void Write(WriteData writeData)
        {
            Server.Tags[writeData.Registration.ServerAlias].Value = writeData.GetConvertBackValue();
        }

        public void Write(IEnumerable<WriteData> writeDataPairs)
        {
            foreach (var writeData in writeDataPairs)
            {
                Server.Tags[writeData.ClientAlias].Value = writeData.GetConvertBackValue();
            }
        }

        public Task WriteAsync(IEnumerable<WriteData> writeDataPairs)
        {
           return Task.Run(() => Write(writeDataPairs));
        }

        public ReadData DirectRead(IAccessItemRegistration reg)
        {
            var clientAlias = reg.ClientAlias;
            var readDirectly = new ReadData
                                   {
                                       Value = Server.Tags[clientAlias].Value.Convert(reg.Config),
                                       ClientAlias = clientAlias
                                   };
            return readDirectly;
        }

        public IEnumerable<ReadData> DirectReadBlock(IEnumerable<IAccessItemRegistration> regs)
        {
            var readDatas = regs.Select(reg => new ReadData
                                                  {
                                                      Value = Server.Tags[reg.ClientAlias].Value.Convert(reg.Config),
                                                      ClientAlias = reg.ServerAlias
                                                  })
                .ToList();
            return readDatas;
        }

        public IObservable<Unit> AsyncRead(IEnumerable<uint> serverAliases)
        {
            throw new NotImplementedException();
        }

        public IObservable<IList<ReadData>> DirectAsyncRead(IEnumerable<uint> serverAliases)
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> AsyncRead(IAccessItemRegistration registration)
        {
            var reg = _registrations[registration.ClientAlias];

            var readData = new ReadData
                               {
                                   ClientAlias = registration.ClientAlias,
                                   Value = Server.Tags[registration.ClientAlias].Value.Convert(reg.Config),
                               };

            reg.OnNext(readData);
            return Observable.Return(new Unit());
        }

        public IObservable<Unit> AsyncRead(IEnumerable<IAccessItemRegistration> accessItemIdentifiers)
        {
            foreach (var accessItemIdentifier in accessItemIdentifiers)
            {
                var reg = _registrations[accessItemIdentifier.ClientAlias];

                var readData = new ReadData
                                   {
                                       ClientAlias = reg.ClientAlias,
                                       Value = Server.Tags[accessItemIdentifier.ClientAlias].Value.Convert(reg.Config)
                                   };

                reg.OnNext(readData);
            }

            return Observable.Return(new Unit());
        }


        public IObservable<ReadData> DirectAsyncRead(IAccessItemRegistration reg)
        {
            var readData = new ReadData
                               {
                                   ClientAlias = reg.ClientAlias,
                                   Value = Server.Tags[reg.ClientAlias].Value.Convert(reg.Config)
                               };
            return Observable.Return(readData);
        }

        public IObservable<IEnumerable<ReadData>> DirectAsyncRead(
            IEnumerable<IAccessItemRegistration> Regs)
        {
            var readDatas = new List<ReadData>();
            foreach (var reg in Regs)
            {
                var readData = new ReadData
                                   {
                                       Value = Server.Tags[reg.ClientAlias].Value.Convert(reg.Config),
                                       ClientAlias = reg.ClientAlias
                                   };
                readDatas.Add(readData);
            }

            return Observable.Return(readDatas);
        }


        public IObservable<Unit> AsyncWrite(WriteData writeData)
        {
            var simTag = Server.Tags[writeData.ClientAlias];
            simTag.Value = writeData.GetConvertBackValue();
            return Observable.Return(new Unit());
        }

        public IObservable<Unit> AsyncWriteBlock(IEnumerable<WriteData> writeDatas)
        {
            foreach (var writeData in writeDatas)
            {
                Server.Tags[writeData.ClientAlias].Value = writeData.GetConvertBackValue();
            }
            return Observable.Return(new Unit());
        }

        public void AddToUpdateList(IEnumerable<uint> registrations)
        {
            Server.Tags
                .Where(p => registrations.Select(x => x).Contains(p.Key))
                .ForEach(p => { p.Value.PropertyChanged += Value_PropertyChanged; });
        }

        public Task AddToUpdateListAsync(IEnumerable<uint> registrations)
        {
            throw new NotImplementedException();
        }


        public void RemoveFromUpdateList(IEnumerable<uint> registrations)
        {
            Server.Tags
                .Where(p => registrations.Select(x => x).Contains(p.Key))
                .ForEach(p => { p.Value.PropertyChanged -= Value_PropertyChanged; });
        }

        private void Value_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var tag = (ISimTag) sender;
            _registrations
                .Where(p => p.Value.Config.Tag == tag.TagName)
                .ForEach(p => p.Value.OnNext(new ReadData
                                                 {
                                                     Value = tag.Value.Convert(p.Value.Config),
                                                     ClientAlias = p.Value.ServerAlias
                                                 }));
        }
    }
}