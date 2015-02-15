namespace Hdc.Mvvm
{
    public interface IInputService<in TData>
    {
        void Input(TData data);
    }
}