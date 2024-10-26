using System.Collections.ObjectModel;
using RepositoryExplorer.Model.DataStructure;
using RepositoryExplorer.Model.Notifications;
using RepositoryExplorer.utils;
using RepositoryExplorer.View.utilControls;

namespace RepositoryExplorer.ViewModel {
    public class VM_FoldersPanel : ViewModel_Base {
        public int rowsCount { get; set; }
        public ObservableCollection<TabButton> tabs { get; private set; } = new();

        private void UpdateData() {
            rowsCount = tabs.Count;
            OnPropertyChanged("tabs");
            OnPropertyChanged("rowsCount");
        }


        public VM_FoldersPanel() {
            FolderActionNotification.AddFolder += OnFolderAdded;
            FolderActionNotification.RemoveFolder += OnFolderRemoved;
            FolderActionNotification.FolderSellected += OnFolderSellection;

            UpdateFolderPannel(new Folders());
        }

        void OnFolderAdded(object? sender, EventArgs e) => UpdateFolderPannel(new Folders());
        void OnFolderRemoved(object? sender, EventArgs e) => UpdateFolderPannel(new Folders());
        void OnFolderSellection(object? sender, EventArgs e) {
            VM_TabButton tab = (VM_TabButton)sender;
            foreach (TabButton button in tabs) {
                VM_TabButton dataContext = button.DataContext as VM_TabButton;
                if (dataContext.isTabActive && dataContext.FolderName != tab.FolderName) {
                    dataContext.isTabActive = false;
                    dataContext.UpdateData();
                }
            }
        }

        void UpdateFolderPannel(Folders dataFolders) {
            tabs.Clear();
            foreach (var dataObj in dataFolders.savedData) {
                AddNewTab(dataObj.FolderPath.Split(@"\").Last());
            }
            UpdateData();
        }

        private void AddNewTab(string folderName) {
            TabButton tab = new TabButton();
            VM_TabButton button_dataContext = tab.DataContext as VM_TabButton;
            button_dataContext.FolderName = folderName;
            tabs.Add(tab);
        }
    }
}
