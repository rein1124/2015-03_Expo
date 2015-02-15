using System;

namespace Hdc.Mvvm.Dialogs
{
    public interface IChangeStringAppService
    {
        void ChangeString(string title, Func<string> getDefaultString, Action<string> setEditedString);
    }
}