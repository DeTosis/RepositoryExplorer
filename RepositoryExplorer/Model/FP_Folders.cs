using System.IO;

namespace RepositoryExplorer.Model {
    public static class FP_Folders {
        public static event EventHandler FilterUpdated;
        private static void OnFilterUpdated() => FilterUpdated?.Invoke(null, EventArgs.Empty);

        public static string? filter { get; private set; }
        public static string? activeFolderPath { get; private set; }

        public static void SetFilter(string searchFilter) {
            filter = searchFilter.ToLower();
            OnFilterUpdated();
        }

        public static void SetActiveTab(string folderPath) {
            if (!Directory.Exists(folderPath)) return;
            activeFolderPath = folderPath.ToLower();
        }
    }
}
