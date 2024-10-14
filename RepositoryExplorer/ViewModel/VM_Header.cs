using System.Windows;
using RepositoryExplorer.utils;


namespace RepositoryExplorer.ViewModel {
    class VM_Header : ViewModel_Base {
        public RelayCommand CloseApp => new RelayCommand(execute => Exit());
        private void Exit() {
            Environment.Exit(0);
            Application.Current.Shutdown();
        }
    }
}
