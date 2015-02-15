using System;
using System.IO;

namespace Hdc.Mvvm.Dialogs
{
    public interface ISelectDirectoryDialogService : IGeneralOutputDialogService<DirectoryInfo>
    {
        //IObservable<DialogEventArgs<DirectoryInfo>> Show();
    }
}