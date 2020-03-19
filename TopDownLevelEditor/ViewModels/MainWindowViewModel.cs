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
using System.Windows;
using TopDownLevelEditor.Interfaces;
using TopDownLevelEditor.Serialization;
using TopDownLevelEditor.Views.Windows;

namespace TopDownLevelEditor.ViewModels
{
    public class MainWindowViewModel : NotifyBase
    {
        private static string _ApplicationName = "TopDown Level Editor";

        //private string _WindowTitle = _ApplicationName;
        public string WindowTitle
        {
            get
            {
                if (String.IsNullOrEmpty(CurrentFile?.Filename))
                    return _ApplicationName;
                else
                {
                    if (CurrentFile.HasChanged)
                        return $"{_ApplicationName} - {CurrentFile.DisplayName}*";
                    else
                        return $"{_ApplicationName} - {CurrentFile.DisplayName}";
                }
            }
        }

        private static LevelBlueprintViewModel _CurrentFile = null;
        public static LevelBlueprintViewModel CurrentFile
        {
            get => _CurrentFile;
            private set { _CurrentFile = value; }
        }


        private LevelBlueprintViewModel _LevelContext = new LevelBlueprintViewModel();
        public LevelBlueprintViewModel LevelContext
        {
            get => _LevelContext;
            set { _LevelContext = value; NotifyPropertyChanged(); CurrentFile = value; NotifyPropertyChanged(nameof(WindowTitle)); }
        }

        public DelegateCommand SaveLevelBlueprintCommand
        {
            get => new DelegateCommand(SaveLevelBlueprint_Execute);/*_SaveLevelBlueprintCommand;*/
        }

        public DelegateCommand LoadLevelBlueprintCommand
        {
            get => new DelegateCommand(LoadLevelBlueprint_Execute); //_LoadLevelBlueprintCommand;
        }

        public DelegateCommand ExportAssetFilesCommand
        {
            get => new DelegateCommand(ExportAssetFiles_Execute);
        }

        private void ExportAssetFiles_Execute(object obj)
        {
            foreach (var blueprint in CurrentFile.BlueprintLibrary.BlueprintItems)
            {
                int index = CurrentFile.BlueprintLibrary.BlueprintItems.IndexOf(blueprint);
                string assetFileName = $"{Path.GetFileNameWithoutExtension(CurrentFile.Filename)}-Room{index}.asset";
                string assetFilePath = $"{Path.GetDirectoryName(CurrentFile.Filepath)}\\{assetFileName}";

                TokenizeRoomSerializer t = new TokenizeRoomSerializer();

                t.BlueprintId = index;
                t.m_Name = Path.GetFileNameWithoutExtension(assetFileName);

                t.RawData = t.GenerateRawDataString(blueprint);

                File.WriteAllLines(assetFilePath, t.GenerateFileLines());
            }
        }

        public DelegateCommand ImportAssetFilesCommand
        {
            get => new DelegateCommand(ImportAssetFiles_Execute);
        }

        private void ImportAssetFiles_Execute(object obj)
        {
            throw new NotImplementedException();
        }

        public DelegateCommand ShowLevelBlueprintPropertiesCommand
        {
            get => new DelegateCommand(ShowLevelBlueprintProperties_Execute);
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
            IFormatter formatter = new BinaryFormatter();
            FileStream s = new FileStream(LevelBlueprintViewModel.DefaultFilePath, FileMode.Open, FileAccess.Read);
            LevelContext = (LevelBlueprintViewModel)formatter.Deserialize(s);
            s.Close();
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
                var newLevelContext = (LevelBlueprintViewModel)formatter.Deserialize(s);
                newLevelContext.Filepath = Sender.FileName;
                LevelContext = newLevelContext;
                s.Close();
            };

            openDialog.ShowDialog();
        }

        private void SaveLevelBlueprint_Execute(object obj)
        {
            if (String.IsNullOrEmpty(LevelContext.Filepath))
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
                    LevelContext.Filepath = Sender.FileName;
                    IFormatter formatter = new BinaryFormatter();
                    FileStream s = (FileStream)Sender.OpenFile();//new FileStream(Sender.FileName, FileMode.Create);
                    formatter.Serialize(s, LevelContext);
                    s.Close();
                    CurrentFile.HasChanged = false;
                };

                saveDialog.ShowDialog();
            }
            else
            {
                IFormatter formatter = new BinaryFormatter();
                FileStream s = new FileStream(LevelContext.Filepath, FileMode.Create);
                formatter.Serialize(s, LevelContext);
                s.Close();
            }
        }

        private int _BrushX = 0;
        public int BrushX
        {
            get => _BrushX;
            set { _BrushX = value; NotifyPropertyChanged(); LevelContext.LevelProperties.PaletteViewModel.SetTileBrush(BrushX, BrushY); }
        }

        private int _BrushY = 0;
        public int BrushY
        {
            get => _BrushY;
            set { _BrushY = value; NotifyPropertyChanged(); LevelContext.LevelProperties.PaletteViewModel.SetTileBrush(BrushX, BrushY); }
        }
    }
}
