namespace Hdc.Patterns
{
    public interface IFactory<out T>
    {
        T Create();
    }
}