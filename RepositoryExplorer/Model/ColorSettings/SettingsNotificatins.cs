namespace RepositoryExplorer.Model.ColorSettings {
    public static class SettingsNotificatins {
        public static event EventHandler ColorChanged;
        public static void OnColorChanged() => ColorChanged?.Invoke(null,EventArgs.Empty);
    }
}
