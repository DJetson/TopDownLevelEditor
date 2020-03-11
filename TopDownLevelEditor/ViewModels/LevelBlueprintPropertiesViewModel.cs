using Microsoft.VisualStudio.PlatformUI;
using Microsoft.Win32;
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
        private LevelPaletteViewModel _PaletteViewModel = new LevelPaletteViewModel();
        public LevelPaletteViewModel PaletteViewModel
        {
            get => _PaletteViewModel;
            set { _PaletteViewModel = value; NotifyPropertyChanged(); }
        }

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
        //public string RoomBackgroundImageSource
        //{
        //    get => _RoomBackgroundImageSource;
        //    set { _RoomBackgroundImageSource = value; NotifyPropertyChanged(); }
        //}
        //private string _RoomBackgroundImageSource = "C:\\Users\\DMalD\\source\\repos\\TopDownLevelEditor\\TopDownLevelEditor\\Assets\\PNG\\RoomBackground.png";

        /// <summary>
        /// The filepath of the image used as the default background for 
        /// rooms in this level
        /// </summary>
        //public string TilePaletteImageSource
        //{
        //    get => _TilePaletteImageSource;
        //    set { _TilePaletteImageSource = value; NotifyPropertyChanged(); }
        //}
        //private string _TilePaletteImageSource = "C:\\Users\\DMalD\\source\\repos\\TopDownLevelEditor\\TopDownLevelEditor\\Assets\\PNG\\Palette.png";

        public Rect TileGridViewport
        {
            get => new Rect(0, 0, TileWidth, TileHeight);
        }

        public DelegateCommand BrowseForRoomBackgroundCommand
        {
            get => new DelegateCommand(BrowseForRoomBackground_Execute);
        }

        public DelegateCommand BrowseForTilePaletteCommand
        {
            get => new DelegateCommand(BrowseForTilePalette_Execute);
        }

        private void BrowseForTilePalette_Execute(object obj)
        {
            var tilePaletteFilePath = BrowseForImageFile();

            //Validation??

            PaletteViewModel.TilePaletteImageSource = tilePaletteFilePath;
        }

        private void BrowseForRoomBackground_Execute(object obj)
        {
            var roomBackgroundFilePath = BrowseForImageFile();

            //Validation??

            PaletteViewModel.RoomBackgroundImageSource = roomBackgroundFilePath;
        }

        private string BrowseForImageFile()
        {
            string filePath = string.Empty;
            var openDialog = new OpenFileDialog()
            {
                Filter = "Supported Image Files|*.bmp;*.png;*.jpg",
                FilterIndex = 0,
                //DefaultExt = ".png",
                //AddExtension = true,
                CheckPathExists = true,
                Multiselect = false
            };

            openDialog.FileOk += (sender, e) =>
            {
                var Sender = sender as OpenFileDialog;
                filePath = Sender.FileName;
            };

            openDialog.ShowDialog();
            return filePath;
        }

        public LevelBlueprintPropertiesViewModel()
        {

        }
    }
}
