using System.Windows.Media;

namespace RepositoryExplorer.Model.ColorSettings {
    public class ColorBrushFromText {
        public SolidColorBrush getBrush(string hexColor) {
            return (SolidColorBrush)new BrushConverter().ConvertFromString(hexColor);
        }
    }
}
