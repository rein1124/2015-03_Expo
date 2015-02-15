using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Advosol.Paxi;
using Xi.Contracts.Data;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public static class XiDataListExtensions
    {
        public static void WriteData(this XiDataList dataList,
                                     WriteValue dataObjectToWrite)
        {
            dataList.WriteData(new List<WriteValue> {dataObjectToWrite});
        }

        public static void WriteData(this XiDataList dataList,
                                     WriteData dataObjectToWrite)
        {
            dataList.WriteData(new List<WriteValue> {dataObjectToWrite.ToWriteValue()});
        }

        public static void WriteData(this XiDataList dataList,
                                     WriteValue dataObjectToWrite,
                                     OnWriteDataComplete onComplete,
                                     object asyncState)
        {
            dataList.WriteData(new List<WriteValue> {dataObjectToWrite}, onComplete, asyncState);
        }


        public static void WriteData(this XiDataList dataList,
                                     WriteData dataObjectToWrite,
                                     OnWriteDataComplete onComplete,
                                     object asyncState)
        {
            var xiWriteValue = dataObjectToWrite.ToWriteValue();
            dataList.WriteData(new List<WriteValue> {xiWriteValue}, onComplete, asyncState);
        }


        public static void WriteData(this XiDataList dataList,
                                     IEnumerable<WriteData> dataObjectsToWrite)
        {
            var objectsToWrite = dataObjectsToWrite.ToWriteValues().ToList();
            dataList.WriteData(objectsToWrite);
        }

        public static Task WriteDataAsync(this XiDataList dataList,
                                          IEnumerable<WriteData> dataObjectsToWrite,
                                          object asyncState = null)
        {
            var objectsToWrite = dataObjectsToWrite.ToWriteValues().ToList();

            var t = new TaskCompletionSource<XiDataListWriteDataCompleteResult>();

            dataList.WriteData(objectsToWrite,
                (e, readValues, o) => t.TrySetResult(new XiDataListWriteDataCompleteResult(e, readValues, o)),
                asyncState);

            return t.Task;
        }

        public static void WriteData(this XiDataList dataList,
                                     IEnumerable<WriteData> dataObjectsToWrite,
                                     OnWriteDataComplete onComplete,
                                     object asyncState)
        {
            List<WriteValue> objectsToWrite = dataObjectsToWrite.ToWriteValues().ToList();
            dataList.WriteData(objectsToWrite, onComplete, asyncState);
        }

        private static ReadValue ReadData(this XiDataList dataList,
                                          uint serverAlias)
        {
            var aliases = new List<uint> {serverAlias};
            var readValues = dataList.ReadData(aliases);
            return readValues.First();
        }

        private static ReadValue ReadData(this XiDataList dataList,
                                          IAccessItemRegistration registration)
        {
            return dataList.ReadData(registration.ServerAlias);
        }

        public static ReadData ReadDataEx(this XiDataList dataList,
                                          IAccessItemRegistration reg)
        {
            ReadValue readValue = dataList.ReadData(reg.ServerAlias);
            var rd = readValue.ToReadData(reg);
            return rd;
        }

        private static List<ReadValue> ReadData(this XiDataList dataList,
                                                IEnumerable<uint> serverAliases)
        {
            var aliases = serverAliases.ToList();
            var readValues = dataList.ReadData(aliases);
            return readValues;
        }

        public static Task<XiDataListReadDataCompleteResult> ReadDataAsync(this XiDataList dataList,
                                                                           IEnumerable<uint> serverAliases,
                                                                           object asyncState = null)
        {
            var t = new TaskCompletionSource<XiDataListReadDataCompleteResult>();

            dataList.ReadData(serverAliases.ToList(),
                (e, readValues, o) => t.TrySetResult(new XiDataListReadDataCompleteResult(e, readValues, o)),
                asyncState);

            return t.Task;
        }

        private static List<ReadValue> ReadData(this XiDataList dataList,
                                                IEnumerable<IAccessItemRegistration> registrations)
        {
            return dataList.ReadData(registrations.Select(x => x.ServerAlias));
        }

        /// <summary>
        /// Convert ReadValue to ReadData with Convertion (like LinearConvert)
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="registrations"></param>
        /// <returns></returns>
        public static IEnumerable<ReadData> ReadDataEx(this XiDataList dataList,
                                                       IEnumerable<IAccessItemRegistration> registrations)
        {
            var regs = registrations.ToList();
            var dic = new Dictionary<uint, IAccessItemRegistration>();
            regs.ForEach(reg => dic.Add(reg.ClientAlias, reg));

            var readValues = dataList.ReadData(regs.Select(x => x.ServerAlias));
            foreach (var readValue in readValues)
            {
                var reg = dic[readValue.ClientAlias];
                yield return readValue.ToReadData(reg);
            }
        }


        public static void ReadData(this XiDataList dataList,
                                    IAccessItemRegistration registration,
                                    OnReadDataComplete onComplete,
                                    object asyncState)
        {
            var list = new List<uint> {registration.ServerAlias};
            dataList.ReadData(list, onComplete, asyncState);
        }

        public static void ReadData(this XiDataList dataList,
                                    IEnumerable<IAccessItemRegistration> registrations,
                                    OnReadDataComplete onComplete,
                                    object asyncState)
        {
            var list = registrations.Select(x => x.ServerAlias).ToList();
            dataList.ReadData(list, onComplete, asyncState);
        }

        public static IObservable<OnInformationReportArgs> GetOnInformationReportEvent(this XiDataList dataList)
        {
            /*var ob = Observable.FromEvent<xiInformationReport>(
                    ev => dataList.OnInformationReport += ev,
                    ev => dataList.OnInformationReport -= ev);

            return ob;*/
            throw new NotImplementedException();
        }

        public static IObservable<XiGeneralNotificationArgs> GetOnPollErrorEvent(this XiDataList dataList)
        {
            /*var ob = Observable.FromEvent<
                XiGeneralNotification,
                XiGeneralNotificationArgs>(
                    ev => dataList.OnPollError += ev,
                    ev => dataList.OnPollError -= ev);

            return ob;*/
            throw new NotImplementedException();
        }

        public static Task<XiListBaseAddDataObjectsToListCompleteResult> AddDataObjectsToListAsync(
            this XiListBase dataList,
            List<ListInstanceDef> instanceDefs,
            object asyncState = null)
        {
            var t = new TaskCompletionSource<XiListBaseAddDataObjectsToListCompleteResult>();

            dataList.AddDataObjectsToList(instanceDefs,
                (e, rslt, o) => t.TrySetResult(new XiListBaseAddDataObjectsToListCompleteResult(e, rslt, o)),
                asyncState);

            return t.Task;
        }
    }
}