using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    public class TileViewModel : NotifyBase, ITile
    {
        private int _Id;
        public int Id
        {
            get => _Id;
            set { _Id = value; NotifyPropertyChanged(); }
        }

        private int _TilePaletteX;
        public int TilePaletteX
        {
            get => _TilePaletteX;
            set { _TilePaletteX = value; NotifyPropertyChanged(); }
        }

        private int _TilePaletteY;
        public int TilePaletteY
        {
            get => _TilePaletteY;
            set { _TilePaletteY = value; NotifyPropertyChanged(); }
        }

        private double _DrawPositionX = 0.0f;
        public double DrawPositionX
        {
            get => _DrawPositionX;
            set { _DrawPositionX = value; NotifyPropertyChanged(); }
        }

        private double _DrawPositionY = 0.0f;
        public double DrawPositionY
        {
            get => _DrawPositionY;
            set { _DrawPositionY = value; NotifyPropertyChanged(); }
        }

        private double _TileWidth = 0.0f;
        public double TileWidth
        {
            get => _TileWidth;
            set { _TileWidth = value; NotifyPropertyChanged(); }
        }

        private Rect _TileViewBox = new Rect(0, 0, 0.03125f, 0.03125f);
        public Rect TileViewBox
        {
            get => _TileViewBox;
            set { _TileViewBox = value; NotifyPropertyChanged(); }
        }

        private double _TileHeight = 0.0f;
        public double TileHeight
        {
            get => _TileHeight;
            set { _TileHeight = value; NotifyPropertyChanged(); }
        }

        private int _TileUnitX = 0;
        public int TileUnitX
        {
            get => _TileUnitX;
            set { _TileUnitX = value; NotifyPropertyChanged(); }
        }

        private int _TileUnitY = 0;
        public int TileUnitY
        {
            get => _TileUnitY;
            set { _TileUnitY = value; NotifyPropertyChanged(); }
        }

        public TileViewModel(int tileX, int tileY)
        {
            TileViewBox = new Rect(tileX * 0.03125f, tileY * 0.03125f, 0.03125f, 0.03125f);
        }

    }
}
