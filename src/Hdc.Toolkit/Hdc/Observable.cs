// 2010-09-14
// rein
// ref to http://stackoverflow.com/questions/1768974/implementing-iobservablet-from-scratch
// TODO FSharpMap cannot be used, using Dictionary<> instead, but have risk
// 

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hdc
{
    internal class Observable<T> : IObservable<T>, IDisposable where T : EventArgs
    {
        private Dictionary<int, IObserver<T>> subscribers = new Dictionary<int, IObserver<T>>();
        private readonly object thisLock = new object();
        private int key;
        private bool isDisposed;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                OnCompleted();
                isDisposed = true;
            }
        }

        protected void OnNext(T value)
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException("Observable<T>");
            }

            foreach (IObserver<T> observer in subscribers.Select(kv => kv.Value))
            {
                observer.OnNext(value);
            }
        }

        protected void OnError(Exception exception)
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException("Observable<T>");
            }

            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            foreach (IObserver<T> observer in subscribers.Select(kv => kv.Value))
            {
                observer.OnError(exception);
            }
        }

        protected void OnCompleted()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException("Observable<T>");
            }

            foreach (IObserver<T> observer in subscribers.Select(kv => kv.Value))
            {
                observer.OnCompleted();
            }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException("observer");
            }

            lock (thisLock)
            {
                int k = key++;
                subscribers.Add(k, observer);
                var anonymousDisposable = new AnonymousDisposable(
                    () =>
                        {
                            lock (thisLock)
                            {
                                subscribers.Remove(k);
                            }
                        });
                return anonymousDisposable;
            }
        }
    }
}