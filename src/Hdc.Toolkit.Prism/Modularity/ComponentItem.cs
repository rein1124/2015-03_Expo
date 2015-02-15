using System;
using System.Collections.Generic;
using Hdc.Reactive;

namespace Hdc.Modularity
{
    public class ComponentItem : IComponentItem
    {
        public string Name { get; set; }

        public bool IsActive
        {
            get { return _isActiveWrapper.Value; }
            set { _isActiveWrapper.Value = value; }
        }

        private readonly RetainedSubject<bool> _isActiveWrapper = new RetainedSubject<bool>();

        public IObservable<bool> IsActiveChangedEvent
        {
            get { return _isActiveWrapper; }
        }

        public ComponentItem() : this(string.Empty)
        {
        }

        public ComponentItem(string name)
        {
            Name = name;
        }

        public IDisposable Subscribe(IObserver<bool> observer)
        {
            return _isActiveWrapper.Subscribe(observer);
        }
    }
}