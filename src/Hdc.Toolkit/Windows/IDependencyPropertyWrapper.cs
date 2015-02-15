using System;

namespace Hdc.Windows
{
    public interface IDependencyPropertyWrapper<T>
    {
        T Object { get; set; }
        event Action<T> ObjectChanged;
    }
}