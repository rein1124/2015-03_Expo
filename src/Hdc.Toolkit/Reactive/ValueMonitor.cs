using System;

namespace Hdc.Reactive
{
    public class ValueMonitor<T> : RetainedSubject<T>, IValueMonitor<T>
    {
        private IDisposable _upSubscribe;
        private IDisposable _downSubscribe;

        public ValueMonitor()
        {
        }

        public ValueMonitor(T initialValue)
            : base(initialValue)
        {
        }

        public void Observe(IObservable<T> observable)
        {
            Observe(observable, null);
        }

        public void Observe(IObservable<T> observable, Action<T> action)
        {
            /* if (_upSubscribe != null)
            {
                _upSubscribe.Dispose();
                _upSubscribe = null;
            }

            if (_downSubscribe != null)
            {
                _downSubscribe.Dispose();
                _downSubscribe = null;
            }*/

            Disconnect();

            //_subscribe = source.DistinctUntilChanged().Subscribe(Update);
            _upSubscribe = observable.Subscribe(x =>
                                                {
                                                    Update(x);

                                                    //                                   if (action != null)
                                                    //                                       action(x);
                                                });

            _downSubscribe = this.Subscribe(x => { action(x); });
        }

        public void Observe(IObservable<T> observable, T initialObject)
        {
            //            Observe(observable);
            //
            //            Update(initialObject);

            Observe(observable, initialObject, x => { });
        }

        public void Observe(IObservable<T> observable, T initialObject, Action<T> action)
        {
            Observe(observable, action);

            Update(initialObject);
        }

        public void Disconnect()
        {
            if (_upSubscribe != null)
            {
                _upSubscribe.Dispose();
                _upSubscribe = null;
            }

            if (_downSubscribe != null)
            {
                _downSubscribe.Dispose();
                _downSubscribe = null;
            }
        }
    }
}