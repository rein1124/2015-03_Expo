using System;

namespace Hdc.Mvvm
{
    public abstract class EditorViewModel<T> : ViewModel, IEditor<T> where T : class
    {
        public void Input(T data)
        {
            OnInput(data);
        }

        public T Output()
        {
            var output = GetOutput(EditingData);
            return output;
        }

        private T _editingData;

        public T EditingData
        {
            get { return _editingData; }
            set
            {
                if (Equals(_editingData, value)) return;
                _editingData = value;
                RaisePropertyChanged(() => EditingData);
            }
        }

        protected virtual T GetOutput(T editingData)
        {
            return editingData;
        }

        protected virtual void OnInput(T input)
        {
            EditingData = input;
        }
    }
}