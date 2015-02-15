/*using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Windows;

namespace Hdc
{
    public class DependencyPropertyObjectWrapper<T> : DependencyObject, IObjectWrapper<T>
    {
        private readonly Subject<T> _objectChangedEvent = new Subject<T>();


        public T Object
        {
            get { return (T) GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }

        public static readonly DependencyProperty ObjectProperty = DependencyProperty.Register(
            "Object", typeof (T), typeof (DependencyPropertyObjectWrapper<T>), new PropertyMetadata(OnObjectChanged));


        public IObservable<T> ObjectChangedEvent
        {
            get { return _objectChangedEvent; }
        }

        private static void OnObjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as DependencyPropertyObjectWrapper<T>;

            if (obj == null)
                return;

            obj._objectChangedEvent.OnNext((T) e.NewValue);
        }
    }
}*/