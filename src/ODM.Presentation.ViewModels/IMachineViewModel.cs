using Hdc.Collections.Generic;
using Hdc.Reactive;
using ODM.Domain;

namespace ODM.Presentation.ViewModels
{
    // ReSharper disable InconsistentNaming
    public interface IMachineViewModel : IContextNodeMiddleTerminal<IMachineViewModel, IMachine>
    {
        int ProductionSpeed { get; }

        int TotalCount { get; }
        int TotalAcceptedCount { get; }
        int TotalRejectCount { get; }
        int TotalRejectRate { get; }

        int JobCount { get; }
        int JobAcceptedCount { get; }
        int JobRejectCount { get; }
        int JobRejectRate { get; }
    }

    // ReSharper restore InconsistentNaming
}