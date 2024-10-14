using System.IO;
using System.Windows;
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
            if (Directory.Exists(TbTExt)) {
                OnSubmitting();
            }
            TbTExt = string.Empty;
        }

        private void Cancel() {
            thisW.Close();
        }

        public event EventHandler<string> Submitting;
        protected virtual void OnSubmitting() {
            EventHandler<string> handler = Submitting;
            handler?.Invoke(this, TbTExt);
        }
    }
}
