using System.Diagnostics;
using System.Windows;
using RepositoryExplorer.Model;
using RepositoryExplorer.Model.SolutionParser;
using RepositoryExplorer.utils;
using RepositoryExplorer.View;

namespace RepositoryExplorer.ViewModel {
    public class VM_Footer : ViewModel_Base{
        public RelayCommand C_AddFolder => new RelayCommand(execute => AddFolder());
        public RelayCommand C_OpenVS => new RelayCommand(execute => OpenVS());

        private void AddFolder() {
            FolderPathInputWindow w = new FolderPathInputWindow();
            VM_FolderPathInputWindow dataC = w.DataContext as VM_FolderPathInputWindow;
            dataC.thisW = w;
            dataC.Submitting += FolderSubmit;
            w.ShowDialog();
        }

        private void FolderSubmit( object? sender, string folderP) {
            if (new ProjectSolutionParser(folderP).GetAllSolutions().Count > 0) {
                FP_Folders.AddFolder(folderP);
            } else {
                MessageBox.Show("Folder does not contain any projects or path is invalid. Correct folder setup:" +
                    "\nRepositories -> ProjectFolder -> .sln file");
            }
        }

        private void OpenVS() {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C start devenv.exe";
            p.Start();
        }
    }
}
