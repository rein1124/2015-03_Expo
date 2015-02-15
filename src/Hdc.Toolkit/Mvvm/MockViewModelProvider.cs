using System;

namespace Hdc.Mvvm
{
    public class MockViewModelProvider : IViewModelProvider
    {
        public object GetViewModel(string viewModelName)
        {
            return null;
        }
    }
}