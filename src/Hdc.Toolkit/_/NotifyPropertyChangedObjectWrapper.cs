using System.Collections.Generic;
using System.Reactive.Subjects;

namespace Hdc
{
//    public class NotifyPropertyChangedObjectWrapper<T> : IObjectWrapper<T>, INotifyPropertyChanged
//    {
//        private T _object;
//
//        private readonly Subject<T> _objectChangedEvent = new Subject<T>();
//
//        public event PropertyChangedEventHandler PropertyChanged;
//
//        public T Object
//        {
//            get { return _object; }
//            set
//            {
//                if (Equals(_object, value))
//                    return;
//
//                _object = value;
//
//                _objectChangedEvent.OnNext(value);
//
//                if (PropertyChanged != null)
//                    PropertyChanged(this, new PropertyChangedEventArgs("Object"));
//            }
//        }
//
//        public IObservable<T> ObjectChangedEvent
//        {
//            get { return _objectChangedEvent; }
//        }
//    }
}