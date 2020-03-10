using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TopDownLevelEditor.ViewModels
{
    /// <summary>
    /// Level Properties
    /// </summary>
    [Serializable]
    public class LevelBlueprintPropertiesViewModel : NotifyBase/*, ISerializable*/
    {
        /// <summary>
        /// The width of a single <see cref="Interfaces.ITile"/> in pixels
        /// </summary>
        public double TileWidth
        {
            get => _TileWidth;
            set
            {
                _TileWidth = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ActualWidth));
                NotifyPropertyChanged(nameof(BackgroundWidth));
                NotifyPropertyChanged(nameof(TileGridViewport));
            }
        }
        private double _TileWidth = 64.0f;

        /// <summary>
        /// The height of a single <see cref="Interfaces.ITile"/> in pixels
        /// </summary>
        public double TileHeight
        {
            get => _TileHeight;
            set
            {
                _TileHeight = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ActualHeight));
                NotifyPropertyChanged(nameof(BackgroundHeight));
                NotifyPropertyChanged(nameof(TileGridViewport));
            }
        }
        private double _TileHeight = 64.0f;

        /// <summary>
        /// The width of an <see cref="Interfaces.IRoomBlueprint"/> <see cref="Interfaces.ITile"/> grid in <see cref="TileWidth"/> units
        /// </summary>
        public int RoomWidth
        {
            get => _RoomWidth;
            set { _RoomWidth = value; NotifyPropertyChanged(); }
        }
        private int _RoomWidth = 12;

        /// <summary>
        /// The height of an <see cref="Interfaces.IRoomBlueprint"/> <see cref="Interfaces.ITile"/> grid in <see cref="TileHeight"/> units
        /// </summary>
        public int RoomHeight
        {
            get => _RoomHeight;
            set { _RoomHeight = value; NotifyPropertyChanged(); }
        }
        private int _RoomHeight = 7;

        /// <summary>
        /// Actual width (in Density Independent Pixels) of the Tile Grid portion of an <see cref="Interfaces.IRoomBlueprint"/>
        /// </summary>
        public double ActualWidth
        {
            get => RoomWidth * TileWidth;
        }

        /// <summary>
        /// Actual height (in Density Independent Pixels) of the Tile Grid portion of an <see cref="Interfaces.IRoomBlueprint"/>
        /// </summary>
        public double ActualHeight
        {
            get => RoomHeight * TileHeight;
        }

        /// <summary>
        /// Actual width (in Density Independent Pixels) of the entire background portion of an <see cref="Interfaces.IRoomBlueprint"/>
        /// This is generally equal to <see cref="ActualWidth"/> plus two additional <see cref="TileWidth"/>s
        /// </summary>
        public double BackgroundWidth
        {
            get => ActualWidth + (2 * TileWidth);
        }

        /// <summary>
        /// Actual height (in Density Independent Pixels) of the entire background portion of an <see cref="Interfaces.IRoomBlueprint"/>
        /// This is generally equal to <see cref="ActualHeight"/> plus two additional <see cref="TileHeight"/>s
        /// </summary>
        public double BackgroundHeight
        {
            get => ActualHeight + (2 * TileHeight);
        }

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

        /// <summary>
        /// The filepath of the image used as the default background for 
        /// rooms in this level
        /// </summary>
        public string TilePaletteImageSource
        {
            get => _TilePaletteImageSource;
            set { _TilePaletteImageSource = value; NotifyPropertyChanged(); }
        }
        private string _TilePaletteImageSource = "C:\\Users\\DMalD\\source\\repos\\TopDownLevelEditor\\TopDownLevelEditor\\Assets\\PNG\\Palette.png";

        public Rect TileGridViewport
        {
            get => new Rect(0, 0, TileWidth, TileHeight);
        }

        public LevelBlueprintPropertiesViewModel()
        {

        }

        //public LevelBlueprintPropertiesViewModel(SerializationInfo info, StreamingContext context)
        //{
        //    DeserializeImageSources(info, context);
        //    TileWidth = info.GetValue<double>(nameof(TileWidth));
        //    TileHeight = info.GetValue<double>(nameof(TileHeight));
        //    RoomWidth = info.GetValue<int>(nameof(RoomWidth));
        //    RoomHeight = info.GetValue<int>(nameof(RoomHeight));
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    SerializeImageSources(info, context);
        //    info.AddValue(nameof(TileWidth), _TileWidth);
        //    info.AddValue(nameof(TileHeight), _TileHeight);
        //    info.AddValue(nameof(RoomWidth), _RoomWidth);
        //    info.AddValue(nameof(RoomHeight), _RoomHeight);
        //}

        //private void SerializeImageSources(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue(nameof(RoomBackgroundImageSource), _RoomBackgroundImageSource, typeof(string));
        //    info.AddValue(nameof(TilePaletteImageSource), _TilePaletteImageSource, typeof(string));

        //    //Serialize the in memory image data here
        //}

        //private void DeserializeImageSources(SerializationInfo info, StreamingContext context)
        //{
        //    RoomBackgroundImageSource = info.GetValue<string>(nameof(RoomBackgroundImageSource));
        //    TilePaletteImageSource = info.GetValue<string>(nameof(TilePaletteImageSource));
        //}
    }
}
