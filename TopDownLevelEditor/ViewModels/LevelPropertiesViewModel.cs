using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownLevelEditor.ViewModels
{
    public class LevelPropertiesViewModel : NotifyBase
    {
        /// <summary>
        /// The width of a single tile in pixels
        /// </summary>
        public double TileWidth
        {
            get => _TileWidth;
            set { _TileWidth = value; NotifyPropertyChanged(); }
        }
        private double _TileWidth = 64.0f;

        /// <summary>
        /// The height of a single tile in pixels
        /// </summary>
        public double TileHeight
        {
            get => _TileHeight;
            set { _TileHeight = value; NotifyPropertyChanged(); }
        }
        private double _TileHeight = 64.0f;

        /// <summary>
        /// The width of a single room in tiles
        /// </summary>
        public int RoomWidth
        {
            get => _RoomWidth;
            set { _RoomWidth = value; NotifyPropertyChanged(); }
        }
        private int _RoomWidth = 12;

        /// <summary>
        /// The height of a single room in tiles
        /// </summary>
        public int RoomHeight
        {
            get => _RoomHeight;
            set { _RoomHeight = value; NotifyPropertyChanged(); }
        }
        private int _RoomHeight = 7;

        /// <summary>
        /// RoomWidth * TileWidth
        /// </summary>
        public double ActualWidth
        {
            get => RoomWidth * TileWidth;
            //set { _ActualWidth = value; NotifyPropertyChanged(); }
        }

        public double BackgroundWidth
        {
            get => ActualWidth + (2 * TileWidth);
        }
        //private double _ActualWidth = 0;
        public double BackgroundHeight
        {
            get => ActualHeight + (2 * TileHeight);
        }

        /// <summary>
        /// RoomHeight * TileHeight
        /// </summary>
        public double ActualHeight
        {
            get => RoomHeight * TileHeight;
            //set { _ActualHeight = value; NotifyPropertyChanged(); }
        }
        //private double _ActualHeight = 0;

        /// <summary>
        /// The filepath of the image used as the default background for 
        /// rooms in this level
        /// </summary>
        public string RoomBackgroundImageSource
        {
            get => _RoomBackgroundImageSource;
            set { _RoomBackgroundImageSource = value; NotifyPropertyChanged(); }
        }
        private string _RoomBackgroundImageSource = "C:\\Users\\DMalD\\source\\repos\\TopDownLevelEditor\\TopDownLevelEditor\\Assets\\PNG\\RoomBackground.png";
    }
}
