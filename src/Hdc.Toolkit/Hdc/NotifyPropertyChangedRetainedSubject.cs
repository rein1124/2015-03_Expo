using System;
using System.ComponentModel;
using Hdc.Reactive;

namespace Hdc
{
    public class NotifyPropertyChangedRetainedSubject<T> : RetainedSubject<T>, INotifyPropertyChanged
    {
        public NotifyPropertyChangedRetainedSubject()
        {
            Subject.Subscribe(x => OnPropertyChanged("Value"));
        }

        public NotifyPropertyChangedRetainedSubject(T initialValue = default(T)) : base(initialValue)
        {
            Subject.Subscribe(x => OnPropertyChanged("Value"));
        }

        public event PropertyChangedEventHandler PropertyChanged;



        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}