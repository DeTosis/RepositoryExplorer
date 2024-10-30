using System.IO;
using System.Windows;
using RepositoryExplorer.Model.Notifications;

namespace RepositoryExplorer.Model.DataStructure {
    public class Folders {

        public List<Data> savedData { get; set; }
        public Folders() {
            LoadData();
        }

        void LoadData() {
            savedData = new SaveLoadSystem().LoadDataObjects();

            if (savedData == null) {
                savedData = new List<Data>();
                new SaveLoadSystem().Save(savedData);
            }

            foreach (Data data in savedData) {
                if (data.FolderData.ResentFolders == null) data.FolderData.ResentFolders = new();
                if (data.FolderData.FavoriteFolders == null) data.FolderData.FavoriteFolders = new();
            }
        }

        public void AddResent(string folderPath) {
            foreach (Data data in savedData) {
                if (data.FolderPath.ToLower() == FP_Folders.activeFolderPath.ToLower()) {
                    if (data.FolderData.ResentFolders == null) {
                        data.FolderData.ResentFolders = new();
                    }
                    if (data.FolderData.ResentFolders.Contains(folderPath)) return;

                    data.FolderData.ResentFolders.Add(folderPath);
                    new SaveLoadSystem().Save(savedData);
                    FolderActionNotification.RefreshFoldersEvent();
                }
            }
        }

        public void RemoveResent(string folderPath) {
            foreach (Data data in savedData) {
                if (data.FolderPath.ToLower() == FP_Folders.activeFolderPath) {
                    if (data.FolderData.ResentFolders.Contains(folderPath)) {
                        data.FolderData.ResentFolders.Remove(folderPath);
                        SaveData();
                        FolderActionNotification.RefreshFoldersEvent();
                        return;
                    }
                }
            }
        }

        public bool CheckResent(string folderPath) {
            foreach (Data data in savedData) {
                if (data.FolderPath.ToLower() != FP_Folders.activeFolderPath.ToLower()) continue;
                if (data.FolderData.ResentFolders.Contains(folderPath)) return true;
            }
            return false;
        }

        //*** ***
        public void AddFolder(string folderPath) {
            if (!Directory.Exists(folderPath)) return;
            if (!new ValidateFolder().Validate(folderPath)) {
                MessageBox.Show("Enter path to repos folder, ex:"+"\n"+@"'C:\Users\user\source\repos'");
                return; 
            }
            foreach (var dataObj in savedData) { if (dataObj.FolderPath.ToLower() == folderPath.ToLower()) return; }

            savedData.Add(new Data(folderPath));
            SaveData();
            FolderActionNotification.AddFolderEvent();
        }

        public void RemoveFolder(string folderPath) {
            if (!Directory.Exists(folderPath)) return;

            for (int i = 0; i < savedData.Count; i++) {
                if (savedData[i].FolderPath.ToLower() == folderPath.ToLower()) {
                    savedData.Remove(savedData[i]);
                    SaveData();
                    FolderActionNotification.RemoveFolderEvent();
                    FolderActionNotification.RefreshFoldersEvent();
                    return;
                }
            }
        }
        //*** ***

        public string GetFolderPathByName(string folderName) {
            foreach (var data in savedData) {
                if (data.FolderPath.ToLower().Split(@"\").Last() == folderName.ToLower()) {
                    return data.FolderPath;
                }
            }
            return null;
        }

        void SaveData() => new SaveLoadSystem().Save(savedData);
    }
}
