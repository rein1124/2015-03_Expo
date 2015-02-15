using System;
using System.Linq;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Windows.Input;
using Hdc.Mvvm;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;

namespace Hdc.Mvvm.Dialogs
{
    public abstract class DialogServiceBase<TOutputData> : ViewModel
    {
        protected IObservable<DialogEventArgs<TOutputData>> ShowModalDialog()
        {
            OnDialogShowing();
            _modalDialogViewModel.Show();
            var closedEvent = _closedEvent.Take(1);

//            closedEvent.Subscribe(x => _modalDialogViewModel.Close());
            return closedEvent;
        }

        private IModalDialogViewModel _modalDialogViewModel;

        private readonly Subject<DialogEventArgs<TOutputData>> _closedEvent =
            new Subject<DialogEventArgs<TOutputData>>();

        [InjectionMethod]
        public virtual void Init()
        {
            _modalDialogViewModel = GetModalDialogViewModel();

            ConfirmCommand = new DelegateCommand(
                Confirm,
                CheckCanConfirmCommandExecute);

            CancelCommand = new DelegateCommand(
                Cancel,
                CheckCanCancelCommandExecute);

            OnInit();
        }

        protected virtual void OnInit()
        {
            
        }

        protected virtual bool CheckCanConfirmCommandExecute()
        {
            return true;
        }

        protected virtual bool CheckCanCancelCommandExecute()
        {
            return true;
        }

        public ICommand ConfirmCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        protected abstract IModalDialogViewModel GetModalDialogViewModel();

//        protected virtual void OnShowing()
//        {
//
//        }
//        protected IModalDialogViewModel ModalDialogViewModel
//        {
//            get { return _modalDialogViewModel; }
//        }

        protected abstract TOutputData GetOutput();

        protected virtual bool CheckCouldConfirm()
        {
            return true;
        }

        protected virtual bool CheckCouldCancel()
        {
            return true;
        }

        public void Confirm()
        {
            if (!CheckCouldConfirm()) return;

//            new Task(() =>
//                         {
            OnDialogClosing();

            BeforeOutput();
            var output = GetOutput();
            AfterOutput();
            _modalDialogViewModel.Close();
            _closedEvent.OnNext(new DialogEventArgs<TOutputData> {Data = output});
//                         }).Start();
        }

        public void Cancel()
        {
            if (!CheckCouldCancel()) return;

//            new Task(() =>
//                         {
            OnDialogClosing();
            _modalDialogViewModel.Close();
            _closedEvent.OnNext(new DialogEventArgs<TOutputData> {IsCanceled = true});
//                         }).Start();
        }

        protected virtual void OnDialogShowing()
        {
        }

        protected virtual void OnDialogClosing()
        {
        }

        protected virtual void BeforeOutput()
        {
        }

        protected virtual void AfterOutput()
        {
        }
    }
}