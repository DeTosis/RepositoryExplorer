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
                if (data.FolderData.RescentFolders == null) data.FolderData.RescentFolders = new();
                if (data.FolderData.FavoriteFolders == null) data.FolderData.FavoriteFolders = new();
            }
        }

        public void AddRescent(string folderPath) {
            foreach (Data data in savedData) {
                if (data.FolderPath.ToLower() == FP_Folders.activeFolderPath.ToLower()) {
                    if (data.FolderData.RescentFolders == null) {
                        data.FolderData.RescentFolders = new();
                    }
                    if (data.FolderData.RescentFolders.Contains(folderPath)) return;

                    data.FolderData.RescentFolders.Add(folderPath);
                    new SaveLoadSystem().Save(savedData);
                    FolderActionNotification.RefreshFoldersEvent();
                }
            }
        }

        public bool CheckRescent(string folderPath) {
            foreach (Data data in savedData) {
                if (data.FolderPath.ToLower() != FP_Folders.activeFolderPath.ToLower()) continue;
                if (data.FolderData.RescentFolders.Contains(folderPath)) return true;
            }
            return false;
        }

        //*** ***
        public void AddFolder(string folderPath) {
            if (!Directory.Exists(folderPath)) return;
            if (!new ValidateFolder().Validate(folderPath)) return;
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
