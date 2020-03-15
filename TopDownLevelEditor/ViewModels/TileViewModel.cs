using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    [Serializable]
    public class TileViewModel : NotifyBase, ITile
    {
        private LevelPaletteViewModel _PaletteViewModel;
        public LevelPaletteViewModel PaletteViewModel
        {
            get => _PaletteViewModel;
            set { _PaletteViewModel = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(Id)); NotifyPropertyChanged(nameof(TileViewBox)); }
        }

        public int Id
        {
            get => (PaletteGridY * PaletteViewModel?.PaletteGridSize.X ?? 0) + PaletteGridX;
        }

        private int _PaletteGridX;
        public int PaletteGridX
        {
            get => _PaletteGridX;
            set
            {
                _PaletteGridX = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Id));
                NotifyPropertyChanged(nameof(TileViewBox));
            }
        }

        private int _PaletteGridY;
        public int PaletteGridY
        {
            get => _PaletteGridY;
            set
            {
                _PaletteGridY = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Id));
                NotifyPropertyChanged(nameof(TileViewBox));
            }
        }

        public double RoomDrawPositionX
        {
            get => RoomGridX * PaletteViewModel.TileWidth;
        }

        public double RoomDrawPositionY
        {
            get => RoomGridY * PaletteViewModel.TileHeight;
        }

        public Rect GetViewBox(int paletteX, int paletteY)
        {
            var palette = PaletteViewModel;

            var posX = paletteX * (palette.TileSize.X / palette.PaletteActualSize.X);
            var posY = paletteY * (palette.TileSize.Y / palette.PaletteActualSize.Y);
            var sizeX = (palette.TileSize.X / palette.PaletteActualSize.X);
            var sizeY = (palette.TileSize.Y / palette.PaletteActualSize.Y);

            return new Rect(posX, posY, sizeX, sizeY);
            //new Rect(PaletteGridX * (TileWidth / PaletteViewModel?.PaletteActualSize.X ?? 1),
            //                PaletteGridY * (TileHeight / PaletteViewModel?.PaletteActualSize.Y ?? 1),
            //                (TileWidth / PaletteViewModel?.PaletteActualSize.X ?? 1),
            //                (TileHeight / PaletteViewModel?.PaletteActualSize.Y ?? 1));
        }

        public Rect TileViewBox
        {
            //TODO: This should be changed so that it's calculated as:
            //      0,0,(PaletteGridX * (TileWidth/PaletteImageWidth)),(PaletteGridY * (TileHeight/PaletteImageHeight))
            //get => new Rect(PaletteGridX * (TileWidth / PaletteViewModel?.PaletteActualSize.X ?? 1), 
            //                PaletteGridY * (TileHeight / PaletteViewModel?.PaletteActualSize.Y ?? 1), 
            //                (TileWidth / PaletteViewModel?.PaletteActualSize.X ?? 1), 
            //                (TileHeight / PaletteViewModel?.PaletteActualSize.Y ?? 1));
            get => GetViewBox(PaletteGridX, PaletteGridY);
        }

        private string _TilePaletteImageSource;
        public string TilePaletteImageSource
        {
            get => _TilePaletteImageSource;
            set { _TilePaletteImageSource = value; NotifyPropertyChanged(); }
        }

        //private double _TileWidth = 0.0f;
        //public double TileWidth
        //{
        //    get => _TileWidth;
        //    set
        //    {
        //        _TileWidth = value;
        //        NotifyPropertyChanged();
        //        NotifyPropertyChanged(nameof(RoomDrawPositionX));
        //    }
        //}

        //private double _TileHeight = 0.0f;
        //public double TileHeight
        //{
        //    get => _TileHeight;
        //    set
        //    {
        //        _TileHeight = value;
        //        NotifyPropertyChanged();
        //        NotifyPropertyChanged(nameof(RoomDrawPositionY));
        //    }
        //}

        private int _RoomGridX = 0;
        public int RoomGridX
        {
            get => _RoomGridX;
            set
            {
                _RoomGridX = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(RoomDrawPositionX));
            }
        }

        private int _RoomGridY = 0;
        public int RoomGridY
        {
            get => _RoomGridY;
            set
            {
                _RoomGridY = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(RoomDrawPositionY));
            }
        }

        public TileViewModel(LevelPaletteViewModel paletteViewModel, int tileX, int tileY)
        {
            PaletteViewModel = paletteViewModel;
            PaletteGridX = tileX;
            PaletteGridY = tileY;
        }
    }
}
