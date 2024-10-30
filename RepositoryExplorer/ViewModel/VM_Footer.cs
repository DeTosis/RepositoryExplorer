using System.Diagnostics;
using RepositoryExplorer.utils;
using RepositoryExplorer.View;
using RepositoryExplorer.View.Settings;
using RepositoryExplorer.ViewModel.Settings;

namespace RepositoryExplorer.ViewModel {
    public class VM_Footer : ViewModel_Base {
        public RelayCommand C_AddFolder => new RelayCommand(execute => AddFolder());
        public RelayCommand C_OpenVS => new RelayCommand(execute => OpenVS());
        public RelayCommand C_OpenSettings => new RelayCommand(execute => OpenSettings());

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

        void OpenSettings() {
            SettingsPage settings = new SettingsPage();
            settings.ShowDialog();
        }
    }
}
