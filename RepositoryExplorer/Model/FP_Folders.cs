using System.IO;
using RepositoryExplorer.Model.SolutionParser;

namespace RepositoryExplorer.Model {
    public static class FP_Folders {
        public static event EventHandler<string> NewFolderAdded;
        private static void OnNewFolderAdded(string newFolderPath) {
            EventHandler<string> handler = NewFolderAdded;
            handler?.Invoke(null, newFolderPath);
        }

        public static event EventHandler<List<DataUnit>> TabSwitched;
        private static void OnTabSwitched(List<DataUnit> data) {
            EventHandler<List<DataUnit>> handler = TabSwitched;
            handler?.Invoke(null, data);
        }

        public static Dictionary<string, List<DataUnit>> folders { get; private set; } = new();
        public static void AddFolder(string folderPath) {
            if (!Directory.Exists(folderPath)) return;
            if (folders.ContainsKey(folderPath)) return;

            List<DataUnit> dataUnits = new ProjectSolutionParser(folderPath).GetAllSolutions();

            folders.Add(folderPath, dataUnits);
            OnNewFolderAdded(folderPath);
        }

        public static void NewTabSellected(string tabName) {
            foreach (var item in folders.Keys) {
                if (tabName == item.Split(@"\").Last()){
                    OnTabSwitched(folders[item]);
                }
            }
        }
    }
}
