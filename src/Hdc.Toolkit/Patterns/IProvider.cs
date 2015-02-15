using System;

namespace Hdc.Patterns
{
    [Obsolete("use RepositoryPattern and IInputOutputService instead")]
    public interface IProvider<TData>
    {
        TData Data { get; set; }

        void Update();

        void Commit();

//        void Save(TData data);
//
//        TData Load();
    }
}