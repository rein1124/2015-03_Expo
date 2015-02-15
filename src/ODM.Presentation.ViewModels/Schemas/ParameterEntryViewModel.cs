using Hdc.Mercury;
using Hdc.Mvvm;
using Hdc.Reactive;
using ODM.Domain.Schemas;

namespace ODM.Presentation.ViewModels.Schemas
{
    public class ParameterEntryViewModel : ViewModel, IParameterEntryViewModel
    {
        private long _id;

        public long Id
        {
            get { return _id; }
            set
            {
                if (Equals(_id, value)) return;
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }

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

        private int _value;

        public int Value
        {
            get { return _value; }
            set
            {
                if (Equals(_value, value)) return;
                _value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        private string _groupName;

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                if (Equals(_groupName, value)) return;
                _groupName = value;
                RaisePropertyChanged(() => GroupName);
            }
        }

        private string _groupDescription;

        public string GroupDescription
        {
            get { return _groupDescription; }
            set
            {
                if (Equals(_groupDescription, value)) return;
                _groupDescription = value;
                RaisePropertyChanged(() => GroupDescription);
            }
        }

        private string _catalogName;

        public string CatalogName
        {
            get { return _catalogName; }
            set
            {
                if (Equals(_catalogName, value)) return;
                _catalogName = value;
                RaisePropertyChanged(() => CatalogName);
            }
        }

        private string _catalogDescription;

        public string CatalogDescription
        {
            get { return _catalogDescription; }
            set
            {
                if (Equals(_catalogDescription, value)) return;
                _catalogDescription = value;
                RaisePropertyChanged(() => CatalogDescription);
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                if (Equals(_description, value)) return;
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }


        public int PlcValue
        {
            get { return Monitor.Value; }
            private set { Monitor.Value = value; }
        }

        public bool IsPlcValueEnabled { get; private set; }

        public void InitMonitor(IDevice<int> device)
        {
            Monitor = new ValueMonitor<int>();
            this.RaisePropertyChangedOnUsingDispatcher(Monitor, () => PlcValue);
            Monitor.Sync(device);
            IsPlcValueEnabled = true;
        }

        public IValueMonitor<int> Monitor { get; private set; }



        private bool _isPlcDevice;

        public bool IsPlcDevice
        {
            get { return _isPlcDevice; }
            set
            {
                if (Equals(_isPlcDevice, value)) return;
                _isPlcDevice = value;
                RaisePropertyChanged(() => IsPlcDevice);
            }
        }
    }
}