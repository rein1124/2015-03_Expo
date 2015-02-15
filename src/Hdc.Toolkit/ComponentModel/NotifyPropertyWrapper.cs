using System.ComponentModel;

namespace Hdc.ComponentModel
{
    public class NotifyPropertyWrapper<T> : INotifyPropertyWrapper<T>
    {
        private T _o;
        public event PropertyChangedEventHandler PropertyChanged;

        public T Object
        {
            get { return _o; }
            set
            {
                if (Equals(_o, value)) return;
                _o = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Object"));
            }
        }
    }
}