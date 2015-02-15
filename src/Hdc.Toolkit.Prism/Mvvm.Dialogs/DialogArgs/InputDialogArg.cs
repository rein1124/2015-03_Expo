using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace Hdc.Mvvm.Dialogs
{
    public class InputDialogArg<TData> : MessageDialogArg
    {
        public InputDialogArg(Action cancelAction, Action<TData> confirmAction)
            : base(cancelAction)
        {
            _confirmCommand = new DelegateCommand(() =>
                                                      {
                                                          if (confirmAction != null)
                                                          {
                                                              confirmAction(Data);
                                                          }
                                                          Close();
                                                      });
        }

        private readonly ICommand _confirmCommand;

        public ICommand ConfirmCommand
        {
            get { return _confirmCommand; }
        }

        public TData Data
        {
            get { return (TData) GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data",
                                                                                             typeof (TData),
                                                                                             typeof (
                                                                                                 InputDialogArg<TData>));
    }
}