using System;

namespace Hdc.Modularity
{
    public interface IComponentItem : IObservable<bool>
    {
        string Name { get; set; }

        bool IsActive { get; set; }

//        IObservable<bool> IsActiveChangedEvent { get; }
    }
}