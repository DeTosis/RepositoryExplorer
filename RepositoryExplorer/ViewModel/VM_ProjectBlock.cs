using System.Diagnostics;
using System.Windows.Media;
using RepositoryExplorer.Model;
using RepositoryExplorer.Model.ColorSettings;
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

        public string SolutionPath { get; set; }

        public bool isresent { get; set; }
        private void UpdateData() {
            OnPropertyChanged("FolderName");
            OnPropertyChanged("FolderPath");
            OnPropertyChanged("DebugPath");
            OnPropertyChanged("ReleasePath");
            OnPropertyChanged("DebugExists");
            OnPropertyChanged("ReleaseExists");
            OnPropertyChanged("isresent");
        }

        private SolidColorBrush borderBrush = new ColorBrushFromText().getBrush("#C4BBAF");
        public SolidColorBrush BorderBrush {
            get { return borderBrush; }
            set {
                borderBrush = value;
                OnPropertyChanged();
            }
        }
        public void INIT() {
            if (isresent) BorderBrush = new ColorBrushFromText().getBrush("#496F5D");
            DebugExists = !string.IsNullOrEmpty(DebugPath);
            ReleaseExists = !string.IsNullOrEmpty(ReleasePath);
            UpdateData();
        }

        public RelayCommand C_OpenFolder => new RelayCommand(execute => OpenFolder());
        public RelayCommand C_OpenDebug => new RelayCommand(execute => OpenDebug());
        public RelayCommand C_OpenRelease => new RelayCommand(execute => OpenRelease());
        public RelayCommand C_OpenSolution => new RelayCommand(execute => OpenSolution());
        public RelayCommand C_RemoveFromResent => new RelayCommand(execute => RemoveFromResent());


        private OpenDesiredFolder open = new();
        private void OpenFolder() {
            open.OpenFolder(FolderPath);
            AddFolderToResent();
        }

        private void OpenDebug() {
            open.OpenFolder(DebugPath);
            AddFolderToResent();
        }

        private void OpenRelease() {
            open.OpenFolder(ReleasePath);
            AddFolderToResent();
        }

        void OpenSolution() {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = $"/C start {SolutionPath}";
            p.Start();
            AddFolderToResent();
        }

        void RemoveFromResent() {
            new Folders().RemoveResent(FolderPath);
        }

        private void AddFolderToResent() {
            new Folders().AddResent(FolderPath);
        }
    }
}
