using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using Hdc.Controls;

namespace FolderBrowser
{
    public class BrowserViewModel : ViewModelBase
    {
        private string _selectedFolder;

        public string SelectedFolder
        {
            get { return _selectedFolder; }
            set
            {
                if (_selectedFolder == value)
                    return;

                _selectedFolder = value;
                OnPropertyChanged("SelectedFolder");
            }
        }

        public ObservableCollection<FolderViewModel> Folders { get; set; }

        // TODO: 2014-04-06, removed temporay, should implement DelegateCommand in Hdc.Toolkit
//        public DelegateCommand<object> FolderSelectedCommand
//        {
//            get
//            {
//                return
//                    new DelegateCommand<object>(
//                        it => SelectedFolder = Environment.GetFolderPath((Environment.SpecialFolder) it));
//            }
//        }


        public BrowserViewModel()
        {
            Folders = new ObservableCollection<FolderViewModel>();
            Environment.GetLogicalDrives().ToList().ForEach(
                it =>
                Folders.Add(new FolderViewModel
                                {
                                    Root = this,
                                    FolderPath = it.TrimEnd('\\'),
                                    FolderName = it.TrimEnd('\\'),
                                    FolderIcon = "Images\\HardDisk.ico"
                                }));
        }
    }
}