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
        private LevelPaletteViewModel _PaletteViewModel;
        public LevelPaletteViewModel PaletteViewModel
        {
            get => _PaletteViewModel;
            set { _PaletteViewModel = value; NotifyPropertyChanged(); }
        }

        public int TileId
        {
            get => (PaletteViewModel.PaletteGridWidth * PaletteGridY) + PaletteGridX;
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

        public TileBrushViewModel(LevelPaletteViewModel paletteViewModel, int paletteX, int paletteY)
        {
            PaletteViewModel = paletteViewModel;
            PaletteGridX = paletteX;
            PaletteGridY = paletteY;
        }
    }
}
