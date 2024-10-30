using System.Windows;
using System.Windows.Input;
using RepositoryExplorer.Model.ColorSettings;

namespace RepositoryExplorer.View {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            new SaveLoadColors().LoadColorsInPelette();
        }

        private void TitleBarGrid_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}