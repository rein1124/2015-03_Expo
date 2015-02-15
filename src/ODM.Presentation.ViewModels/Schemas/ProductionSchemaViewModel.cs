using System;
using System.Collections.Generic;
using System.ComponentModel;
using Hdc;
using Hdc.Collections.ObjectModel;
using Hdc.Mvvm;
using ODM.Domain.Schemas;

namespace ODM.Presentation.ViewModels.Schemas
{
    public class ProductionSchemaViewModel : ViewModel, IProductionSchemaViewModel
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

        private int _index;

        public int Index
        {
            get { return _index; }
            set
            {
                if (Equals(_index, value)) return;
                _index = value;
                RaisePropertyChanged(() => Index);
            }
        }

        public ProductionSchemaViewModel()
        {
            _parameterEntries = new BindingList<IParameterEntryViewModel>();
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

        private string _comment;

        public string Comment
        {
            get { return _comment; }
            set
            {
                if (Equals(_comment, value)) return;
                _comment = value;
                RaisePropertyChanged(() => Comment);
            }
        }

        private DateTime _modifiedDateTime;

        public DateTime ModifiedDateTime
        {
            get { return _modifiedDateTime; }
            set
            {
                if (Equals(_modifiedDateTime, value)) return;
                _modifiedDateTime = value;
                RaisePropertyChanged(() => ModifiedDateTime);
            }
        }

        private DateTime _downloadDateTime;

        public DateTime DownloadDateTime
        {
            get { return _downloadDateTime; }
            set
            {
                if (Equals(_downloadDateTime, value)) return;
                _downloadDateTime = value;
                RaisePropertyChanged(() => DownloadDateTime);
            }
        }

        private IList<IParameterEntryViewModel> _parameterEntries;

        public IList<IParameterEntryViewModel> ParameterEntries
        {
            get { return _parameterEntries; }
            set { _parameterEntries = value; }
        }

        public IParameterEntryViewModel this[string parameterName]
        {
            get
            {
                var pn = parameterName;
                return this.GetParameterEntry(pn);
            }
        }

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (Equals(_isActive, value)) return;
                _isActive = value;
                RaisePropertyChanged(() => IsActive);
            }
        }


        
    }
}