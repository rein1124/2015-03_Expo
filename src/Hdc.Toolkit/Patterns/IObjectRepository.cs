namespace Hdc.Patterns
{
    public interface IObjectRepository
    {
        T Load<T>() where T : class;

        T Load<T>(T defaultData) where T : class;

        void Save<T>(T data);
    }
}