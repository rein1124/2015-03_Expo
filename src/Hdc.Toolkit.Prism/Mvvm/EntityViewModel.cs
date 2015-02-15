namespace Hdc.Mvvm
{
    public class EntityViewModel : ViewModel, IEntityViewModel
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
    }
}