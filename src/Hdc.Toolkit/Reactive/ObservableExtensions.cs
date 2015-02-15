using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Joins;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using System.Windows.Threading;

namespace Hdc.Reactive
{
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



        public static IObservable<T> ObserveOnApplicationDispatcher<T>(this IObservable<T> observable,
            DispatcherPriority priority =
                DispatcherPriority.Loaded)
        {
            if (observable == null)
                throw new NullReferenceException();

            return observable.ObserveOn(Application.Current.Dispatcher, priority);
        }

        public static IObservable<T> ObserveOnCurrentDispatcher<T>(this IObservable<T> observable,
            DispatcherPriority priority = DispatcherPriority.Loaded)
        {
            if (observable == null)
                throw new NullReferenceException();

            return observable.ObserveOn(Dispatcher.CurrentDispatcher, priority);
        }

        public static IObservable<T> ObserveOn<T>(this IObservable<T> observable, Dispatcher dispatcher,
            DispatcherPriority priority = DispatcherPriority.Loaded)
        {
            if (observable == null)
                throw new NullReferenceException();

            if (dispatcher == null)
                throw new ArgumentNullException("dispatcher");

            return Observable.Create<T>(
                o =>
                {
                    return observable.Subscribe(
                        obj => dispatcher.Invoke((Action) (() => o.OnNext(obj)), priority),
                        ex => dispatcher.Invoke((Action) (() => o.OnError(ex)), priority),
                        () => dispatcher.Invoke((Action) (() => o.OnCompleted()), priority));
                });
        }

        public static IObservable<T> ObserveOnApplicationDispatcherAsync<T>(this IObservable<T> observable,
            DispatcherPriority priority =
                DispatcherPriority.Loaded)
        {
            if (observable == null)
                throw new NullReferenceException();

            return observable.ObserveOnAsync(Application.Current.Dispatcher, priority);
        }

        public static IObservable<T> ObserveOnDispatcherAsync<T>(this IObservable<T> observable,
            DispatcherPriority priority = DispatcherPriority.Loaded)
        {
            if (observable == null)
                throw new NullReferenceException();

            return observable.ObserveOnAsync(Dispatcher.CurrentDispatcher, priority);
        }

        public static IObservable<T> ObserveOnAsync<T>(this IObservable<T> observable, Dispatcher dispatcher,
            DispatcherPriority priority = DispatcherPriority.Loaded)
        {
            if (observable == null)
                throw new NullReferenceException();

            if (dispatcher == null)
                throw new ArgumentNullException("dispatcher");

            return Observable.Create<T>(
                o =>
                {
                    return observable.Subscribe(
                        obj => dispatcher.BeginInvoke((Action) (() => o.OnNext(obj)), priority),
                        ex => dispatcher.BeginInvoke((Action) (() => o.OnError(ex)), priority),
                        () => dispatcher.BeginInvoke((Action) (() => o.OnCompleted()), priority));
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

        public static IObservable<bool> WhereTrue(this IObservable<bool> observable)
        {
            return observable.Where(x => x == true);
        }

        public static IObservable<bool> WhereFalse(this IObservable<bool> observable)
        {
            return observable.Where(x => x == false);
        }
    }
}