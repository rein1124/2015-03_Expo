namespace Hdc.Patterns
{

    public interface IPlugin<in T>
    {
        void Initialize(T arg);
    }
}