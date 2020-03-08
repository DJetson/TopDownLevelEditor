using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    public class MainWindowViewModel : NotifyBase
    {
        private LevelViewModel _LevelContext = new LevelViewModel();
        public LevelViewModel LevelContext
        {
            get => _LevelContext;
            set { _LevelContext = value; NotifyPropertyChanged(); }
        }

        //private IRoomBlueprint _EditorContext;
        //public IRoomBlueprint EditorContext
        //{
        //    get => _EditorContext;
        //    set { _EditorContext = value; NotifyPropertyChanged(); }
        //}

        public MainWindowViewModel()
        {
            //LevelContext.BlueprintLibrary.AddNewRoomBlueprint();
            //EditorContext = LevelContext.BlueprintLibrary.BlueprintItems.First();
        }

        private int _BrushX = 0;
        public int BrushX
        {
            get => _BrushX;
            set { _BrushX = value; NotifyPropertyChanged(); PaletteViewModel.SetTileBrush(BrushX, BrushY); }
        }

        private int _BrushY = 0;
        public int BrushY
        {
            get => _BrushY;
            set { _BrushY = value; NotifyPropertyChanged(); PaletteViewModel.SetTileBrush(BrushX, BrushY); }
        }
    }
}
