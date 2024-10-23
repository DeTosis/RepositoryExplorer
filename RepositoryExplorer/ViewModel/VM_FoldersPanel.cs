using System.Collections.ObjectModel;
using RepositoryExplorer.Model;
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
            FP_Folders.NewFolderAdded += OnNewTabAdded;
            FP_Folders.RemoveTab += OnTabRemoved;
            UpdateData();
        }

        private void AddNewTab(string folderName) {
            TabButton tab = new TabButton();
            VM_TabButton button_dataContext = tab.DataContext as VM_TabButton;
            button_dataContext.FolderName = folderName;
            button_dataContext.NewTabActive += OnTabActive;
            tabs.Add(tab);
        }

        private void OnTabRemoved(object? sender, string e) {
            foreach (var tab in tabs.ToList()) {
                VM_TabButton button_dataContext = tab.DataContext as VM_TabButton;
                if (button_dataContext.FolderName == e) {
                    tabs.Remove(tab);
                    UpdateData();
                }
            }
        }

        private void OnTabActive(object? sender, EventArgs e) {
            UpdateData();
            VM_TabButton tab = (VM_TabButton)sender;
            foreach (TabButton button in tabs) {
                VM_TabButton dataContext = button.DataContext as VM_TabButton;
                if (dataContext.isTabActive && dataContext.FolderName != tab.FolderName) {
                    dataContext.isTabActive = false;
                    dataContext.UpdateData();
                } else if (tab.FolderName == dataContext.FolderName){
                    FP_Folders.NewTabSellected(tab.FolderName);
                }
            }
        }

        private void OnNewTabAdded(object? sender, string e) {
            AddNewTab(e.Split(@"\").Last());
            UpdateData();
        }
    }
}
