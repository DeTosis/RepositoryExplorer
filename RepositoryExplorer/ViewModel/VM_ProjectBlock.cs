using System.Windows;
using RepositoryExplorer.Model;
using RepositoryExplorer.utils;

namespace RepositoryExplorer.ViewModel {
    public class VM_ProjectBlock : ViewModel_Base  {
        public string FolderName { get; set; }
        public string FolderPath { get; set; }
        public string DebugPath { get; set; }
        public string ReleasePath { get; set; }
        public bool DebugExists { get; set; }
        public bool ReleaseExists { get; set; }

        private void UpdateData() {
            OnPropertyChanged("FolderName");
            OnPropertyChanged("FolderPath");
            OnPropertyChanged("DebugPath");
            OnPropertyChanged("ReleasePath");
            OnPropertyChanged("DebugExists");
            OnPropertyChanged("ReleaseExists");
        }

        public void INIT() {
            DebugExists = !string.IsNullOrEmpty(DebugPath);
            ReleaseExists = !string.IsNullOrEmpty(ReleasePath);
            UpdateData();
        }

        public RelayCommand C_OpenFolder => new RelayCommand(execute => OpenFolder());
        public RelayCommand C_OpenDebug => new RelayCommand(execute => OpenDebug());
        public RelayCommand C_OpenRelease => new RelayCommand(execute => OpenRelease());


        private OpenDesiredFolder open = new();
        private void OpenFolder() {
            open.OpenFolder(FolderPath);
        }
        private void OpenDebug() { 
            open.OpenFolder(DebugPath);
        }
        private void OpenRelease() {
            open.OpenFolder(ReleasePath);
        }
    }
}
