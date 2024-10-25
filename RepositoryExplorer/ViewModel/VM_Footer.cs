using System.Diagnostics;
using RepositoryExplorer.utils;
using RepositoryExplorer.View;

namespace RepositoryExplorer.ViewModel {
    public class VM_Footer : ViewModel_Base {
        public RelayCommand C_AddFolder => new RelayCommand(execute => AddFolder());
        public RelayCommand C_OpenVS => new RelayCommand(execute => OpenVS());

        private void AddFolder() {
            FolderPathInputWindow w = new FolderPathInputWindow();
            VM_FolderPathInputWindow dataC = w.DataContext as VM_FolderPathInputWindow;
            dataC.thisW = w;
            w.ShowDialog();
        }

        private void OpenVS() {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C start devenv.exe";
            p.Start();
        }
    }
}
