using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hdc.Mercury.Communication
{
    public interface ICommunicationTracking
    {
        IObservable<ICommunicationStateArgs> CommunitactionTracker { get; }

        void ReportCommunicationState(ICommunicationStateArgs state);
    }
}
