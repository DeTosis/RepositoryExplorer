using System.Diagnostics;
using System.IO;

namespace RepositoryExplorer.Model {
    class OpenDesiredFolder {
        public void OpenFolder(string Path) {
            if (!Directory.Exists(Path)) return;
            Process.Start("explorer.exe", Path);
        }
    }
}
