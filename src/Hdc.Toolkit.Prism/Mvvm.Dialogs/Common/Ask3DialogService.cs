using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;

namespace Hdc.Mvvm.Dialogs
{
    public class Ask3DialogService : RegionDialogService<Ask3DialogResult>, IAsk3DialogService
    {
        [InjectionMethod]
        public void Init()
        {
            YesCommand = new DelegateCommand(
                () => Close(Ask3DialogResult.Yes));

            NoCommand = new DelegateCommand(
                () => Close(Ask3DialogResult.No));

            CancelCommand = new DelegateCommand(
                () => Close(Ask3DialogResult.Cancel));
        }

        public IObservable<DialogEventArgs<Ask3DialogResult>> Show(string inputData)
        {
            Message = inputData;

            return Show();
        }

        public DelegateCommand YesCommand { get; set; }

        public DelegateCommand NoCommand { get; set; }

        public DelegateCommand CancelCommand { get; set; }

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
            return HdcDialogNames.Ask3Dialog;
        }

        protected override string GetDialogViewName()
        {
            return HdcDialogNames.Ask3Dialog;
        }
    }
}