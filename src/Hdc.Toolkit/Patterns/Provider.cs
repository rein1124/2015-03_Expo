using System;

namespace Hdc.Patterns
{
    [Obsolete("use RepositoryPattern and IInputOutputService instead")]
    public  abstract class Provider<TData> : IProvider<TData> where TData : class
    {
        private TData _data;

        public TData Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public abstract void Update();
//        {
//            LoadData(ref _data);
//            _data = data ?? GetDefualtData();
//        }

        public TData Load()
        {
            Update();
            return _data;
        }

//        protected virtual TData GetDefualtData()
//        {
//            return null;
//        }

        public void Commit()
        {
            Save(_data);
        }

        public abstract void Save(TData data);
//        {
//            _data = data;
//            Commit();
//        }

//        protected abstract void SaveData(TData data);

//        protected abstract void LoadData(ref TData oldData);
    }
}