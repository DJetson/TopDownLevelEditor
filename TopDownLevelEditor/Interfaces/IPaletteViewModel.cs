using System.Collections.ObjectModel;

namespace TopDownLevelEditor.Interfaces
{
    public interface IPaletteViewModel
    {
        string TilePaletteImageSource { get; set; }
        ObservableCollection<ITileBrush> TileBrushItems { get; set; }
    }
}