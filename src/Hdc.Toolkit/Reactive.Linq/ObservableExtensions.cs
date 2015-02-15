using System.Collections.Generic;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Joins;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Hdc.Patterns;

namespace Hdc.Reactive.Linq
{
    /// <summary>
    /// ref: 
    ///     How can I get an IObservableT in Rx from a ¡°non-standard¡± event
    ///     how-can-i-get-an-iobservablet-in-rx-from-a-non-standard-event.htm
    /// </summary>
    public static class ObservableExtensions
    {  
        public static IObservable<ICustomEvent<TSource, TArgs>> FromCustomEvent<TSource, TArgs>(
            Action<Action<TSource, TArgs>> addHandler,
            Action<Action<TSource, TArgs>> removeHandler)
        {
            return Observable.Create<ICustomEvent<TSource, TArgs>>(
                observer =>
                    {
                        Action<TSource, TArgs> eventHandler =
                            (s, a) =>
                                {
                                    var customEvent = new CustomEvent<TSource, TArgs>(s, a);
                                    observer.OnNext(customEvent);
                                };

                        addHandler(eventHandler);

                        return Disposable.Create(() => removeHandler(eventHandler));
                    });
        }
        
        //        public static IObservable<Unit> SubscribeWith(this IObservable<Unit> observable, Action action)
        //        {
        //            var ob = new Subject<Unit>();
        //            observable.Subscribe(u =>
        //                                     {
        //                                         action();
        //                                         ob.OnNext(new Unit());
        //                                     });
        //            return ob;
        //        }

        public static IObservable<TChild> SubscribeWith<TSource, TChild>(this IObservable<TSource> observable,
                                                                         Func<IObservable<TChild>> action)
        {
            throw new NotImplementedException("TODO");

         /*   var ob = new Subject<TChild>();
            observable.Subscribe(u =>
                                     {
                                         var childOb = action();
                                         childOb.Subscribe(t =>
                                                               {
                                                                   ob.OnNext(t);
                                                               });
                                     });
            return ob;*/
        }

        public static IObservable<TResult> Join<TResult>(this Plan<TResult> plan)
        {
            return Observable.When(plan);
        }

//        public static IObservable<TResult> Join<TResult>(this Plan<TResult> plan)
//        {
//            return Observable.When(plan);
//        }



        public static IObservable<T> ObserveOnTaskPool<T>(this IObservable<T> observable)
        {
            if (observable == null)
                throw new NullReferenceException();

            return observable.ObserveOn(TaskPoolScheduler.Default);
        }

        public static IObservable<Unit> ZipToUnit<T>(this System.IObservable<T> first, System.IObservable<T> second)
        {
            return Observable.Zip(first, second, (x, y) => new Unit());
        }
    }
}