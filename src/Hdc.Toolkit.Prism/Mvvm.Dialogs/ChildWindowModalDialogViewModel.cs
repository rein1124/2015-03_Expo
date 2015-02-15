using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Hdc.Mvvm;

namespace Hdc.Mvvm.Dialogs
{
    public class ChildWindowModalDialogViewModel : ViewModel, IModalDialogViewModel
    {
        private bool _isShow;

        public bool IsShow
        {
            get { return _isShow; }
            set
            {
                if (Equals(_isShow, value)) return;
                _isShow = value;
                RaisePropertyChanged(() => IsShow);

//                if (!_isShow)
//                {
//                    _subject.OnNext(new Unit());
//                }
            }
        }

        public void Close()
        {
            IsShow = false;
        }

        private Subject<Unit> _subject = new Subject<Unit>();

        public IObservable<Unit> Show()
        {
            IsShow = true; 
//            new Task(() => { IsShow = true; })
//                .Start();

            return _subject.Take(1);
        }
    }
}