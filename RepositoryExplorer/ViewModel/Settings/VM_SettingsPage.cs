using System.Collections.ObjectModel;
using RepositoryExplorer.Model.ColorSettings;
using RepositoryExplorer.utils;
using RepositoryExplorer.View.Settings;
using static RepositoryExplorer.Model.ColorSettings.SaveLoadColors;

namespace RepositoryExplorer.ViewModel.Settings {
    public class VM_SettingsPage : ViewModel_Base{
        public ObservableCollection<ChangeColorBlock> colorSettings { get; set; } = new();
        public int rowsCount { get; set; } = 0;

        void UpdateData() {
            rowsCount = colorSettings.Count;
            OnPropertyChanged("colorSettings");
            OnPropertyChanged("rowsCount");
        }

        public VM_SettingsPage() {
            AssembleSettings();
        }

        void AssembleSettings() {
            List<List<ColorDataUnit>> data = new SaveLoadColors().GetColorSettings();

            foreach (var item in data) {
                foreach (var setting in item) {
                    ChangeColorBlock colorBlock = new ChangeColorBlock();
                    VM_ChangeColorBlock vM_ChangeColorBlock = colorBlock.DataContext as VM_ChangeColorBlock;
                    vM_ChangeColorBlock.InputColor = setting.keyValue;
                    vM_ChangeColorBlock.PropName = setting.keyName;
                    vM_ChangeColorBlock.ColorBrush = new ColorBrushFromText().getBrush(setting.keyValue);

                    colorSettings.Add(colorBlock);
                }
            }
            UpdateData();
        }
    }
}
