using System.Collections.ObjectModel;
using RepositoryExplorer.Model;
using RepositoryExplorer.Model.SolutionParser;
using RepositoryExplorer.utils;
using RepositoryExplorer.View.utilControls;

namespace RepositoryExplorer.ViewModel {
    public class VM_ProjectsPanel :ViewModel_Base {
        public ObservableCollection<ProjectBlock> Projects { get; set; } = new();
        private void UpdateData() {
            OnPropertyChanged("Projects");
        }

        public VM_ProjectsPanel() {
            FP_Folders.TabSwitched += OnTabSwitched;
        }

        private void OnTabSwitched(object? sender, List<DataUnit> data) {
            Projects.Clear();
            for (int i = 0; i < data.Count(); i++) {
                AddNewFolder(data[i]);
            }
            UpdateData();
        }

        private void AddNewFolder(DataUnit data) {
            ProjectBlock pb = new ProjectBlock();
            VM_ProjectBlock dataContext = pb.DataContext as VM_ProjectBlock;

            dataContext.FolderName = data.fldrName;
            dataContext.FolderPath = data.fldrPath;
            dataContext.ReleasePath = data.releaseFldrPath;
            dataContext.DebugPath= data.debugFldrPath;
            dataContext.INIT();

            Projects.Add(pb);
        }
    }
}
