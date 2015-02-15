using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Advosol.Paxi;
using Xi.Contracts.Data;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public static class XiServerExtensions
    {
        [Obsolete]
        public static IObservable<XiServerInitateCompleteResult> InitiateEx(this XiServer server)
        {
            var subject = new Subject<XiServerInitateCompleteResult>();

            new Task(() => server.Initiate((e, o) =>
                                           {
                                               var sub = (Subject<XiServerInitateCompleteResult>)o;
                                               sub.OnNext(new XiServerInitateCompleteResult()
                                                          {
                                                              Error = e,
                                                          });
                                               sub.OnCompleted();
                                           }, subject)).Start();


            return subject.Take(1);
        }

        public static Task<XiServerInitateCompleteResult> InitiateAsync(this XiServer server,
            object asyncState = null)
        {
            var t = new TaskCompletionSource<XiServerInitateCompleteResult>();

            server.Initiate((e, o) => t.TrySetResult(new XiServerInitateCompleteResult(e, o)), asyncState);

            return t.Task;
        }
        
        public static Task<OnDefineListCompleteEventArgs> AddDataListAsync(
            this XiServer server,
            bool enableList,
            uint updateRate,
            uint bufferingRate,
            FilterSet filterSet,
            CommunicationPreferences endpoints,
            ListRWAccessMode rwMode,
            SubscriptionUpdateMode subscrMode,
            object asyncState = null)
        {
            var t = new TaskCompletionSource<OnDefineListCompleteEventArgs>();

            //            dynamic xiDataListWrapper = new object();
            var xiDataListWrapper = new XiDataListWrapper();
            xiDataListWrapper.XiDataList = server.AddDataList(
                enableList,
                updateRate,
                bufferingRate,
                filterSet,
                endpoints,
                rwMode,
                subscrMode,
                (error, rslt, a) => t.TrySetResult(new OnDefineListCompleteEventArgs(
                    error,
                    rslt, 
                    xiDataListWrapper.XiDataList, 
                    a)),
                    asyncState);

            return t.Task;
        }

        private class XiDataListWrapper
        {
            public XiDataList XiDataList { get; set; }
        }
    }
}