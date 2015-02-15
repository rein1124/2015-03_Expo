using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Hdc.Mvvm;

namespace Hdc.Mvvm.Dialogs
{
    public class ModalDialogViewModel : IModalDialogViewModel
    {
        private readonly SimplePocoDialogArg _dialogArg;

        private Subject<Unit> _subject;

        public ModalDialogViewModel()
        {
            _dialogArg = new SimplePocoDialogArg(() =>
                                                 {
                                                     if (_subject == null) return;
                                                     _subject.OnNext(new Unit());
                                                     _subject.OnCompleted();
                                                     Reset();
                                                 });
        }

        public SimplePocoDialogArg DialogArg
        {
            get { return _dialogArg; }
        }

        public void Close()
        {
            _dialogArg.Close();
            Reset();
        }

        public IObservable<Unit> Show()
        {
          /*  if (!_dialogArg.CheckAccess())
            {
               var obj = _dialogArg.Dispatcher.Invoke(
                   new Func<IObservable<Unit>>(() =>
                                  {
                                      return this.Show();
                                  }));
                return obj as IObservable<Unit>;
            }*/

            Reset();

            _dialogArg.Show();
            _subject = new Subject<Unit>();
            return _subject.Take(1);
        }

        private void Reset()
        {
            if (_subject != null)
            {
                _subject.OnCompleted();
                _subject = null;
            }
        }
    }
}