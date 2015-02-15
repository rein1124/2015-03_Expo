using System;
using System.Reactive.Subjects;

namespace Hdc.Reactive
{
    public class RetainedSubject<T> : IRetainedSubject<T>
    {
        private T _value;

        private readonly ISubject<T> _subject = new Subject<T>();

        protected ISubject<T> Subject
        {
            get { return _subject; }
        }

        public RetainedSubject()
        {
        }

        public RetainedSubject(T initialValue = default(T))
        {
            //_subject = new Subject<T>();
            //            _subject.Subscribe(x =>
            //                                   {
            //                                       _object = x;
            //                                       onChanged(x);
            //                                   });

            _value = initialValue;
        }

        //        public ObjectObservable(
        //                                IObservable<T> source,
        //                                T initialObject = default(T))
        //            : this(onChanged, initialObject)
        //        {
        //            Observe(source, initialObject);
        //        }


        public T Value
        {
            get { return _value; }
            set { Update(value); }
        }

        protected virtual void Update(T @object)
        {
            if (Equals(_value, @object))
                return;

            _value = @object;
            _subject.OnNext(@object);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _subject.Subscribe(observer);
        }

        public void OnNext(T value)
        {
            _value = value;
            _subject.OnNext(value);
        }

        public void OnError(Exception error)
        {
            _subject.OnError(error);
        }

        public void OnCompleted()
        {
            _subject.OnCompleted();
        }
    }
}