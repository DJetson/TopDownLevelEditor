using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    public class TileBrushViewModel : NotifyBase, ITileBrush
    {
        //private TileType _TileType = TileType.Terrain;
        //public TileType TileType
        //{
        //    get => _TileType;
        //}

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
    }
}
