using Microsoft.VisualStudio.PlatformUI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TopDownLevelEditor.Views.Windows;

namespace TopDownLevelEditor.ViewModels
{
    /// <summary>
    /// Level Properties
    /// </summary>
    [Serializable]
    public class LevelBlueprintPropertiesViewModel : NotifyDependentsBase/*, ISerializable*/
    {
        private LevelPaletteViewModel _PaletteViewModel;
        public LevelPaletteViewModel PaletteViewModel
        {
            get => _PaletteViewModel;
            set { _PaletteViewModel = value; NotifyPropertyChanged(); }
        }

        private Point2 _RoomSize = new Point2(13, 7);
        public Point2 RoomSize
        {
            get => _RoomSize;
            set { _RoomSize = value; NotifyPropertyChanged(); NotifyDependentProperties(); }
        }

        protected override void NotifyDependentProperties([CallerMemberName]string property = "")
        {
            PaletteViewModel.NotifyPropertyChanged(nameof(PaletteViewModel.BackgroundSize));
            //PaletteViewModel.NotifyPropertyChanged(nameof(PaletteViewModel.BackgroundHeight));
        }


        ///// <summary>
        ///// The width of an <see cref="Interfaces.IRoomBlueprint"/> <see cref="Interfaces.ITile"/> grid in <see cref="TileWidth"/> units
        ///// </summary>
        //public int RoomWidth
        //{
        //    get => _RoomWidth;
        //    set
        //    {
        //        _RoomWidth = value;
        //        NotifyPropertyChanged();
        //        PaletteViewModel.NotifyPropertyChanged(nameof(PaletteViewModel.BackgroundWidth));
        //    }
        //}
        //private int _RoomWidth = 13;

        ///// <summary>
        ///// The height of an <see cref="Interfaces.IRoomBlueprint"/> <see cref="Interfaces.ITile"/> grid in <see cref="TileHeight"/> units
        ///// </summary>
        //public int RoomHeight
        //{
        //    get => _RoomHeight;
        //    set
        //    {
        //        _RoomHeight = value;
        //        NotifyPropertyChanged();
        //        PaletteViewModel.NotifyPropertyChanged(nameof(PaletteViewModel.BackgroundHeight));
        //    }
        //}
        //private int _RoomHeight = 7;

        public DelegateCommand BrowseForRoomBackgroundCommand
        {
            get => new DelegateCommand(BrowseForRoomBackground_Execute);
        }


        public DelegateCommand BrowseForTilePaletteCommand
        {
            get => new DelegateCommand(BrowseForTilePalette_Execute);
        }

        public DelegateCommand ShowPaletteEditorViewCommand
        {
            get => new DelegateCommand(ShowPaletteEditorView_Execute);
        }

        private void ShowPaletteEditorView_Execute(object obj)
        {
            DataTemplatedViewModelWindow paletteEditorWindow = new DataTemplatedViewModelWindow()
            {
                DataContext = PaletteViewModel,
                Owner = Application.Current.MainWindow
            };
            paletteEditorWindow.ShowDialog();
            PaletteViewModel.ApplyPalette();
        }

        private void BrowseForTilePalette_Execute(object obj)
        {
            var tilePaletteFilePath = BrowseForImageFile();

            //Validation??

            PaletteViewModel.TilePaletteImageSource = tilePaletteFilePath;
        }

        private void BrowseForRoomBackground_Execute(object obj)
        {
            var roomBackgroundFilePath = BrowseForImageFile();

            //Validation??

            PaletteViewModel.RoomBackgroundImageSource = roomBackgroundFilePath;
        }

        private string BrowseForImageFile()
        {
            string filePath = string.Empty;
            var openDialog = new OpenFileDialog()
            {
                Filter = "Supported Image Files|*.bmp;*.png;*.jpg",
                FilterIndex = 0,
                //DefaultExt = ".png",
                //AddExtension = true,
                CheckPathExists = true,
                Multiselect = false
            };

            openDialog.FileOk += (sender, e) =>
            {
                var Sender = sender as OpenFileDialog;
                filePath = Sender.FileName;
            };

            openDialog.ShowDialog();
            return filePath;
        }

        private LevelBlueprintViewModel _LevelBlueprintViewModel;
        public LevelBlueprintViewModel LevelBlueprintViewModel
        {
            get => _LevelBlueprintViewModel;
            set { _LevelBlueprintViewModel = value; NotifyPropertyChanged(); }
        }

        public LevelBlueprintPropertiesViewModel(LevelBlueprintViewModel levelBlueprintViewModel)
        {
            LevelBlueprintViewModel = levelBlueprintViewModel;
            PaletteViewModel = new LevelPaletteViewModel(this);
        }
    }
}
