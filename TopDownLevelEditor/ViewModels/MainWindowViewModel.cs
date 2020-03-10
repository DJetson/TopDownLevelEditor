using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    public class MainWindowViewModel : NotifyBase
    {
        private LevelBlueprintViewModel _LevelContext = new LevelBlueprintViewModel();
        public LevelBlueprintViewModel LevelContext
        {
            get => _LevelContext;
            set { _LevelContext = value; NotifyPropertyChanged(); }
        }

        //private DelegateCommand _SaveLevelBlueprintCommand;
        public DelegateCommand SaveLevelBlueprintCommand
        {
            get => new DelegateCommand(SaveLevelBlueprint_Execute);/*_SaveLevelBlueprintCommand;*/
            //set { _SaveLevelBlueprintCommand = value; NotifyPropertyChanged(); }
        }

        //private DelegateCommand _LoadLevelBlueprintCommand;
        public DelegateCommand LoadLevelBlueprintCommand
        {
            get => new DelegateCommand(LoadLevelBlueprint_Execute); //_LoadLevelBlueprintCommand;
            //set { _LoadLevelBlueprintCommand = value; NotifyPropertyChanged(); }
        }
        //private IRoomBlueprint _EditorContext;
        //public IRoomBlueprint EditorContext
        //{
        //    get => _EditorContext;
        //    set { _EditorContext = value; NotifyPropertyChanged(); }
        //}

        public MainWindowViewModel()
        {
            //SaveLevelBlueprintCommand = new DelegateCommand(SaveLevelBlueprint_Execute);
            //LoadLevelBlueprintCommand = new DelegateCommand(LoadLevelBlueprint_Execute);
            //LevelContext.BlueprintLibrary.AddNewRoomBlueprint();
            //EditorContext = LevelContext.BlueprintLibrary.BlueprintItems.First();
        }

        private void LoadLevelBlueprint_Execute(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream s = new FileStream("./test.lbp", FileMode.Open);
            LevelContext = (LevelBlueprintViewModel)formatter.Deserialize(s);
            s.Close();
        }

        private void SaveLevelBlueprint_Execute(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream s = new FileStream("./test.lbp", FileMode.Create);
            formatter.Serialize(s, LevelContext);
            s.Close();
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
