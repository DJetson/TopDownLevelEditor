using System.Runtime.Serialization;
using System.Windows;

namespace TopDownLevelEditor.Interfaces
{
    public enum TileType { Terrain = 0, Pickup = 1, Item = 2, }
    public interface ITile /*: ISerializable*/
    {
        int Id { get; /*set;*/ }
        double RoomDrawPositionX { get; /*set;*/ }
        double RoomDrawPositionY { get; /*set;*/ }
        int PaletteGridX { get; set; }
        int PaletteGridY { get; set; }
        double TileWidth { get; set; }
        double TileHeight { get; set; }
        Rect TileViewBox { get; /*set;*/ }
        int RoomGridX { get; set; }
        int RoomGridY { get; set; }
    }
}