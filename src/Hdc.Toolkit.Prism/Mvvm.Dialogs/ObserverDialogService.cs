using System;
using System.Reactive.Subjects;
using Hdc;
using Hdc.Mvvm;
using Hdc.Reactive;
using Microsoft.Practices.Unity;

namespace Hdc.Mvvm.Dialogs
{
    public abstract class ObserverDialogService<T> : RegionDialogService,
                                                         IObserverDialogService<T>
    {
        private Subject<T> _closedEvent;


        private readonly IRetainedSubject<T> _dataWrapper = new NotifyPropertyChangedRetainedSubject<T>();

        [InjectionMethod]
        public virtual void Init()
        {
            _dataWrapper.Subscribe(x => RaisePropertyChanged(() => Data));
        }

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

        protected void Update(T newValue)
        {
            _closedEvent.OnNext(newValue);
        }

        public IObserver<T> Show(T data)
        {
            OnShowing(data);
            
            Data = data;

            ShowDialog();

            if (_closedEvent != null)
            {
                _closedEvent.Dispose();
                _closedEvent = null;
            }

            _closedEvent = new Subject<T>();
            _closedEvent.Subscribe(d => Data = d, Cancel);

            return _closedEvent;
        }

        protected virtual void OnShowing(T data)
        {
        }

//        public IObjectWrapper<T> DataWrapper
//        {
//            get { return _dataWrapper; }
//        }

        public T Data
        {
            get { return _dataWrapper.Value; }
            set { _dataWrapper.Value = value; }
        }
    }
}