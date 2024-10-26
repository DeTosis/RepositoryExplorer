using System.Windows.Media;
using RepositoryExplorer.Model.DataStructure;
using RepositoryExplorer.Model.Notifications;
using RepositoryExplorer.utils;

namespace RepositoryExplorer.ViewModel {
    public class VM_TabButton : ViewModel_Base {

        private string folderName;
        public string FolderName {
            get { return folderName; }
            set {
                folderName = value;
                OnPropertyChanged();
            }
        }

        public bool isTabActive { get; set; } = false;

        private SolidColorBrush inactiveBG = (SolidColorBrush)new BrushConverter().ConvertFromString("#0F161D");
        private SolidColorBrush activeBG = (SolidColorBrush)new BrushConverter().ConvertFromString("#131316");
        public SolidColorBrush currentBg { get; private set; }

        public RelayCommand C_TabButtonClick => new RelayCommand(execute => TabButtonClick());
        public RelayCommand C_RemoveFolder => new RelayCommand(execute => RemoveFolder());

        public VM_TabButton() {
            inactiveBG.Opacity = 0f;
            UpdateData();
        }

        public void UpdateData() {
            currentBg = isTabActive ? activeBG : inactiveBG;
            OnPropertyChanged("currentBg");
        }

        private void TabButtonClick() {
            isTabActive = true;
            UpdateData();
            FolderActionNotification.FolderSellectedEvent(this);
        }

        private void RemoveFolder() {
            Folders folders = new();
            foreach (var folder in folders.savedData) {
                if (folderName == folder.FolderPath.Split(@"\").Last()) {
                    folders.RemoveFolder(folder.FolderPath);
                    break;
                }
            }
        }
    }
}
