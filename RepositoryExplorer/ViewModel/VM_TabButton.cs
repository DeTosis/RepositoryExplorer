using System.Windows.Media;
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

        public VM_TabButton() {
            inactiveBG.Opacity = 0f;
            UpdateData();
        }

        public void UpdateData() {
            currentBg = isTabActive ? activeBG : inactiveBG;
            OnPropertyChanged("currentBg");
        }

        private void TabButtonClick() {
            isTabActive = !isTabActive;
            UpdateData();
            OnNewTabActive();
        }

        public event EventHandler NewTabActive;
        protected virtual void OnNewTabActive() {
            EventHandler handler = NewTabActive;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
