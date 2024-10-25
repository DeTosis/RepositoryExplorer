using RepositoryExplorer.Model.SolutionParser;

namespace RepositoryExplorer.Model.DataStructure {
    public class ValidateFolder {
        public bool Validate(string folderPath) {
            return new ProjectSolutionParser(folderPath).GetAllSolutions().Count > 0;
        }
    }
}
