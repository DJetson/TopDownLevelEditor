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
            get => (PaletteViewModel.PaletteGridSize.X * PaletteGridY) + PaletteGridX;
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

        private TileViewModel _TileModel;
        public TileViewModel TileModel
        {
            get => _TileModel;
            set { _TileModel = value; NotifyPropertyChanged(); }
        }

        private bool _IsValidBrush = false;
        public bool IsValidBrush
        {
            get => _IsValidBrush;
            set { _IsValidBrush = value; NotifyPropertyChanged(); }
        }

        public TileBrushViewModel(LevelPaletteViewModel paletteViewModel, int paletteX, int paletteY)
        {
            PaletteViewModel = paletteViewModel;
            PaletteGridX = paletteX;
            PaletteGridY = paletteY;
            TileModel = new TileViewModel(paletteViewModel, paletteX, paletteY);
        }
    }
}
