namespace Hdc.Mvvm
{
    public interface IViewModelProvider
    {
        object GetViewModel(string viewModelName);
    }
}