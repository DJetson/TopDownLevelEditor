using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TopDownLevelEditor.ViewModels
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName]string property = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}