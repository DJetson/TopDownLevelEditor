using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    [Serializable]
    public class TileBrushViewModel : NotifyBase, ITileBrush
    {
        public int TileId
        {
            get => (32 * PaletteGridY) + PaletteGridX;
        }

        private int _PaletteGridX;
        public int PaletteGridX
        {
            get => _PaletteGridX;
            set { _PaletteGridX = value; NotifyPropertyChanged(); }
        }

        private int _PaletteGridY;
        public int PaletteGridY
        {
            get => _PaletteGridY;
            set { _PaletteGridY = value; NotifyPropertyChanged(); }
        }
    }
}
