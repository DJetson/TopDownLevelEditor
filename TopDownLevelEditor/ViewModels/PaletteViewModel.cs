using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    public class PaletteViewModel : NotifyBase, IPaletteViewModel
    {
        private static PaletteViewModel _Instance;
        private static PaletteViewModel Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new PaletteViewModel();
                return _Instance;
            }
        }

        private string _ImageSource;
        public string ImageSource
        {
            get => _ImageSource;
            set { _ImageSource = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<ITileBrush> _TileBrushItems = new ObservableCollection<ITileBrush>();
        public ObservableCollection<ITileBrush> TileBrushItems
        {
            get => _TileBrushItems;
            set { _TileBrushItems = value; NotifyPropertyChanged(); }
        }

        private TileBrushViewModel _SelectedTileBrush = new TileBrushViewModel() { PaletteGridX = 0, PaletteGridY = 0 };
        public TileBrushViewModel SelectedTileBrush
        {
            get => _SelectedTileBrush;
            set { _SelectedTileBrush = value; NotifyPropertyChanged(); }
        }

        public static void SetTileBrush(int brushX, int brushY)
        {
            Instance.SelectedTileBrush.PaletteGridX = brushX;
            Instance.SelectedTileBrush.PaletteGridY = brushY;
        }

        public static TileBrushViewModel GetTileBrush()
        {
            return Instance.SelectedTileBrush;
        }
    }
}
