using System.Windows.Media;
using RepositoryExplorer.Model;
using RepositoryExplorer.Model.DataStructure;
using RepositoryExplorer.utils;

namespace RepositoryExplorer.ViewModel {
    public class VM_ProjectBlock : ViewModel_Base {
        public string FolderName { get; set; }
        public string FolderPath { get; set; }
        public string DebugPath { get; set; }
        public string ReleasePath { get; set; }
        public bool DebugExists { get; set; }
        public bool ReleaseExists { get; set; }

        public bool isRescent { get; set; }
        private void UpdateData() {
            OnPropertyChanged("FolderName");
            OnPropertyChanged("FolderPath");
            OnPropertyChanged("DebugPath");
            OnPropertyChanged("ReleasePath");
            OnPropertyChanged("DebugExists");
            OnPropertyChanged("ReleaseExists");
        }

        private SolidColorBrush borderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#C4BBAF");
        public SolidColorBrush BorderBrush {
            get { return borderBrush; }
            set {
                borderBrush = value;
                OnPropertyChanged();
            }
        }


        public void INIT() {
            if (isRescent) BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#496F5D");
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
            AddFolderToRescent();
        }

        private void OpenDebug() {
            open.OpenFolder(DebugPath);
            AddFolderToRescent();
        }

        private void OpenRelease() {
            open.OpenFolder(ReleasePath);
            AddFolderToRescent();
        }

        private void AddFolderToRescent() {
            new Folders().AddRescent(FolderPath);
        }
    }
}
