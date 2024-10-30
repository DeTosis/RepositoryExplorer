using System.Windows.Media;
using RepositoryExplorer.Model.ColorSettings;
using RepositoryExplorer.utils;
using System.Windows;

namespace RepositoryExplorer.ViewModel.Settings {
    public class VM_ChangeColorBlock : ViewModel_Base{
		private string inputColor;
		public string InputColor {
			get { return inputColor; }
			set { inputColor = value; OnPropertyChanged(); ChangeColor(); }
		}

		private SolidColorBrush colorBrush;
		public SolidColorBrush ColorBrush{
			get { return colorBrush; }
			set { colorBrush = value;  OnPropertyChanged(); }
		}

		private string propName;
		public string PropName {
			get { return propName; }
			set { propName = value; OnPropertyChanged(); }
		}

		void ChangeColor() {
			try {
                ColorBrush = new ColorBrushFromText().getBrush(inputColor);
				Application.Current.Resources[propName] = ColorBrush;
				new SaveLoadColors().SaveColorPrefs();
            } catch {
				ColorBrush = new ColorBrushFromText().getBrush("#00000000");
            }
		}
	}
}
