using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Advosol.Paxi;
using Hdc.Mercury.Communication.OPC.Xi;
using Hdc.Mercury.Communication;
using Hdc.Mercury.Configs;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Xi.Contracts.Data;
using SBCD10 = System.Int16;
using HSBCD = System.Int16;

namespace Hdc.Mercury.Communication
{
    public class XiAccessChannel : IAccessChannel, IDisposable
    {
        private XiDataList _dataList;

        /// <summary>
        /// clientAlias, AccessItemRegistration
        /// </summary>
        private readonly IDictionary<uint, XiAccessItemRegistration> _registrations =
            new ConcurrentDictionary<uint, XiAccessItemRegistration>();

        //        /// <summary>
        //        /// clientAlias, AccessItemRegistration
        //        /// </summary>
        //        private readonly IDictionary<uint, XiAccessItemRegistration> _refreshRegistrations =
        //            new ConcurrentDictionary<uint, XiAccessItemRegistration>();

        private AccessChannelConfig _channelConfig;

        private bool _isStarted;
        private bool _isDataListInitialized;

        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        [Dependency("XiServerProvider")]
        public IXiServerProvider XiServerProvider { get; set; }

        public void Initialize(AccessChannelConfig accessChannelConfig)
        {
            _channelConfig = accessChannelConfig;
        }

        public void Start()
        {
            if (_isStarted)
                throw new InvalidOperationException("AccessChannel cannot start twice");

            _isStarted = true;

            XiServerProvider.XiServer.Initiate();

            var r = XiServerProvider.XiServer
                .AddDataList(
                    true,
                    _channelConfig.Interval, //update rate auto = 0;
                    0,
                    null,
                    null,
                    _channelConfig.AccessMode.ToListRWAccessMode(),
                    _channelConfig.SubscriptionMode.ToSubscriptionUpdateMode());

            _dataList = r;
            _dataList.OnInformationReport += _dataList_OnInformationReport;
            _dataList.OnPollError += _dataList_OnPollError;
            _isDataListInitialized = true;
        }

        public async Task StartAsync()
        {
            if (_isStarted)
                throw new InvalidOperationException("AccessChannel cannot start twice");

            _isStarted = true;

            var initateCompleteEventArgs = await XiServerProvider.XiServer.InitiateAsync();

            if (initateCompleteEventArgs.Error != null)
                throw new Exception("InitiateEx failed", initateCompleteEventArgs.Error);

            var defineListCompleteEventArgs = await XiServerProvider.XiServer.AddDataListAsync(
                true,
                _channelConfig.Interval, //update rate auto = 0;
                0,
                null,
                null,
                _channelConfig.AccessMode.ToListRWAccessMode(),
                _channelConfig.SubscriptionMode.ToSubscriptionUpdateMode());

            _dataList = defineListCompleteEventArgs.DataList;
            _dataList.OnInformationReport += _dataList_OnInformationReport;
            _dataList.OnPollError += _dataList_OnPollError;
            _isDataListInitialized = true;
        }

        public void Stop()
        {
            _dataList.StopPoll();
            XiServerProvider.XiServer.Dispose();
            //            _dataList.Dispose();
        }

        private void _dataList_OnPollError(string info, Exception ex, object tag)
        {
            var message = string.Format(
                "Mercury Error in Poll Handling:{0}, tag:{1}", info, tag);
            throw new MercuryAccessException(message, ex);
        }

        private void _dataList_OnInformationReport(uint listId, List<ReadValue> updatedValues, List<ErrorInfo> errorInfo)
        {
            if (errorInfo != null)
                return;
            //                throw new MercuryAccessException(
            //                    "OnInformationReport failed");

            if (!updatedValues.Any())
                return;

            var counter = 0;
            updatedValues.ForEach(
                rv =>
                {
                    if (rv.ClientAlias == 0)
                    {
                        counter++;
                        return;
                    }

                    PushReadData(rv);
                });

            if (counter > 0)
            {
                Debug.WriteLine("OnInformationReport CA=0: " + counter);
                throw new MercuryAccessException("OnInformationReport CA missing, count=" + counter);
            }
        }

        public IEnumerable<IDevice> Register(IEnumerable<DeviceConfig> configs)
        {
            var devices = configs.Select(
                x =>
                {
                    var reg = new XiAccessItemRegistration(x, this);
                    _registrations.Add(reg.ClientAlias, reg);

                    var accessDataType = x.DataType.ToAccessDataType();
                    var deviceType = typeof (IDevice<>).MakeGenericType(accessDataType);
                    var dvc = ServiceLocator.GetInstance(deviceType) as IDevice;

                    if (dvc == null)
                        throw new InvalidOperationException("cannot resolve device");

                    dvc.Init(reg);

                    return dvc;
                });

            return devices;
        }

        public void Read(IEnumerable<uint> serverAliases)
        {
            var rs = _dataList.ReadData(serverAliases.ToList());

            foreach (var rv in rs)
            {
                PushReadData(rv);
            }
        }

        public async Task ReadAsync(IEnumerable<uint> serverAliases)
        {
            if (serverAliases == null)
                throw new ArgumentNullException("serverAliases");

            var aliases = serverAliases.ToList();

            if (!aliases.Any())
                return;

            var result = await _dataList.ReadDataAsync(aliases);

            if (result.Error != null || result.ReadValues == null || (!result.ReadValues.Any()))
            {
                throw new Exception("AsyncRead failed");
            }

            foreach (var rv in result.ReadValues)
            {
                PushReadData(rv);
            }
        }

        public IObservable<IList<ReadData>> DirectAsyncRead(IEnumerable<uint> serverAliases)
        {
            var subject = new Subject<IList<ReadData>>();

            _dataList.ReadData(serverAliases.ToList(),
                (error, readValues, o) =>
                {
                    if ((error != null) || (readValues == null) || (!readValues.Any()))
                        throw new Exception("AsyncReadBlockDirectly failed");


                    var readDatas = readValues.Select(GetReadData).ToList();

                    var subject2 = (Subject<IList<ReadData>>) o;
                    subject2.OnNext(readDatas);
                    subject2.OnCompleted();
                },
                subject);
            return subject.Take(1);
        }

        public ReadData DirectRead(IAccessItemRegistration reg)
        {
            return _dataList.ReadDataEx(reg);
        }

        public IEnumerable<ReadData> DirectReadBlock(IEnumerable<IAccessItemRegistration> regs)
        {
            return _dataList.ReadDataEx(regs);
        }

        public void Write(IEnumerable<WriteData> writeDatas)
        {
            _dataList.WriteData(writeDatas);
        }

        public Task WriteAsync(IEnumerable<WriteData> writeDatas)
        {
           return _dataList.WriteDataAsync(writeDatas);
        }

        public IObservable<Unit> AsyncWrite(WriteData writeData)
        {
            var subject = new Subject<Unit>();

            var serverAliasByClientAlias = writeData.Registration.ServerAlias;

            Debug.WriteLine("AsyncWrite: \t" + DateTime.Now + ", id:" + serverAliasByClientAlias);

            _dataList.WriteData(
                writeData,
                (error, results, o) =>
                {
                    Debug.WriteLine("\tAsyncWrite.Callback: \t" + DateTime.Now + ", id:" + serverAliasByClientAlias);
                    if (error != null)
                    {
                        Debug.WriteLine("\tAsyncWrite.Callback.Error: \t" + DateTime.Now + ", id:" +
                                        serverAliasByClientAlias);
                        throw new Exception("AsyncWrite failed");
                    }
                    //                        var registrations = results.Select(v => _registerInfoDict[v.ClientAlias]);
                    var subject2 = (Subject<Unit>) o;
                    subject2.OnNext(new Unit());
                    subject2.OnCompleted();
                },
                subject);

            return subject.Take(1);
        }

        public IObservable<Unit> AsyncWriteBlock(IEnumerable<WriteData> writeDatas)
        {
            var writeDatas2 = writeDatas.ToList();
            var subject = new Subject<Unit>();

            _dataList.WriteData(writeDatas2,
                (error, results, o) =>
                {
                    if (error != null)
                        throw new Exception("AsyncWriteBlock failed");

                    var subject2 = (Subject<Unit>) o;
                    subject2.OnNext(new Unit());
                    subject2.OnCompleted();
                },
                subject);

            return subject.Take(1);
        }


        public async Task AddToUpdateListAsync(IEnumerable<uint> clientAliases)
        {
            var unusedRegs = clientAliases.Select(ca => _registrations[ca]).ToList();

            List<ListInstanceDef> toAdd = unusedRegs.Select(x => x.ListInstanceDef).ToList();

            var rslt = await _dataList.AddDataObjectsToListAsync(toAdd);

            if (rslt.Error != null)
            {
                Debug.WriteLine(rslt.Error.Message);
                throw new Exception("OnAddDataObjectsToListComplete failed", rslt.Error);
            }

            if (rslt.AddDataObjectResults == null)
            {
                return;
            }

            rslt.AddDataObjectResults.ForEach(
                p =>
                {
                    var registerInfo = _registrations[p.ClientAlias];
                    registerInfo.ServerDataType = p.DataTypeId.LocalId;
                    registerInfo.ServerAlias = p.ServerAlias;

                    if (registerInfo.ServerDataType == null)
                        throw new Exception("registerInfo.ServerDataType==null");
                });
        }

        public void AddToUpdateList(IEnumerable<uint> clientAliases)
        {
            var toAdd = clientAliases
                .Select(ca => _registrations[ca].ListInstanceDef)
                .ToList();

            if (!toAdd.Any())
                return;

            _dataList
                .AddDataObjectsToList(toAdd)
                .ForEach(
                    p =>
                    {
                        var registerInfo = _registrations[p.ClientAlias];
                        registerInfo.ServerDataType = p.DataTypeId.LocalId;
                        registerInfo.ServerAlias = p.ServerAlias;
                    });
        }

        public void RemoveFromUpdateList(IEnumerable<uint> registrations)
        {
            var toRemove = registrations
                .Select(p => _registrations[p].ServerAlias)
                .ToList();

            if (!toRemove.Any())
                return;

            _dataList.RemoveDataObjectsFromList(toRemove);
        }

        private ReadData GetReadData(ReadValue readValue)
        {
            var reg = _registrations[readValue.ClientAlias];
            var readData = readValue.ToReadData(reg);
            return readData;
        }

        private void PushReadData(ReadValue readValue)
        {
            var reg = _registrations[readValue.ClientAlias];
            var readData = readValue.ToReadData(reg);
            reg.OnNext(readData);
        }

        public void Dispose()
        {
            //TODO TBD
        }
    }
}