using Microsoft.VisualStudio.PlatformUI;
using Microsoft.Win32;
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

        public DelegateCommand SaveLevelBlueprintCommand
        {
            get => new DelegateCommand(SaveLevelBlueprint_Execute);/*_SaveLevelBlueprintCommand;*/
        }

        public DelegateCommand LoadLevelBlueprintCommand
        {
            get => new DelegateCommand(LoadLevelBlueprint_Execute); //_LoadLevelBlueprintCommand;
        }

        public MainWindowViewModel()
        {
        }

        private void LoadLevelBlueprint_Execute(object obj)
        {
            var openDialog = new OpenFileDialog()
            {
                Filter = "Level Blueprint (.lbp)|*.lbp",
                DefaultExt = ".lbp",
                AddExtension = true,
                CheckPathExists = true,
            };

            openDialog.FileOk += (sender, e) =>
            {
                var Sender = sender as OpenFileDialog;

                IFormatter formatter = new BinaryFormatter();
                FileStream s = new FileStream(Sender.FileName, FileMode.Open);
                LevelContext = (LevelBlueprintViewModel)formatter.Deserialize(s);
                s.Close();
            };

            openDialog.ShowDialog();
        }

        private void SaveLevelBlueprint_Execute(object obj)
        {
            var saveDialog = new SaveFileDialog()
            {
                Filter = "Level Blueprint (.lbp)|*.lbp",
                DefaultExt = ".lbp",
                AddExtension = true,
                CheckPathExists = true,
                OverwritePrompt = true,
            };

            saveDialog.FileOk += (sender, e) =>
            {
                var Sender = sender as SaveFileDialog;

                IFormatter formatter = new BinaryFormatter();
                FileStream s = new FileStream(Sender.FileName, FileMode.Create);
                formatter.Serialize(s, LevelContext);
                s.Close();
            };

            saveDialog.ShowDialog();
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
