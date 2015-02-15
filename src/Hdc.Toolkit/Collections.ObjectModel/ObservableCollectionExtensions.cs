using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace Hdc.Collections.ObjectModel
{
    /// <summary>
    /// ref: How to force Subscribe to hook without delay
    /// http://social.msdn.microsoft.com/Forums/en-US/rx/thread/d0ae7c92-c678-45a7-aa8b-7a154f21ac4d
    /// </summary>
    public static class ObservableCollectionExtensions
    {
        //TODO refactor two GetAddedItems
        public static IObservable<T> GetAddedItems<T>(this ObservableCollection<T> collection)
        {
            var notificationObservable = Observable.FromEventPattern
                <NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                    h => new NotifyCollectionChangedEventHandler(h),
                    handler => collection.CollectionChanged += handler,
                    handler => collection.CollectionChanged -= handler);

            var selection = notificationObservable
                .SelectMany(arg =>
                                {
                                    if (arg.EventArgs.Action == NotifyCollectionChangedAction.Add)
                                    {
                                        return (from newItem in arg.EventArgs.NewItems.Cast<T>()
                                                select newItem).ToObservable();
                                    }
                                    return Observable.Empty<T>();
                                });

            return selection;
        }

        //TODO refactor two GetAddedItems
        public static IObservable<T> GetAddedItems<T>(
            this IObservable<EventPattern<NotifyCollectionChangedEventArgs>> notificationObservable)
        {
            var selection = notificationObservable
                .SelectMany(arg =>
                                {
                                    if (arg.EventArgs.Action == NotifyCollectionChangedAction.Add)
                                    {
                                        return (from newItem in arg.EventArgs.NewItems.Cast<T>()
                                                select newItem).ToObservable();
                                    }
                                    return Observable.Empty<T>();
                                });

            return selection;
        }

        //TODO refactor two GetRemovedItems
        public static IObservable<T> GetRemovedItems<T>(this ObservableCollection<T> collection)
        {
            var notificationObservable = Observable.FromEventPattern
                <NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                    h => new NotifyCollectionChangedEventHandler(h),
                    handler => collection.CollectionChanged += handler,
                    handler => collection.CollectionChanged -= handler);

            var selection = notificationObservable
                .SelectMany(arg =>
                                {
                                    if (arg.EventArgs.Action == NotifyCollectionChangedAction.Remove ||
                                        arg.EventArgs.Action == NotifyCollectionChangedAction.Replace)
                                    {
                                        return (from newItem in arg.EventArgs.OldItems.Cast<T>()
                                                select newItem).ToObservable();
                                    }
                                    return Observable.Empty<T>();
                                });

            return selection;
        }

        //TODO refactor two GetRemovedItems
        public static IObservable<T> GetRemovedItems<T>(
            this IObservable<EventPattern<NotifyCollectionChangedEventArgs>> notificationObservable)
        {
            var selection = notificationObservable
                .SelectMany(arg =>
                                {
                                    if (arg.EventArgs.Action == NotifyCollectionChangedAction.Remove ||
                                        arg.EventArgs.Action == NotifyCollectionChangedAction.Replace)
                                    {
                                        return (from newItem in arg.EventArgs.OldItems.Cast<T>()
                                                select newItem).ToObservable();
                                    }
                                    return Observable.Empty<T>();
                                });

            return selection;
        }

        
        public static IObservable<EventPattern<NotifyCollectionChangedEventArgs>> GetCollectionChangedEvent(
            this INotifyCollectionChanged collection)
        {
            var collectionChangedEvent = Observable.FromEventPattern<
                NotifyCollectionChangedEventHandler,
                NotifyCollectionChangedEventArgs>(
                    h =>
                        {
                            EventHandler<NotifyCollectionChangedEventArgs> eventHandler1 = h;
                            return new NotifyCollectionChangedEventHandler(eventHandler1);
                        },
                    x => collection.CollectionChanged += x,
                    x => collection.CollectionChanged -= x);

            return collectionChangedEvent;
        }


        // add, 2014-04-14, rein
        // ref: http://social.msdn.microsoft.com/Forums/vstudio/en-US/2e278e3c-27ab-42b5-8a7b-6828ddbb9caf/how-to-sync-two-observable-collection-?forum=wpf
        // cannot use with Caliburn.Micro BindinableCollection
        public static void Sync<T>(this IObservableCollection<T> target, INotifyCollectionChanged source)
        {
            source.CollectionChanged += (sender, e) => {
//                target.CollectionChanged -= coll2_CollectionChanged;

                if (e.NewItems != null)
                    foreach (var newItem in e.NewItems)
                        target.Add((T)newItem);

                if (e.OldItems != null)
                    foreach (var oldItem in e.OldItems)
                        target.Remove((T)oldItem);

//                target.CollectionChanged += coll2_CollectionChanged;
            };
        }
    }
}