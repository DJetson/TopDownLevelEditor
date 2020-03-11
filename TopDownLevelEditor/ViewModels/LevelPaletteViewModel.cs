using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    [Serializable]
    public class LevelPaletteViewModel : NotifyBase, IPaletteViewModel
    {
        private LevelBlueprintPropertiesViewModel _LevelProperties;
        public LevelBlueprintPropertiesViewModel LevelProperties
        {
            get => _LevelProperties;
            set { _LevelProperties = value; NotifyPropertyChanged(); }
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

                UpdatePaletteInfo(TilePaletteImageSource);
                NotifyPropertyChanged(nameof(PaletteActualWidth));
                NotifyPropertyChanged(nameof(PaletteGridWidth));
                NotifyPropertyChanged(nameof(TileGridViewport));
                LevelProperties.NotifyPropertyChanged(nameof(LevelProperties.RoomWidth));
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

                UpdatePaletteInfo(TilePaletteImageSource);
                NotifyPropertyChanged(nameof(PaletteActualHeight));
                NotifyPropertyChanged(nameof(PaletteGridHeight));
                NotifyPropertyChanged(nameof(TileGridViewport));
                LevelProperties.NotifyPropertyChanged(nameof(LevelProperties.RoomHeight));
            }
        }
        private double _TileHeight = 64.0f;

        /// <summary>
        /// Actual width (in Density Independent Pixels) of the Tile Grid portion of an <see cref="Interfaces.IRoomBlueprint"/>
        /// </summary>
        public double ActualWidth
        {
            get => (LevelProperties?.RoomWidth ?? 0) * TileWidth;
        }

        /// <summary>
        /// Actual height (in Density Independent Pixels) of the Tile Grid portion of an <see cref="Interfaces.IRoomBlueprint"/>
        /// </summary>
        public double ActualHeight
        {
            get => (LevelProperties?.RoomHeight ?? 0) * TileHeight;
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

        public Rect TileGridViewport
        {
            get => new Rect(0, 0, TileWidth, TileHeight);
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
            set { _TilePaletteImageSource = value; NotifyPropertyChanged(); UpdatePaletteInfo(value); }
        }
        private string _TilePaletteImageSource = "C:\\Users\\DMalD\\source\\repos\\TopDownLevelEditor\\TopDownLevelEditor\\Assets\\PNG\\Palette.png";

        [NonSerialized]
        private BitmapDecoder _PaletteInfo;
        public BitmapDecoder PaletteInfo
        {
            get
            {
                if (_PaletteInfo == null)
                    UpdatePaletteInfo(TilePaletteImageSource);
                return _PaletteInfo;
            }
            set
            {
                _PaletteInfo = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(PaletteActualWidth));
                NotifyPropertyChanged(nameof(PaletteActualHeight));
                NotifyPropertyChanged(nameof(PaletteGridWidth));
                NotifyPropertyChanged(nameof(PaletteGridHeight));
            }
        }
        private void UpdatePaletteInfo(string value)
        {
            using (var imageStream = File.OpenRead(TilePaletteImageSource))
            {
                _PaletteInfo = BitmapDecoder.Create(imageStream, BitmapCreateOptions.IgnoreColorProfile,
                    BitmapCacheOption.Default);

                PaletteActualWidth = _PaletteInfo?.Frames?.First()?.PixelWidth ?? 0;
                PaletteActualHeight = _PaletteInfo?.Frames?.First()?.PixelHeight ?? 0;
            }

            NotifyPropertyChanged(nameof(PaletteActualHeight));
            NotifyPropertyChanged(nameof(PaletteActualWidth));
            NotifyPropertyChanged(nameof(PaletteGridHeight));
            NotifyPropertyChanged(nameof(PaletteGridWidth));

            TileBrushItems.Clear();

            for (int y = 0; y < PaletteGridHeight; y++)
            {
                for (int x = 0; x < PaletteGridWidth; x++)
                {
                    var newBrush = new TileBrushViewModel(this, x, y);

                    TileBrushItems.Add(newBrush);
                }
            }

            SelectedTileBrush = TileBrushItems.First();


            foreach(var room in LevelProperties.LevelBlueprintViewModel.BlueprintLibrary.BlueprintItems)
            {
                List<ITile> newTiles = new List<ITile>();
                foreach(var tile in room.Tiles)
                {
                    var newTile = new TileViewModel(this, tile.PaletteGridX, tile.PaletteGridY)
                    {
                        TileWidth = TileWidth,
                        TileHeight = TileHeight,
                        RoomGridX = tile.RoomGridX,
                        RoomGridY = tile.RoomGridY,
                    };
                    newTiles.Add(newTile);
                }
                room.Tiles.Clear();
                newTiles.ForEach(e => room.Tiles.Add(e));
            }
        }

        private double _PaletteActualHeight = 0;
        public double PaletteActualHeight
        {
            get => _PaletteActualHeight;
            set
            {
                _PaletteActualHeight = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(PaletteGridHeight));
            }
        }

        private double _PaletteActualWidth = 0;
        public double PaletteActualWidth
        {
            get => _PaletteActualWidth;
            set
            {
                _PaletteActualWidth = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(PaletteGridWidth));
            }
        }

        public int PaletteGridHeight
        {
            get => (int)(PaletteActualHeight / TileHeight);
        }

        public int PaletteGridWidth
        {
            get => (int)(PaletteActualWidth / TileWidth);
        }

        private ObservableCollection<ITileBrush> _TileBrushItems = new ObservableCollection<ITileBrush>();
        public ObservableCollection<ITileBrush> TileBrushItems
        {
            get => _TileBrushItems;
            set { _TileBrushItems = value; NotifyPropertyChanged(); }
        }

        private ITileBrush _SelectedTileBrush;
        public ITileBrush SelectedTileBrush
        {
            get => _SelectedTileBrush;
            set { _SelectedTileBrush = value; NotifyPropertyChanged(); }
        }

        public void SetTileBrush(int brushX, int brushY)
        {
            SelectedTileBrush = TileBrushItems.Where(e => e.PaletteGridX == brushX && e.PaletteGridY == brushY).First();
            //SelectedTileBrush.PaletteGridX = brushX;
            //SelectedTileBrush.PaletteGridY = brushY;
        }

        public ITileBrush GetTileBrush()
        {
            return SelectedTileBrush;
        }

        public LevelPaletteViewModel(LevelBlueprintPropertiesViewModel levelProperties)
        {
            LevelProperties = levelProperties;
            TileBrushItems = new ObservableCollection<ITileBrush>();
            UpdatePaletteInfo(TilePaletteImageSource);
            //for (int y = 0; y < PaletteGridHeight; y++)
            //{
            //    for (int x = 0; x < PaletteGridWidth; x++)
            //    {
            //        var newBrush = new TileBrushViewModel(this, x, y);

            //        TileBrushItems.Add(newBrush);
            //    }
            //}

            //SelectedTileBrush = TileBrushItems.First();
        }
    }
}
