// 2010-09-14
// rein
// ref to http://stackoverflow.com/questions/1768974/implementing-iobservablet-from-scratch

using System;

namespace Hdc
{
    public class AnonymousObservable<T> : IObservable<T>
    {
        private Func<IObserver<T>, IDisposable> _subscribe;

        public AnonymousObservable(Func<IObserver<T>, IDisposable> subscribe)
        {
            _subscribe = subscribe;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _subscribe(observer);
        }
    }
}