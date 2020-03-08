using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    public class RoomBlueprintViewModel : NotifyBase, IRoomBlueprint
    {
        private LevelViewModel _ParentLevel;
        public LevelViewModel ParentLevel
        {
            get => _ParentLevel;
            set { _ParentLevel = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<ITile> _Tiles = new ObservableCollection<ITile>();
        public ObservableCollection<ITile> Tiles
        {
            get => _Tiles;
            set { _Tiles = value; NotifyPropertyChanged(); }
        }

        private double _ZoomX = 0.52f;
        public double ZoomX
        {
            get => _ZoomX;
            set { _ZoomX = value; NotifyPropertyChanged(); }
        }

        private double _ZoomY = 0.52f;
        public double ZoomY
        {
            get => _ZoomY;
            set { _ZoomY = value; NotifyPropertyChanged(); }
        }

        private DelegateCommand _AddTileCommand;
        public DelegateCommand AddTileCommand
        {
            get => _AddTileCommand;
            set { _AddTileCommand = value; NotifyPropertyChanged(); }
        }

        private DelegateCommand _ZoomCommand;
        public DelegateCommand ZoomCommand
        {
            get => _ZoomCommand;
            set { _ZoomCommand = value; NotifyPropertyChanged(); }
        }

        private Rect _TileGridViewport = new Rect(0,0,64,64);
        public Rect TileGridViewport
        {
            get => _TileGridViewport;
            set { _TileGridViewport = value; NotifyPropertyChanged(); }
        }

        

        public RoomBlueprintViewModel()
        {
            AddTileCommand = new DelegateCommand(AddTileCommand_Execute, AddTileCommand_CanExecute);
            ZoomCommand = new DelegateCommand(ZoomCommand_Execute, ZoomCommand_CanExecute);
        }

        private bool ZoomCommand_CanExecute(object obj)
        {
            double amount = double.Parse(obj.ToString());


            if (ZoomX >= 2.0f || ZoomY >= 2.0f)
            {
                if (amount < 0)
                    return false;
            }
            if(ZoomX < 0.51f || ZoomY < 0.51f)
            {
                if (amount > 0)
                    return false;
            }

            return true;
        }

        private void ZoomCommand_Execute(object obj)
        {
            double amount = double.Parse(obj.ToString());
            ZoomX -= amount;
            ZoomY -= amount;
        }

        private bool AddTileCommand_CanExecute(object obj)
        {
            //throw new NotImplementedException();
            return true;
        }

        private void AddTileCommand_Execute(object obj)
        {
            var pos = Mouse.GetPosition(obj as IInputElement);

            int tileUnitX = (int)(pos.X / ParentLevel.LevelProperties.TileWidth);
            int tileUnitY = (int)(pos.Y / ParentLevel.LevelProperties.TileHeight);

            int tileBrushX = PaletteViewModel.GetTileBrush().TilePaletteX;
            int tileBrushY = PaletteViewModel.GetTileBrush().TilePaletteY;

            var existingTile = Tiles.Where(e => e.TileUnitX == tileUnitX && e.TileUnitY == tileUnitY).FirstOrDefault();

            if (existingTile != null)
            {
                Tiles.Remove(existingTile);
            }
            Tiles.Add(new TileViewModel(tileBrushX, tileBrushY)
            {
                TileWidth = ParentLevel.LevelProperties.TileWidth,
                TileHeight = ParentLevel.LevelProperties.TileHeight,
                TileUnitX = tileUnitX,
                TileUnitY = tileUnitY,
                DrawPositionX = tileUnitX * ParentLevel.LevelProperties.TileWidth,
                DrawPositionY = tileUnitY * ParentLevel.LevelProperties.TileHeight
            });
        }
    }
}
