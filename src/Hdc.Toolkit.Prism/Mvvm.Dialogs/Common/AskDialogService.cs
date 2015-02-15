using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Hdc.Mvvm.Dialogs;

namespace Hdc.Mvvm.Dialogs
{
    public class AskDialogService : RegionDialogService<bool>, IAskDialogService
    {
        [InjectionMethod]
        public void Init()
        {
            YesCommand = new DelegateCommand(
                () => { Close(true); });

            NoCommand = new DelegateCommand(
                () => { Close(false); });
        }

        public IObservable<DialogEventArgs<bool>> Show(string inputData)
        {
            Message = inputData;

            return Show();
        }

        public DelegateCommand YesCommand { get; set; }

        public DelegateCommand NoCommand { get; set; }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                if (Equals(_message, value)) return;
                _message = value;
                RaisePropertyChanged(() => Message);
            }
        }

        protected override string GetDialogRegionName()
        {
            return HdcDialogNames.AskDialog;
        }

        protected override string GetDialogViewName()
        {
            return HdcDialogNames.AskDialog;
        }
    }
}