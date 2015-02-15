using System;
using System.Reactive.Subjects;

namespace Hdc.Mvvm.Dialogs
{
    public abstract class SequenceRegionDialogService<T> : RegionDialogService, ISeqenceOutputDialogService<T>
    {
        private Subject<T> _closedEvent;

        public Subject<T> ClosedEvent
        {
            get { return _closedEvent; }
        }

        public void Cancel()
        {
            CloseDialog();
            ClosedEvent.OnCompleted();
            ClosedEvent.Dispose();
        }

        public IObservable<T> Show()
        {
            ShowDialog();

            if (_closedEvent != null)
            {
                _closedEvent.Dispose();
                _closedEvent = null;
            }
            _closedEvent = new Subject<T>();

            return _closedEvent;
        }

        protected void Update(T newValue)
        {
            _closedEvent.OnNext(newValue);
        }
    }
}