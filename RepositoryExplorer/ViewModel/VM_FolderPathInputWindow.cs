using System.Windows;
using RepositoryExplorer.Model.DataStructure;
using RepositoryExplorer.utils;

namespace RepositoryExplorer.ViewModel {
    class VM_FolderPathInputWindow : ViewModel_Base {
        private string tbText;
        public Window thisW;

        public RelayCommand C_Submit => new RelayCommand(execute => Submit());
        public RelayCommand C_Cancel => new RelayCommand(execute => Cancel());

        public string TbTExt {
            get { return tbText; }
            set {
                tbText = value;
                OnPropertyChanged();
            }
        }

        private void Submit() {
            new Folders().AddFolder(TbTExt);
            TbTExt = string.Empty;
            Cancel();
        }

        private void Cancel() => thisW.Close();
    }
}
