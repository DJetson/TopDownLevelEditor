using TopDownLevelEditor.ViewModels;

namespace TopDownLevelEditor.Interfaces
{
    public interface ITileBrush
    {
        LevelPaletteViewModel PaletteViewModel { get; set; }
        bool IsValidBrush { get; set; }
        int TileId { get; }
        int PaletteGridX { get; set; }
        int PaletteGridY { get; set; }
        //ITile TileViewModel { get; set; }
    }
}