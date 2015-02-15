namespace Hdc.Mvvm
{
    public interface IOutputService<out TData>
    {
        //void Initialize();

        TData Output();
    }
}