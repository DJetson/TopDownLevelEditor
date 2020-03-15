using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    [Serializable]
    public class LevelPaletteViewModel : NotifyDependentsBase, IPaletteViewModel
    {
        private LevelBlueprintPropertiesViewModel _LevelProperties;
        public LevelBlueprintPropertiesViewModel LevelProperties
        {
            get => _LevelProperties;
            set { _LevelProperties = value; NotifyPropertyChanged(); }
        }

        private Point2 _TileSize = new Point2(64, 64);
        public Point2 TileSize
        {
            get => _TileSize;
            set { _TileSize = value; NotifyPropertyChanged(); NotifyDependentProperties(); }
        }

        public double TileWidth
        {
            get => _TileSize.X;
            set { _TileSize.X = value; NotifyPropertyChanged(nameof(TileSize)); NotifyDependentProperties(nameof(TileSize)); }
        }

        public double TileHeight
        {
            get => _TileSize.Y;
            set { _TileSize.Y = value; NotifyPropertyChanged(nameof(TileSize)); NotifyDependentProperties(nameof(TileSize)); }
        }

        protected override void NotifyDependentProperties([CallerMemberName] string property = "")
        {
            switch (property)
            {
                case nameof(TileSize):
                    NotifyPropertyChanged(nameof(ActualSize));
                    NotifyPropertyChanged(nameof(BackgroundSize));

                    UpdatePaletteInfo(TilePaletteImageSource);
                    NotifyPropertyChanged(nameof(PaletteActualSize));
                    NotifyPropertyChanged(nameof(PaletteGridSize));
                    NotifyPropertyChanged(nameof(TileGridViewport));
                    LevelProperties.NotifyPropertyChanged(nameof(LevelProperties.RoomSize));
                    break;
                case nameof(PaletteActualSize):
                    NotifyPropertyChanged(nameof(PaletteGridSize));
                    break;
            }
        }

        public Point2 ActualSize
        {
            get => ((LevelProperties?.RoomSize ?? new Point2(0, 0)) * TileSize);
        }

        public Point2 BackgroundSize
        {
            get => ActualSize + (2 * TileSize);
        }

        public Rect TileGridViewport
        {
            get => new Rect(0, 0, TileSize.X, TileSize.Y);
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

        internal void ApplyPalette()
        {
            foreach (var brush in TileBrushItems.Where(e => e.IsValidBrush))
            {
                PaletteBrushes.Add(brush);
            }
        }

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
                NotifyPropertyChanged(nameof(PaletteActualSize));
                NotifyPropertyChanged(nameof(PaletteGridSize));
            }
        }

        private void UpdatePaletteInfo(string value)
        {
            using (var imageStream = File.OpenRead(TilePaletteImageSource))
            {
                _PaletteInfo = BitmapDecoder.Create(imageStream, BitmapCreateOptions.IgnoreColorProfile,
                    BitmapCacheOption.Default);

                PaletteActualSize.X = _PaletteInfo?.Frames?.First()?.PixelWidth ?? 0;
                PaletteActualSize.Y = _PaletteInfo?.Frames?.First()?.PixelHeight ?? 0;

            }

            NotifyPropertyChanged(nameof(PaletteActualSize));
            NotifyPropertyChanged(nameof(PaletteGridSize));

            TileBrushItems.Clear();

            for (int y = 0; y < PaletteGridSize.Y; y++)
            {
                for (int x = 0; x < PaletteGridSize.X; x++)
                {
                    var newBrush = new TileBrushViewModel(this, x, y);
                    TileBrushItems.Add(newBrush);
                }
            }

            ApplyPalette();

            SelectedTileBrush = PaletteBrushes.FirstOrDefault();

            foreach (var room in LevelProperties.LevelBlueprintViewModel.BlueprintLibrary.BlueprintItems)
            {
                List<ITile> newTiles = new List<ITile>();
                foreach (var tile in room.Tiles)
                {
                    var newTile = new TileViewModel(this, tile.PaletteGridX, tile.PaletteGridY)
                    {
                        RoomGridX = tile.RoomGridX,
                        RoomGridY = tile.RoomGridY,
                    };
                    newTiles.Add(newTile);
                }
                room.Tiles.Clear();
                newTiles.ForEach(e => room.Tiles.Add(e));
            }
        }

        private Point2 _PaletteActualSize = new Point2(0, 0);
        public Point2 PaletteActualSize
        {
            get => _PaletteActualSize;
            set
            {
                _PaletteActualSize = value;
                NotifyPropertyChanged();
                NotifyDependentProperties();
            }
        }

        public Point2Int PaletteGridSize
        {
            get => (PaletteActualSize / TileSize);
        }

        private ObservableCollection<ITileBrush> _PaletteBrushes = new ObservableCollection<ITileBrush>();
        public ObservableCollection<ITileBrush> PaletteBrushes
        {
            get => _PaletteBrushes;
            set { _PaletteBrushes = value; NotifyPropertyChanged(); }
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
            SelectedTileBrush = PaletteBrushes.Where(e => e.PaletteGridX == brushX && e.PaletteGridY == brushY).First();
        }

        public ITileBrush GetTileBrush()
        {
            return SelectedTileBrush;
        }

        public DelegateCommand SelectTileBrushCommand
        {
            get => new DelegateCommand(SelectTileBrush_Execute, SelectTileBrush_CanExecute);
        }

        private bool SelectTileBrush_CanExecute(object obj)
        {
            TileBrushViewModel brush = obj as TileBrushViewModel;
            if (brush == null && obj.ToString() != "Erase")
                return false;

            return true;
        }

        private void SelectTileBrush_Execute(object obj)
        {
            if (obj.ToString() == "Erase")
            {
                SelectedTileBrush = null;
                return;
            }

            TileBrushViewModel brush = obj as TileBrushViewModel;

            if (brush == null)
                return;

            SelectedTileBrush = brush;
        }

        public LevelPaletteViewModel(LevelBlueprintPropertiesViewModel levelProperties)
        {
            LevelProperties = levelProperties;
            TileBrushItems = new ObservableCollection<ITileBrush>();
            PaletteBrushes = new ObservableCollection<ITileBrush>();
            UpdatePaletteInfo(TilePaletteImageSource);
        }
    }
}
