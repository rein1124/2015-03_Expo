using System;
using System.Reactive.Linq;
using Hdc;
using Hdc.Mercury;
using Hdc.Mvvm;
using Hdc.Reactive;

namespace Hdc.Mercury
{
    public static class MonitorExtensions
    {
        public static void Sync<T>(this IValueMonitor<T> valueMonitor, IValueObservable<T> ob)
        {
            valueMonitor.Observe(ob, ob.Value, x => { ob.Value = x; });
        }

        public static void Sync<TSource, TTarget>(this IValueMonitor<TTarget> valueMonitor,
                                                  IValueObservable<TSource> ob,
                                                  Func<TSource, TTarget> convertToTarget,
                                                  Func<TTarget, TSource> convertToSource)
        {
            valueMonitor.Observe(ob.Select(convertToTarget),
                                 convertToTarget(ob.Value),
                                 x => { ob.Value = convertToSource(x); });
        }

        public static void SyncReadOnly<TSource, TTarget>(this IValueMonitor<TTarget> valueMonitor,
                                                          IValueObservable<TSource> ob,
                                                          Func<TSource, TTarget> convertToTarget)
        {
            valueMonitor.Observe(ob.Select(convertToTarget),
                                 convertToTarget(ob.Value));
        }
    }
}