using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TopDownLevelEditor.Interfaces;
using TopDownLevelEditor.Views.Windows;

namespace TopDownLevelEditor.ViewModels
{
    [Serializable]
    public class LevelBlueprintViewModel : NotifyBase, ILevelBlueprint
    {
        private LevelBlueprintPropertiesViewModel _LevelProperties = new LevelBlueprintPropertiesViewModel();
        public LevelBlueprintPropertiesViewModel LevelProperties
        {
            get => _LevelProperties;
            set { _LevelProperties = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// A collection of room blueprints that can be used when generating
        /// this level
        /// </summary>
        public IRoomBlueprintLibrary BlueprintLibrary
        {
            get => _BlueprintLibrary;
            set { _BlueprintLibrary = value; NotifyPropertyChanged(); }
        }
        private IRoomBlueprintLibrary _BlueprintLibrary;

        
        public LevelBlueprintViewModel()
        {
            BlueprintLibrary = new RoomBlueprintLibraryViewModel(this);
        }

        public LevelBlueprintViewModel(SerializationInfo info, StreamingContext context)
        {
            LevelProperties = info.GetValue<LevelBlueprintPropertiesViewModel>(nameof(LevelProperties));
            BlueprintLibrary = info.GetValue<RoomBlueprintLibraryViewModel>(nameof(BlueprintLibrary));
            BlueprintLibrary.ParentLevel = this;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(LevelProperties), _LevelProperties);
            info.AddValue(nameof(BlueprintLibrary), _BlueprintLibrary);
        }
    }
}
