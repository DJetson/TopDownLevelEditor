namespace TopDownLevelEditor.Interfaces
{
    public interface ITileBrush
    {
        int TileId { get; }
        int PaletteGridX { get; set; }
        int PaletteGridY { get; set; }
        //ITile TileViewModel { get; set; }
    }
}