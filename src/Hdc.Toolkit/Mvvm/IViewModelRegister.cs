using System;

namespace Hdc.Mvvm
{
    public interface IViewModelRegister
    {
        void Register(string viewModelName,Type viewModelType);
        void Register(string viewModelName,Type viewModelType,string viewModelKey);

        void Register<T>(string viewModelName);
        void Register<T>(string viewModelName,string key);
    }
}