using System.Collections.Generic;

namespace Hdc.Mvvm
{
    public interface ISelectService<T>:IInputOutputService<IEnumerable<T>,T>
    {
        
    }


}