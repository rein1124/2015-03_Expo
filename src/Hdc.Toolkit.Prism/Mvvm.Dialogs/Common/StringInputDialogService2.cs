using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Hdc.Mvvm.Dialogs;

namespace Hdc.Mvvm.Dialogs
{
    public class StringInputDialogService : RegionDialogService<string>, IStringInputDialogService
    {
        [InjectionMethod]
        public void Init()
        {
            CancelCommand = new DelegateCommand(
                () => { Cancel(); });

            ConfirmCommand = new DelegateCommand(
                () => { Close(CurrentString); });
        }


        public IObservable<DialogEventArgs<string>> Show(string title, string defaultString)
        {
            return Show(title, defaultString, Int32.MaxValue);
        }

        public IObservable<DialogEventArgs<string>> Show(string title, string defaultString, int maxLength)
        {
            Title = title;
            CurrentString = defaultString;

            return Show();
        }

        public DelegateCommand ConfirmCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                if (Equals(_title, value)) return;
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        private string _currentString;

        public string CurrentString
        {
            get { return _currentString; }
            set
            {
                if (Equals(_currentString, value)) return;
                _currentString = value;
                RaisePropertyChanged(() => CurrentString);
            }
        }

        protected override string GetDialogRegionName()
        {
            return HdcDialogNames.StringInput;
        }

        protected override string GetDialogViewName()
        {
            return HdcDialogNames.StringInput;
        }
    }
}