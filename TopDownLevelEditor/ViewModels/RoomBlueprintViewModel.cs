﻿using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
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
        /// <see cref="DelegateCommand"/> that can be used by a <see cref="ICommandSource"/> in the View Layer to trigger execution of the <see cref="RoomBlueprintViewModel.Zoom(double, double)"/> method
        /// </summary>
        public DelegateCommand ZoomCommand
        {
            get => new DelegateCommand(ZoomCommand_Execute, ZoomCommand_CanExecute);//_ZoomCommand;
            //set { _ZoomCommand = value; NotifyPropertyChanged(); }
        }
        //[NonSerialized]
        //private DelegateCommand _ZoomCommand;

        /// <summary>
        /// <see cref="DelegateCommand"/> that can be used by a <see cref="ICommandSource"/> in the View Layer to trigger execution of the <see cref="RoomBlueprintViewModel.AddTile(int, int, int)"/> method
        /// </summary>
        public DelegateCommand AddTileCommand
        {
            get => new DelegateCommand(AddTileCommand_Execute, AddTileCommand_CanExecute);//_AddTileCommand;
            //set { _AddTileCommand = value; NotifyPropertyChanged(); }
        }
        //[NonSerialized]
        //private DelegateCommand _AddTileCommand;

        public RoomBlueprintViewModel()
        {
            InitializeCommands();
        }

        public void InitializeCommands()
        {
            //AddTileCommand = new DelegateCommand(AddTileCommand_Execute, AddTileCommand_CanExecute);
            //ZoomCommand = new DelegateCommand(ZoomCommand_Execute, ZoomCommand_CanExecute);
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
            //throw new NotImplementedException();
            return true;
        }

        private void AddTileCommand_Execute(object obj)
        {
            var pos = Mouse.GetPosition(obj as IInputElement);

            int roomGridX = (int)(pos.X / ParentLevel.LevelProperties.TileWidth);
            int roomGridY = (int)(pos.Y / ParentLevel.LevelProperties.TileHeight);

            int tileId = PaletteViewModel.GetTileBrush().TileId;

            AddTile(tileId, roomGridX, roomGridY);

            //int paletteGridX = PaletteViewModel.GetTileBrush().TilePaletteX;
            //int paletteGridY = PaletteViewModel.GetTileBrush().TilePaletteY;

            //var existingTile = Tiles.Where(e => e.RoomGridX == roomGridX && e.RoomGridY == roomGridY).FirstOrDefault();

            //if (existingTile != null)
            //{
            //    Tiles.Remove(existingTile);
            //}
            //Tiles.Add(new TileViewModel(tileId, paletteGridX, paletteGridY)
            //{
            //    TileWidth = ParentLevel.LevelProperties.TileWidth,
            //    TileHeight = ParentLevel.LevelProperties.TileHeight,
            //    RoomGridX = roomGridX,
            //    RoomGridY = roomGridY,
            //    RoomDrawPositionX = roomGridX * ParentLevel.LevelProperties.TileWidth,
            //    RoomDrawPositionY = roomGridY * ParentLevel.LevelProperties.TileHeight
            //});
        }

        /// <summary>
        /// Adds a new <see cref="ITile"/> to the <see cref="=Tiles"/> collection for this <see cref="RoomBlueprintViewModel"/>
        /// </summary>
        /// <param name="tileId">The Type Id of the new tile that will be added</param>
        /// <param name="roomGridX">The X-Axis placement position of the new <see cref="ITile"/> in the <see cref="Tiles"/> grid</param>
        /// <param name="roomGridY">The Y-Axis placement position of the new <see cref="ITile"/> in the <see cref="Tiles"/> grid</param>
        public void AddTile(int tileId, int roomGridX, int roomGridY)
        {
            //var pos = Mouse.GetPosition(obj as IInputElement);

            //int roomGridX = (int)(pos.X / ParentLevel.LevelProperties.TileWidth);
            //int roomGridY = (int)(pos.Y / ParentLevel.LevelProperties.TileHeight);

            //int tileId = PaletteViewModel.GetTileBrush().TileId;
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
                //RoomDrawPositionX = roomGridX * ParentLevel.LevelProperties.TileWidth,
                //RoomDrawPositionY = roomGridY * ParentLevel.LevelProperties.TileHeight
            });
        }

        //public RoomBlueprintViewModel(SerializationInfo info, StreamingContext context)
        //{
        //    Tiles = info.GetValue<ObservableCollection<ITile>>(nameof(Tiles));
        //    InitializeCommands();
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue(nameof(Tiles), _Tiles);
        //    //SerializeTiles(info, context);
        //}

        //private void SerializeTiles(SerializationInfo info, StreamingContext context)
        //{
        //    foreach (var tile in Tiles)
        //    {
        //        tile.GetObjectData(info, context);
        //    }
        //}
    }
}