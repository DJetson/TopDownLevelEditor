using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.IO;
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
        private bool _HasChanged = false;
        public bool HasChanged
        {
            get => _HasChanged;
            set { _HasChanged = value; NotifyPropertyChanged(); }
        }

        [NonSerialized]
        private string _Filepath = "";
        public string Filepath
        {
            get => _Filepath;
            set
            {
                _Filepath = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Filename));
                NotifyPropertyChanged(nameof(FullPath));
            }
        }

        public static string DefaultFilePath
        {
            get => "./untitled.lbp";
        }

        public string FileExtension
        {
            get => ".lbp";
        }

        public string DisplayName
        {
            get => $"{Filename}{FileExtension}";
        }

        public string Filename
        {
            get => Path.GetFileNameWithoutExtension(_Filepath);
            set { _Filepath = $"{FullPath ?? "./"}{value}{FileExtension}"; NotifyPropertyChanged(); }
        }

        public string FullPath
        {
            get => Path.GetFullPath(_Filepath);
            set { _Filepath = $"{value}{Filename}{FileExtension}"; NotifyPropertyChanged(); }
        }

        private LevelBlueprintPropertiesViewModel _LevelProperties;
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
            LevelProperties = new LevelBlueprintPropertiesViewModel(this);
        }

        //public LevelBlueprintViewModel(SerializationInfo info, StreamingContext context)
        //{
        //    LevelProperties = info.GetValue<LevelBlueprintPropertiesViewModel>(nameof(LevelProperties));
        //    BlueprintLibrary = info.GetValue<RoomBlueprintLibraryViewModel>(nameof(BlueprintLibrary));
        //    BlueprintLibrary.ParentLevel = this;
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue(nameof(LevelProperties), _LevelProperties);
        //    info.AddValue(nameof(BlueprintLibrary), _BlueprintLibrary);
        //}
    }
}
