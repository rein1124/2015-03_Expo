using System;
using System.Reactive.Linq;
using Hdc.Mvvm;
using Microsoft.Practices.Unity;

namespace ODM.Presentation.ViewModels
{
    public class InfoBarViewModel:ViewModel,IViewModel
    {
        [InjectionMethod]
        public void Init()
        {
            Observable
                .Interval(TimeSpan.FromSeconds(1))
                .ObserveOnDispatcher()
                .Subscribe(
                    x =>
                        {
                            var now = DateTime.Now;
                            Date = now.Date;
                            TimeOfDay = now.TimeOfDay;
                            DayOfWeek = now.DayOfWeek;
                        });
        }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (Equals(_date, value)) return;
                _date = value;
                RaisePropertyChanged(() => Date);
            }
        }

        private TimeSpan _timeOfDay;

        public TimeSpan TimeOfDay
        {
            get { return _timeOfDay; }
            set
            {
                if (Equals(_timeOfDay, value)) return;
                _timeOfDay = value;
                RaisePropertyChanged(() => TimeOfDay);
            }
        }

        private DayOfWeek _dayOfWeek;

        public DayOfWeek DayOfWeek
        {
            get { return _dayOfWeek; }
            set
            {
                if (Equals(_dayOfWeek, value)) return;
                _dayOfWeek = value;
                RaisePropertyChanged(() => DayOfWeek);
            }
        }
    }
}