namespace Hdc
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}