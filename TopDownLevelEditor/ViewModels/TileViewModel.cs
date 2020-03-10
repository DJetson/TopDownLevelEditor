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
        public int Id
        {
            get => (PaletteGridY * 32) + PaletteGridX;
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
            get => RoomGridX * TileWidth;
        }

        public double RoomDrawPositionY
        {
            get => RoomGridY * TileHeight;
        }

        public Rect TileViewBox
        {
            //TODO: This should be changed so that it's calculated as:
            //      0,0,(PaletteGridX * (TileWidth/PaletteImageWidth)),(PaletteGridY * (TileHeight/PaletteImageHeight))
            get => new Rect(PaletteGridX * 0.03125f, PaletteGridY * 0.03125f, 0.03125f, 0.03125f);
        }

        private string _TilePaletteImageSource;
        public string TilePaletteImageSource
        {
            get => _TilePaletteImageSource;
            set { _TilePaletteImageSource = value; NotifyPropertyChanged(); }
        }

        private double _TileWidth = 0.0f;
        public double TileWidth
        {
            get => _TileWidth;
            set
            {
                _TileWidth = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(RoomDrawPositionX));
            }
        }

        private double _TileHeight = 0.0f;
        public double TileHeight
        {
            get => _TileHeight;
            set
            {
                _TileHeight = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(RoomDrawPositionY));
            }
        }

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

        public TileViewModel(int tileX, int tileY)
        {
            PaletteGridX = tileX;
            PaletteGridY = tileY;
        }
    }
}
