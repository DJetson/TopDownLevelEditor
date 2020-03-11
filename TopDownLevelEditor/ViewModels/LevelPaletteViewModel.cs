using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    [Serializable]
    public class LevelPaletteViewModel : NotifyBase, IPaletteViewModel
    {
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

        /// <summary>
        /// The filepath of the image used as the default background for 
        /// rooms in this level
        /// </summary>
        public string TilePaletteImageSource
        {
            get => _TilePaletteImageSource;
            set { _TilePaletteImageSource = value; NotifyPropertyChanged(); }
        }
        private string _TilePaletteImageSource = "C:\\Users\\DMalD\\source\\repos\\TopDownLevelEditor\\TopDownLevelEditor\\Assets\\PNG\\Palette.png";

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

        public void SetTileBrush(int brushX, int brushY)
        {
            SelectedTileBrush.PaletteGridX = brushX;
            SelectedTileBrush.PaletteGridY = brushY;
        }

        public TileBrushViewModel GetTileBrush()
        {
            return SelectedTileBrush;
        }
    }
}
