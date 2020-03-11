using Microsoft.Win32;
using Prism.Commands;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using TopDownLevelEditor.Views.Windows;

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

        public DelegateCommand<object> SaveLevelBlueprintCommand
        {
            get => new DelegateCommand<object>(SaveLevelBlueprint_Execute);/*_SaveLevelBlueprintCommand;*/
        }

        public DelegateCommand<object> LoadLevelBlueprintCommand
        {
            get => new DelegateCommand<object>(LoadLevelBlueprint_Execute); //_LoadLevelBlueprintCommand;
        }

        public DelegateCommand<object> ShowLevelBlueprintPropertiesCommand
        {
            get => new DelegateCommand<object>(ShowLevelBlueprintProperties_Execute);
        }

        private void ShowLevelBlueprintProperties_Execute(object obj)
        {
            DataTemplatedViewModelWindow levelBlueprintPropertiesWindow = new DataTemplatedViewModelWindow()
            {
                DataContext = LevelContext.LevelProperties,
                Owner = Application.Current.MainWindow
            };

            levelBlueprintPropertiesWindow.ShowDialog();
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
