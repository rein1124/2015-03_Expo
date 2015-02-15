using System.Collections.Generic;
using Hdc.Collections.ObjectModel;

namespace Hdc.Mvvm
{

    public class SelectorViewModel<T> : ViewModel, ISelectService<T> where T : class
    {
        public IObservableCollection<T> Datas = new BindableCollection<T>();

        public void Input(IEnumerable<T> data)
        {
            Reset();
            Datas.AddRange(data);
        }

        public T Output()
        {
            var selectedData = SelectedData;
            Reset();
            return selectedData;
        }

        void Reset()
        {
            Datas.Clear();
            SelectedData = null;
        }


        private T _selectedData;

        public T SelectedData
        {
            get { return _selectedData; }
            set
            {
                if (Equals(_selectedData, value)) return;
                _selectedData = value;
                RaisePropertyChanged(() => SelectedData);
            }
        }
    }
}