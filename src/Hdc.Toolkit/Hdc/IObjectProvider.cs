namespace Hdc
{
    public interface IObjectProvider<out TObject>
    {
        TObject Object { get; }
    }
}