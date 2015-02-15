using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Hdc.Mvvm.Dialogs
{
    public abstract class RegionDialogService<T> : RegionDialogService, IGeneralOutputDialogService<T>
    {
        private readonly Subject<DialogEventArgs<T>> _closedEvent = new Subject<DialogEventArgs<T>>();

        public Subject<DialogEventArgs<T>> ClosedEvent
        {
            get { return _closedEvent; }
        }

        public void Close(T data)
        {
            CloseDialog();
            ClosedEvent.OnNext(new DialogEventArgs<T>(data));
        }

        public void Cancel()
        {
            CloseDialog();
            ClosedEvent.OnNext(new DialogEventArgs<T>(true));
        }

        public IObservable<DialogEventArgs<T>> Show()
        {
            ShowDialog();
            return _closedEvent.Take(1);
        }
    }
}