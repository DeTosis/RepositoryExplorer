using System.Windows;
using System.Windows.Input;

namespace RepositoryExplorer.View {
    public partial class FolderPathInputWindow : Window {
        public FolderPathInputWindow() {
            InitializeComponent();
        }

        private void TitleBarGrid_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
