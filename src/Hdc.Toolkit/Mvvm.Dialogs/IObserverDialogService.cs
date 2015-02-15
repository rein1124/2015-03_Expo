using System;
using System.Collections.Generic;

namespace Hdc.Mvvm.Dialogs
{
    public interface IObserverDialogService<in TData>
    {
        IObserver<TData> Show(TData data);
    }
}