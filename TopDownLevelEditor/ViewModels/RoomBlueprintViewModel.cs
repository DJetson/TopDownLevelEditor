using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    [Serializable]
    public class RoomBlueprintViewModel : NotifyBase, IRoomBlueprint
    {
        /// <summary>
        /// The <see cref="LevelBlueprintViewModel"/> which defines the <see cref="LevelBlueprintViewModel.BlueprintLibrary"/> that this <see cref="RoomBlueprintViewModel"/> belongs to.
        /// </summary>
        public ILevelBlueprint ParentLevel
        {
            get => _ParentLevel;
            set { _ParentLevel = value; NotifyPropertyChanged(); }
        }
        private ILevelBlueprint _ParentLevel;

        /// <summary>
        /// A collection of <see cref="ITile"/> objects that determine what and where particular objects may be placed in
        /// a room generated using this <see cref="IRoomBlueprint"/>
        /// </summary>
        public ObservableCollection<ITile> Tiles
        {
            get => _Tiles;
            set { _Tiles = value; NotifyPropertyChanged(); }
        }
        private ObservableCollection<ITile> _Tiles = new ObservableCollection<ITile>();

        /// <summary>
        /// Horizontal Zoom Factor used to scale the <see cref="Views.RoomBlueprintEditorView"/> for which this 
        /// <see cref="RoomBlueprintViewModel"/> serves as a <see cref="FrameworkElement.DataContext"/>
        /// </summary>
        public double ZoomX
        {
            get => _ZoomX;
            set { _ZoomX = value; NotifyPropertyChanged(); }
        }
        private double _ZoomX = 0.52f;

        /// <summary>
        /// Vertical zoom factor used to scale the <see cref="Views.RoomBlueprintEditorView"/> for which this 
        /// <see cref="RoomBlueprintViewModel"/> serves as a <see cref="FrameworkElement.DataContext"/>
        /// </summary>
        public double ZoomY
        {
            get => _ZoomY;
            set { _ZoomY = value; NotifyPropertyChanged(); }
        }
        private double _ZoomY = 0.52f;

        /// <summary>
        /// <see cref="DelegateCommand{T}"/> that can be used by a <see cref="ICommandSource"/> in the View Layer to trigger execution of the <see cref="RoomBlueprintViewModel.Zoom(double, double)"/> method
        /// </summary>
        public DelegateCommand<object> ZoomCommand
        {
            get => new DelegateCommand<object>(ZoomCommand_Execute, ZoomCommand_CanExecute);//_ZoomCommand;
        }

        /// <summary>
        /// <see cref="DelegateCommand{T}"/> that can be used by a <see cref="ICommandSource"/> in the View Layer to trigger execution of the <see cref="RoomBlueprintViewModel.AddTile(int, int, int)"/> method
        /// </summary>
        public DelegateCommand<object> AddTileCommand
        {
            get => new DelegateCommand<object>(AddTileCommand_Execute, AddTileCommand_CanExecute);//_AddTileCommand;
        }

        public RoomBlueprintViewModel()
        {
            InitializeCommands();
        }

        public void InitializeCommands()
        {
        }

        private bool ZoomCommand_CanExecute(object obj)
        {
            double amount = double.Parse(obj.ToString());

            if (ZoomX >= 2.0f || ZoomY >= 2.0f)
            {
                if (amount < 0)
                    return false;
            }
            if (ZoomX < 0.51f || ZoomY < 0.51f)
            {
                if (amount > 0)
                    return false;
            }
            return true;
        }

        private void ZoomCommand_Execute(object obj)
        {
            double amount = double.Parse(obj.ToString());
            Zoom(amount, amount);
        }


        /// <summary>
        /// Adjust the current <see cref="ZoomX"/> and <see cref="ZoomY"/> by the specified amounts which will, in turn, scale the corresponding <see cref="Views.RoomBlueprintEditorView"/>
        /// </summary>
        /// <param name="amountX">Amount by which to increase or decrease current horizontal zoom factor</param>
        /// <param name="amountY">Amount by which to increase or decrease current vertical zoom factor</param>
        public void Zoom(double amountX, double amountY)
        {
            ZoomX -= amountX;
            ZoomY -= amountY;
        }

        private bool AddTileCommand_CanExecute(object obj)
        {
            return true;
        }

        private void AddTileCommand_Execute(object obj)
        {
            var pos = Mouse.GetPosition(obj as IInputElement);

            int roomGridX = (int)(pos.X / ParentLevel.LevelProperties.TileWidth);
            int roomGridY = (int)(pos.Y / ParentLevel.LevelProperties.TileHeight);

            int tileId = PaletteViewModel.GetTileBrush().TileId;

            AddTile(tileId, roomGridX, roomGridY);
        }

        /// <summary>
        /// Adds a new <see cref="ITile"/> to the <see cref="=Tiles"/> collection for this <see cref="RoomBlueprintViewModel"/>
        /// </summary>
        /// <param name="tileId">The Type Id of the new tile that will be added</param>
        /// <param name="roomGridX">The X-Axis placement position of the new <see cref="ITile"/> in the <see cref="Tiles"/> grid</param>
        /// <param name="roomGridY">The Y-Axis placement position of the new <see cref="ITile"/> in the <see cref="Tiles"/> grid</param>
        public void AddTile(int tileId, int roomGridX, int roomGridY)
        {
            int paletteGridX = PaletteViewModel.GetTileBrush().PaletteGridX;
            int paletteGridY = PaletteViewModel.GetTileBrush().PaletteGridY;

            var existingTile = Tiles.Where(e => e.RoomGridX == roomGridX && e.RoomGridY == roomGridY).FirstOrDefault();

            if (existingTile != null)
            {
                Tiles.Remove(existingTile);
            }
            Tiles.Add(new TileViewModel(paletteGridX, paletteGridY)
            {
                TileWidth = ParentLevel.LevelProperties.TileWidth,
                TileHeight = ParentLevel.LevelProperties.TileHeight,
                RoomGridX = roomGridX,
                RoomGridY = roomGridY,
            });
        }
    }
}
