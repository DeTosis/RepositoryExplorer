using System.Collections.ObjectModel;
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
            FolderActionNotification.RefreshFolders += Refresh;
            FP_Folders.FilterUpdated += OnSearchFilterUpdated;
        }
        void OnSearchFilterUpdated(object? sender, EventArgs e) => SetupTabs();

        void TabSellected(object? sender, EventArgs e) {
            VM_TabButton tab = (VM_TabButton)sender;
            string folderPath = new Folders().GetFolderPathByName(tab.FolderName);
            FP_Folders.SetActiveTab(folderPath);
            if (FP_Folders.activeFolderPath == null) return;
            SetupTabs();
        }

        void Refresh(object? sender, EventArgs e) {
            Projects.Clear();
            rescentList.Clear();
            SetupTabs();
        }

        void SetupTabs() {
            string folderPath = FP_Folders.activeFolderPath;
            if (folderPath == null) return;

            Projects.Clear();
            rescentList.Clear();

            List<DataUnit> dataUnits = new ProjectSolutionParser(folderPath).GetAllSolutions();
            if (!string.IsNullOrEmpty(FP_Folders.filter)) {
                dataUnits = new ProjectSolutionParser(FP_Folders.activeFolderPath).GetAllSolutions(FP_Folders.filter);
            } else {
                dataUnits = new ProjectSolutionParser(FP_Folders.activeFolderPath).GetAllSolutions();
            }

            for (int i = 0; i < dataUnits.Count(); i++) {
                AddNewFolder(dataUnits[i]);
            }

            for (int i = 0; i < rescentList.Count(); i++) {
                Projects.Insert(i, rescentList[i]);
            }

            UpdateData();
        }


        List<ProjectBlock> rescentList = new();
        private void AddNewFolder(DataUnit data) {
            ProjectBlock pb = new ProjectBlock();
            VM_ProjectBlock dataContext = pb.DataContext as VM_ProjectBlock;

            dataContext.isRescent = new Folders().CheckRescent(data.fldrPath);
            dataContext.FolderName = data.fldrName;
            dataContext.FolderPath = data.fldrPath;
            dataContext.ReleasePath = data.releaseFldrPath;
            dataContext.DebugPath = data.debugFldrPath;
            dataContext.INIT();

            if (dataContext.isRescent) {
                rescentList.Add(pb);
            } else {
                Projects.Add(pb);
            }
        }
    }
}
