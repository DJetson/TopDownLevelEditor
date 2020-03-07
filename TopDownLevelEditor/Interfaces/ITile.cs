using System.Windows;

namespace TopDownLevelEditor.Interfaces
{
    public enum TileType { Terrain = 0, Pickup = 1, Item = 2, }
    public interface ITile
    {
        int Id { get; set; }
        double DrawPositionX { get; set; }
        double DrawPositionY { get; set; }
        int TilePaletteX { get; set; }
        int TilePaletteY { get; set; }
        double TileWidth { get; set; }
        Rect TileViewBox { get; set; }
        double TileHeight { get; set; }
        int TileUnitX { get; set; }
        int TileUnitY { get; set; }
    }
}