using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using RepositoryExplorer.Model;
using RepositoryExplorer.Model.DataStructure;
using RepositoryExplorer.Model.Notifications;
using RepositoryExplorer.Model.SolutionParser;
using RepositoryExplorer.utils;
using RepositoryExplorer.View.utilControls;

namespace RepositoryExplorer.ViewModel {
    public class VM_ProjectsPanel : ViewModel_Base {
        public ObservableCollection<ProjectBlock> Projects { get; set; } = new();
        private void UpdateData() {
            OnPropertyChanged("Projects");
        }

        public VM_ProjectsPanel() {
            FolderActionNotification.FolderSellected += TabSellected;
            FP_Folders.FilterUpdated += OnSearchFilterUpdated;
        }
        void OnSearchFilterUpdated(object? sender, EventArgs e) => SetupTabs();

        void TabSellected(object? sender, EventArgs e) {
            VM_TabButton tab = (VM_TabButton)sender;
            FP_Folders.SetActiveTab(new Folders().GetFolderPathByName(tab.FolderName));
            if (FP_Folders.activeFolderPath == null) return;
            SetupTabs();
        }

        void SetupTabs() {
            string folderPath = FP_Folders.activeFolderPath;
            if (folderPath == null) return;
            
            Projects.Clear();
            List<DataUnit> dataUnits = new ProjectSolutionParser(folderPath).GetAllSolutions();
            if (!string.IsNullOrEmpty(FP_Folders.filter)) {
                dataUnits = new ProjectSolutionParser(FP_Folders.activeFolderPath).GetAllSolutions(FP_Folders.filter);
            } else {
                dataUnits = new ProjectSolutionParser(FP_Folders.activeFolderPath).GetAllSolutions();
            }
            for (int i = 0; i < dataUnits.Count(); i++) {
                AddNewFolder(dataUnits[i]);
            }
            UpdateData();
        }


        private void AddNewFolder(DataUnit data) {
            ProjectBlock pb = new ProjectBlock();
            VM_ProjectBlock dataContext = pb.DataContext as VM_ProjectBlock;

            dataContext.FolderName = data.fldrName;
            dataContext.FolderPath = data.fldrPath;
            dataContext.ReleasePath = data.releaseFldrPath;
            dataContext.DebugPath = data.debugFldrPath;
            dataContext.INIT();

            Projects.Add(pb);
        }
    }
}
