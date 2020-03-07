using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    public class BrushSelectorViewModel : NotifyBase
    {
        private ITileBrush _Selected;
        public ITileBrush Selected
        {
            get => _Selected;
            set { _Selected = value; NotifyPropertyChanged(); }
        }

        private PaletteViewModel _PaletteViewModel;
        public PaletteViewModel PaletteViewModel
        {
            get => _PaletteViewModel;
            set { _PaletteViewModel = value; NotifyPropertyChanged(); }
        }
    }
}
