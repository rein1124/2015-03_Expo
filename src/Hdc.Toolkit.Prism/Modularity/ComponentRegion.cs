using System;
using System.Collections.Generic;
using Hdc.Mvvm;

namespace Hdc.Modularity
{
    public class ComponentRegion : ViewModel, IComponentRegion
    {
        private bool _isEnabled = true;

        private bool _isVisible = true;

        private bool _isCollapsed  = false;

        private string _name;

        private bool _isActive = true;

        public ComponentRegion()
        {
        }

        public ComponentRegion(string name)
        {
            _name = name;
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (Equals(_isEnabled, value)) return;
                _isEnabled = value;
                RaisePropertyChanged(() => IsEnabled);
            }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (Equals(_isVisible, value)) return;
                _isVisible = value;
                RaisePropertyChanged(() => IsVisible);
            }
        }

        public bool IsCollapsed
        {
            get { return _isCollapsed; }
            set
            {
                if (Equals(_isCollapsed, value)) return;
                _isCollapsed = value;
                RaisePropertyChanged(() => IsCollapsed);
            }
        }

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