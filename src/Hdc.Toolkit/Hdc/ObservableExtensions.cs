using System;
using System.Reactive.Subjects;

namespace Hdc
{
    public static class ObservableExtensions
    {
        public static IObservable<TChild> SubscribeWith<TSource, TChild>(this IObservable<TSource> observable,
                                                                         Func<IObservable<TChild>> action)
        {
            throw new NotImplementedException("TODO");
/*
            var ob = new Subject<TChild>();
            observable.Subscribe(u =>
                                     {
                                         var childOb = action();
                                         childOb.Subscribe(t => { ob.OnNext(t); });
                                     });
            return ob;*/
        }

        public static IObservable<TChild> SubscribeFluent<TSource, TChild>(
            this IObservable<TSource> observable,
            Func<TSource,IObservable<TChild>> action)
        {
            var ob = new Subject<TChild>();
            observable.Subscribe(u =>
                                     {
                                         var childOb = action(u);
                                         childOb.Subscribe(t => { ob.OnNext(t); });
                                     });
            return ob;
        }
    }
}