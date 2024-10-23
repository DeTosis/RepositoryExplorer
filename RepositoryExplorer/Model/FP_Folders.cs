using System.IO;
using RepositoryExplorer.Model.SolutionParser;

namespace RepositoryExplorer.Model {
    public static class FP_Folders {

        public static Dictionary<string, List<DataUnit>> folders { get; private set; } = new();
        private static string currentTab = "";
        public static string activeFilter = "";

        public static void OnLoad() {
            List<string> folders = new SaveLoadSystem().LoadData();
            if (folders == null) return;
            foreach ( var i in folders) {
                AddFolder(i);
            }
        }

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

        public static event EventHandler<string> RemoveTab;
        private static void OnRemoveTab(string folderName) {
            EventHandler<string> handler = RemoveTab;
            handler?.Invoke(null, folderName);
        }

        public static void AddFolder(string folderPath) {
            if (!Directory.Exists(folderPath)) return;
            if (folders.ContainsKey(folderPath)) return;

            List<DataUnit> dataUnits = new ProjectSolutionParser(folderPath).GetAllSolutions();

            folders.Add(folderPath, dataUnits);
            OnNewFolderAdded(folderPath);
            SaveData();
        }

        public static void RemoveFolder(string folderName) {
            foreach (var i in folders.Keys) {
                if (i.Split(@"\").Last() == folderName) {
                    folders.Remove(i);
                    OnRemoveTab(folderName);
                    SaveData();
                }
            }
        }

        public static void Refresh() {
            if (string.IsNullOrEmpty(currentTab)) return;

            List<DataUnit> dataUnits = new();
            if (!string.IsNullOrEmpty(activeFilter)) {
                dataUnits = new ProjectSolutionParser(currentTab).GetAllSolutions(activeFilter);
            } else {
                dataUnits = new ProjectSolutionParser(currentTab).GetAllSolutions();
            }
            folders[currentTab].Clear();
            folders[currentTab] = dataUnits;
            OnTabSwitched(folders[currentTab]);
        }

        private static void SaveData() {
            List<string> paths = [.. folders.Keys];
            new SaveLoadSystem().Save(paths);
        }


        public static void NewTabSellected(string tabName) {
            foreach (var item in folders.Keys) {
                if (tabName == item.Split(@"\").Last()){
                    currentTab = item;
                    Refresh();
                    OnTabSwitched(folders[item]);
                }
            }
        }
    }
}
