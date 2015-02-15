using System;
using System.Reactive;

namespace Hdc.Mvvm.Dialogs
{
    public abstract class GeneralInputDialogService<TInputData> :
        GeneralInputOutputDialogService<TInputData, Unit>,
        IGeneralInputDialogService<TInputData>
    {
        protected override Unit GetOutput()
        {
            return new Unit();
        }
    }
}