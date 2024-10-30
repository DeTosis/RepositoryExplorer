using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace RepositoryExplorer.Model {
    public class SaveLoadSystem {

        string path = Directory.GetCurrentDirectory() + @"\folders.json";

        public void Save(List<Data> folderData) {
            string Json = JsonConvert.SerializeObject(folderData, Formatting.Indented);
            if (!File.Exists(path)) { File.Create(path); }
            File.WriteAllText(path, Json);
        }

        public List<Data> LoadDataObjects() {
            if (!File.Exists(path)) {
                return null;
            }
            string Json = File.ReadAllText(path);
            List<Data> data = JsonConvert.DeserializeObject<List<Data>>(Json);
            
            if (data == null || data.Count() == 0) {
                return null;
            }
            foreach (var item in data) {
                if (!Directory.Exists(item.FolderPath)) {
                    data.Remove(item);
                    Save(data);
                    LoadDataObjects();
                    return data;
                }
            }

            return data;
        }
    }
    public class Data {
        public string FolderPath { get; set; }
        public FolderPrefs FolderData { get; set; } = new();

        public Data(string FolderPath, List<string> resentFolders = null, List<string> favFolders = null) {
            this.FolderPath = FolderPath;
            FolderData.ResentFolders = resentFolders;
            FolderData.FavoriteFolders = favFolders;
        }

        public class FolderPrefs {
            public List<string> ResentFolders { get; set; }
            public List<string> FavoriteFolders { get; set; }
        }
    }
}
