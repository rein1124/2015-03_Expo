using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace Hdc.Mercury.Communication
{
    public class CommunicationTracking : ICommunicationTracking
    {
        private readonly Subject<ICommunicationStateArgs<ICommunicationState>> _communicationTracker;

        public CommunicationTracking()
        {
            _communicationTracker = new Subject<ICommunicationStateArgs<ICommunicationState>>();
        }

        public IObservable<ICommunicationStateArgs> CommunitactionTracker
        {
            get { return _communicationTracker; }
        }

        public void ReportCommunicationState(ICommunicationStateArgs state)
        {
            var st = state as ICommunicationStateArgs<ICommunicationState>;
            _communicationTracker.OnNext(st);
        }
    }
}
