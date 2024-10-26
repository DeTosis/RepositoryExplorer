using System.Windows;
using RepositoryExplorer.Model;
using RepositoryExplorer.utils;


namespace RepositoryExplorer.ViewModel {
    class VM_Header : ViewModel_Base {
        public RelayCommand CloseApp => new RelayCommand(execute => Exit());
        public RelayCommand C_ClearTextBox => new RelayCommand(execute => ClearTextBox());

        private void Exit() {
            Environment.Exit(0);
            Application.Current.Shutdown();
        }

        private void ClearTextBox() {
            SearchText = string.Empty;
        }

        private string searchText;
        public string SearchText {
            get { return searchText; }
            set {
                searchText = value;
                OnPropertyChanged();
                FP_Folders.SetFilter(SearchText);
            }
        }
    }
}
