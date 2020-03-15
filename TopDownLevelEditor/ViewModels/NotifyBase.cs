using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TopDownLevelEditor.ViewModels
{
    [Serializable]
    public abstract class NotifyBase : INotifyPropertyChanged
    {
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName]string property = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}