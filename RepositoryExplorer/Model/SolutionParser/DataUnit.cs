namespace RepositoryExplorer.Model.SolutionParser {
    public class DataUnit(string fldrName, string fldrPath, string slnPath, string debugFldrPath, string releaseFldrPath) {
        public string fldrName { get; } = fldrName;
        public string fldrPath { get; } = fldrPath;
        public string slnPath { get; } = slnPath;
        public string debugFldrPath { get; } = debugFldrPath;
        public string releaseFldrPath { get; } = releaseFldrPath;
    }
}
