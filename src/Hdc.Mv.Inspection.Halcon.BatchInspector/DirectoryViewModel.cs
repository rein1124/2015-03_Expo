using System.ComponentModel;
using System.Runtime.CompilerServices;
using Hdc.Mv.Inspection.Halcon.BatchInspector.Annotations;

namespace Hdc.Mv.Inspection.Halcon.BatchInspector
{
    public class DirectoryViewModel:INotifyPropertyChanged
    {
        private string _directoryPath;

        public string DirectoryPath
        {
            get { return _directoryPath; }
            set
            {
                if (value == _directoryPath) return;
                _directoryPath = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}