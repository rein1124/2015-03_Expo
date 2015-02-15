using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Hdc.Mvvm.Dialogs;

namespace Hdc.Mvvm.Dialogs
{
    public class MessageDialogService : RegionDialogService<Unit>, IMessageDialogService
    {
//        private const string MessageDialog = "MessageDialog";

//        private Subject<Unit> _subject = new Subject<Unit>();

        [InjectionMethod]
        public void Init()
        {
            CloseCommand = new DelegateCommand(
                () =>
                    {
                        Close(Unit.Default);
                    });
        }

        public DelegateCommand CloseCommand { get; set; }

        public IObservable<DialogEventArgs<Unit>> Show(string message)
        {
            Message = message;
            return base.Show();
        }

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
            return HdcDialogNames.MessageDialog;
        }

        protected override string GetDialogViewName()
        {
            return HdcDialogNames.MessageDialog;
        }

        public void Close()
        {
            Close(Unit.Default);
        }
    }
}