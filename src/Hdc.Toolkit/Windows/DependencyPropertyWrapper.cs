namespace Hdc.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using ComponentModel;

    public class DependencyPropertyWrapper<T> : DependencyObject, IDependencyPropertyWrapper<T>
        //, INotifyPropertyChanged
    {
        public T Object
        {
            get { return (T) GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }

        public static readonly DependencyProperty ObjectProperty = DependencyProperty.Register(
            "Object", typeof (T), typeof (DependencyPropertyWrapper<T>), new PropertyMetadata(OnCallback));

        private static void OnCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as DependencyPropertyWrapper<T>;
            if (obj == null)
                return;

            if (obj.ObjectChanged != null)
                obj.ObjectChanged((T) e.NewValue);
        }

        public event Action<T> ObjectChanged;
    }
}