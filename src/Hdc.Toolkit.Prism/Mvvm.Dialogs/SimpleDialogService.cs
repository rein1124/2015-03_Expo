using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Windows.Input;

using Microsoft.Practices.Unity;

namespace Hdc.Mvvm.Dialogs
{
    public abstract class SimpleDialogService : GeneralOutputDialogService<Unit>, ISimpleDialogService
    {
        protected override Unit GetOutput()
        {
            return new Unit();
        }
    }
}