using System.IO;
using System.Windows;
using Newtonsoft.Json;
using RepositoryExplorer.Model.ColorSettings.prefs;

namespace RepositoryExplorer.Model.ColorSettings {

    //After changing save file options are not regenerates, if needed, => add check

    public class SaveLoadColors {
        static string root = @$"{Directory.GetCurrentDirectory()}\set";
        string settingsCfgP = @$"{root}\settings.json";

        ResourceDictionary rsDicts = Application.Current.Resources;
        List<List<ColorDataUnit>> colorPrefs = new();

        public SaveLoadColors() {
            LoadColorSettings();
        }

        public void LoadColorsInPelette() {
            foreach (var item in colorPrefs) {
                foreach (var key in item) {
                    Application.Current.Resources[key.keyName] = new ColorBrushFromText().getBrush(key.keyValue);
                }
            }
        }

        void CheckFile() {
            if (!Directory.Exists(root)) Directory.CreateDirectory(root);
            if (!File.Exists(settingsCfgP)) { using (File.Create(settingsCfgP)); }
        }

        public List<List<ColorDataUnit>> GetColorSettings() { return colorPrefs; }

        void LoadColorSettings() {
            CheckFile();
            colorPrefs = JsonConvert.DeserializeObject<List<List<ColorDataUnit>>>(File.ReadAllText(settingsCfgP));
            if (colorPrefs == null || colorPrefs.Count() < 1) {
                colorPrefs = new();
                SaveColorPrefs();
            }
        }
        
        public void SaveColorPrefs() {
            colorPrefs.Clear();
            colorPrefs.Add(BuildColorBlock(colorKeys.key_other));
            colorPrefs.Add(BuildColorBlock(colorKeys.key_headers));
            colorPrefs.Add(BuildColorBlock(colorKeys.key_tabPannel));
            colorPrefs.Add(BuildColorBlock(colorKeys.key_footer));

            CheckFile();
            string json = JsonConvert.SerializeObject(colorPrefs, Formatting.Indented);
            File.WriteAllText(settingsCfgP, json);
        }


        List<ColorDataUnit> BuildColorBlock(List<string> keys) {
            List<ColorDataUnit> colorDataUnits = new List<ColorDataUnit>();
            foreach (string item in keys) {
                if (rsDicts.Contains(item)) {
                    colorDataUnits.Add(new ColorDataUnit(item, rsDicts[item].ToString()));
                }
            }

            return colorDataUnits;
        }
     
        public class ColorDataUnit {
            public ColorDataUnit(string keyName, string keyValue) {
                this.keyName = keyName;
                this.keyValue = keyValue;
            }

            public string keyName { get; set; }
            public string keyValue { get; set; }
        }
    }
}
