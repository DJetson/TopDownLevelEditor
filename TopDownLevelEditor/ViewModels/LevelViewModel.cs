using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    public class LevelViewModel : NotifyBase
    {
        ///// <summary>
        ///// The width of a single tile in pixels
        ///// </summary>
        //public double TileWidth
        //{
        //    get => _TileWidth;
        //    set { _TileWidth = value; NotifyPropertyChanged(); }
        //}
        //private double _TileWidth = 64.0f;

        ///// <summary>
        ///// The height of a single tile in pixels
        ///// </summary>
        //public double TileHeight
        //{
        //    get => _TileHeight;
        //    set { _TileHeight = value; NotifyPropertyChanged(); }
        //}
        //private double _TileHeight = 64.0f;

        ///// <summary>
        ///// The width of a single room in tiles
        ///// </summary>
        //public int RoomWidth
        //{
        //    get => _RoomWidth;
        //    set { _RoomWidth = value; NotifyPropertyChanged(); }
        //}
        //private int _RoomWidth = 16;

        ///// <summary>
        ///// The height of a single room in tiles
        ///// </summary>
        //public int RoomHeight
        //{
        //    get => _RoomHeight;
        //    set { _RoomHeight = value; NotifyPropertyChanged(); }
        //}
        //private int _RoomHeight = 7;

        ///// <summary>
        ///// The filepath of the image used as the default background for 
        ///// rooms in this level
        ///// </summary>
        //public string RoomBackgroundImageSource
        //{
        //    get => _RoomBackgroundImageSource;
        //    set { _RoomBackgroundImageSource = value; NotifyPropertyChanged(); }
        //}
        //private string _RoomBackgroundImageSource;

        private LevelPropertiesViewModel _LevelProperties = new LevelPropertiesViewModel();
        public LevelPropertiesViewModel LevelProperties
        {
            get => _LevelProperties;
            set { _LevelProperties = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// A collection of room blueprints that can be used when generating
        /// this level
        /// </summary>
        public IBlueprintLibrary BlueprintLibrary
        {
            get => _BlueprintLibrary;
            set { _BlueprintLibrary = value; NotifyPropertyChanged(); }
        }
        private IBlueprintLibrary _BlueprintLibrary;


        public LevelViewModel()
        {
            BlueprintLibrary = new BlueprintLibraryViewModel(this);
        }
    }
}
