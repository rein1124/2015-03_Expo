using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;

namespace Hdc.Mvvm.Dialogs
{
    public class CalculateDialogService : RegionDialogService<double>, ICalculateDialogService
    {
        [InjectionMethod]
        public void Init()
        {
            CancelCommand = new DelegateCommand(
                () =>
                    {
                        DefaultValue = null;
                        CurrentValue = null;

                        Cancel();
                    });

            ConfirmCommand = new DelegateCommand(
                () => { Close(CurrentValue.Value); });
        }

        public IObservable<DialogEventArgs<double>> Show(double minValue,
                                                         double maxValue,
                                                         double unitValue,
                                                         double defaultValue,
                                                         string valueName)
        {
            MaxValue = maxValue;
            MinValue = minValue;
            Unit = unitValue;
            ValueName = valueName;

            DefaultValue = defaultValue;
            CurrentValue = defaultValue;

            return Show();
        }

        private double? _defaultValue;

        public double? DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                if (Equals(_defaultValue, value)) return;
                _defaultValue = value;
                RaisePropertyChanged(() => DefaultValue);
            }
        }

        private string _valueName;

        public string ValueName
        {
            get { return _valueName; }
            set
            {
                if (Equals(_valueName, value)) return;
                _valueName = value;
                RaisePropertyChanged(() => ValueName);
            }
        }


        public DelegateCommand ConfirmCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        private double? _currentValue;

        public double? CurrentValue
        {
            get { return _currentValue; }
            set
            {
                if (Equals(_currentValue, value)) return;
                _currentValue = value;
                RaisePropertyChanged(() => CurrentValue);
            }
        }

        private double _maxValue;

        public double MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (Equals(_maxValue, value)) return;
                _maxValue = value;
                RaisePropertyChanged(() => MaxValue);
            }
        }

        private double _minValue;

        public double MinValue
        {
            get { return _minValue; }
            set
            {
                if (Equals(_minValue, value)) return;
                _minValue = value;
                RaisePropertyChanged(() => MinValue);
            }
        }

        private double _unit;

        public double Unit
        {
            get { return _unit; }
            set
            {
                if (Equals(_unit, value)) return;
                _unit = value;
                RaisePropertyChanged(() => Unit);
            }
        }

        protected override string GetDialogRegionName()
        {
            return HdcDialogNames.Calculate;
        }

        protected override string GetDialogViewName()
        {
            return HdcDialogNames.Calculate;
        }
    }
}