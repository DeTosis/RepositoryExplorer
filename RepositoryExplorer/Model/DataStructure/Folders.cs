using System.IO;
using RepositoryExplorer.Model.Notifications;

namespace RepositoryExplorer.Model.DataStructure {
    public class Folders {
        public List<string> foldersPaths { get; private set; }
        public Folders() {
            foldersPaths = new SaveLoadSystem().LoadData();
        }

        public void AddFolder(string folderPath) {
            if (!Directory.Exists(folderPath)) return;
            if (foldersPaths.Contains(folderPath)) return;
            if (!new ValidateFolder().Validate(folderPath)) return;

            foldersPaths.Add(folderPath);
            SaveData();
            FolderActionNotification.AddFolderEvent();
        }

        public void RemoveFolder(string folderPath) {
            if (!Directory.Exists(folderPath)) return;
            if (!foldersPaths.Contains(folderPath)) return;

            foldersPaths.Remove(folderPath);
            SaveData();
            FolderActionNotification.RemoveFolderEvent();
        }

        public string GetFolderPathByName(string folderName) {
            foreach (var s in foldersPaths) {
                if (s.Split(@"\").Last() == folderName) return s;
            }
            return null;
        }

        void SaveData()  => new SaveLoadSystem().Save(foldersPaths);
    }
}
