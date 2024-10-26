using System.IO;

namespace RepositoryExplorer.Model.SolutionParser {
    public class ProjectSolutionParser {
        string fldr;
        public ProjectSolutionParser(string folderPath) {
            fldr = folderPath;
        }

        List<DataUnit> dataUnits = new List<DataUnit>();
        int basePathLen => fldr.Split(@"\").Count();

        public List<DataUnit> GetAllSolutions(string foldersNames = "") {
            if (!string.IsNullOrEmpty(foldersNames)) {
                AssembleData(foldersNames);
            } else { AssembleData(); }
            return dataUnits;
        }

        private void AssembleData() {
            string[] i = Directory.GetDirectories(fldr);
            foreach (string dir in i) {
                Query(dir);
            }
        }

        private void AssembleData(string filter) {
            string[] i = Directory.GetDirectories(fldr);
            foreach (string dir in i) {
                if (!dir.Split(@"\").Last().ToLower().Contains(filter.ToLower())) continue;
                Query(dir);
            }
        }

        private void Query(string path) {
            foreach (var i in Directory.GetFiles(path)) {
                if (i.EndsWith(".sln")) {
                    string fldrPath = path;
                    string fldrname = path.Split(@"\")[basePathLen];
                    string slnPath = i;

                    GetDebugReleaseFldrs(path);
                    dataUnits.Add(new DataUnit(fldrname, fldrPath, slnPath, debug, release));
                }
            }
        }

        private void GetDebugReleaseFldrs(string pPath) {
            foreach (var dir in Directory.GetDirectories(pPath)) {
                if (!dir.EndsWith("bin")) { GetDebugReleaseFldrs(dir); continue; } else {
                    getDirPaths(dir);
                }
            }
        }

        string debug = "";
        string release = "";
        private void getDirPaths(string binPath) {
            debug = "";
            release = "";
            foreach (var bin in Directory.GetDirectories(binPath)) {
                string endsWith = bin.Split(@"\").Last();

                if (endsWith == "Debug") {
                    debug = bin;
                }
                if (endsWith == "Release") {
                    release = bin;
                }
            }
        }
    }
}
