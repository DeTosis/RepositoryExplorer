namespace RepositoryExplorer.Model.Notifications {
    public static class FolderActionNotification {
        public static event EventHandler AddFolder;
        public static event EventHandler RemoveFolder;

        public static event EventHandler FolderSellected;

        public static void AddFolderEvent(object? sender = null) => AddFolder?.Invoke(sender, EventArgs.Empty);
        public static void RemoveFolderEvent(object? sender = null) => RemoveFolder?.Invoke(sender, EventArgs.Empty);
        public static void FolderSellectedEvent(object? sender = null) => FolderSellected?.Invoke(sender, EventArgs.Empty);
    }
}
