using Hdc.Mercury;
using Hdc.Mvvm;
using Hdc.Reactive;

namespace ODM.Presentation.ViewModels
{
    public class AlarmEntryViewModel : ViewModel
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (Equals(_name, value)) return;
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public AlarmEntryViewModel()
        {
        }

        public AlarmEntryViewModel(IDevice<bool> device)
        {
            InitMonitor(device);
        }

        public void InitMonitor(IDevice<bool> device)
        {
            Monitor = new ValueMonitor<bool>();
            this.RaisePropertyChangedOnUsingDispatcher(Monitor, () => PlcValue);
            Monitor.Sync(device);
            Name = device.Name;
        }

        public bool PlcValue
        {
            get { return Monitor.Value; }
            private set { Monitor.Value = value; }
        }

        public IValueMonitor<bool> Monitor { get; private set; }
    }
}