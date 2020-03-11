using Microsoft.VisualStudio.PlatformUI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TopDownLevelEditor.ViewModels
{
    /// <summary>
    /// Level Properties
    /// </summary>
    [Serializable]
    public class LevelBlueprintPropertiesViewModel : NotifyBase/*, ISerializable*/
    {
        private LevelPaletteViewModel _PaletteViewModel;
        public LevelPaletteViewModel PaletteViewModel
        {
            get => _PaletteViewModel;
            set { _PaletteViewModel = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// The width of an <see cref="Interfaces.IRoomBlueprint"/> <see cref="Interfaces.ITile"/> grid in <see cref="TileWidth"/> units
        /// </summary>
        public int RoomWidth
        {
            get => _RoomWidth;
            set
            {
                _RoomWidth = value;
                NotifyPropertyChanged();
                PaletteViewModel.NotifyPropertyChanged(nameof(PaletteViewModel.BackgroundWidth));
            }
        }
        private int _RoomWidth = 13;

        /// <summary>
        /// The height of an <see cref="Interfaces.IRoomBlueprint"/> <see cref="Interfaces.ITile"/> grid in <see cref="TileHeight"/> units
        /// </summary>
        public int RoomHeight
        {
            get => _RoomHeight;
            set
            {
                _RoomHeight = value;
                NotifyPropertyChanged();
                PaletteViewModel.NotifyPropertyChanged(nameof(PaletteViewModel.BackgroundHeight));
            }
        }
        private int _RoomHeight = 7;

        public DelegateCommand BrowseForRoomBackgroundCommand
        {
            get => new DelegateCommand(BrowseForRoomBackground_Execute);
        }

        public DelegateCommand BrowseForTilePaletteCommand
        {
            get => new DelegateCommand(BrowseForTilePalette_Execute);
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
