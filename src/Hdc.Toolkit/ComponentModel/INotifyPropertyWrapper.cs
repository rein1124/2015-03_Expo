using System.ComponentModel;

namespace Hdc.ComponentModel
{
    public interface INotifyPropertyWrapper<T> : INotifyPropertyChanged
    {
        T Object { get; set; }
    }
}