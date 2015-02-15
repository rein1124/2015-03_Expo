using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Hdc.Mvvm;

using Microsoft.Practices.Unity;

namespace Hdc.Mvvm.Dialogs
{
    public abstract class GeneralOutputDialogService<TOutputData> : DialogServiceBase<TOutputData>,
                                                                    IGeneralOutputDialogService<TOutputData>
    {
        public IObservable<DialogEventArgs<TOutputData>> Show()
        {
            OnShowing();

            return ShowModalDialog();
        }

        protected virtual void OnShowing()
        {
        }
    }
}