using System.IO;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using Newtonsoft.Json;

namespace RepositoryExplorer.Model {
    public class SaveLoadSystem {

        string path = Directory.GetCurrentDirectory() + @"\folders.json";

        public void Save(List<string> folderPaths) {
            string Json = JsonConvert.SerializeObject(folderPaths, Formatting.Indented);
            File.WriteAllText(path, Json);
        }

        public List<string> LoadData() {
            if (!File.Exists(path)) {
                MessageBox.Show("Unable to load Data");
                return null;
            }
            string Json = File.ReadAllText(path);
            List<string> data = JsonConvert.DeserializeObject<List<string>>(Json);
            return data;
        }
    }
}
