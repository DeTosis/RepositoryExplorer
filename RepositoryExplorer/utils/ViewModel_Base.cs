using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RepositoryExplorer.utils {
    public class ViewModel_Base : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));       
        }
    }
}
