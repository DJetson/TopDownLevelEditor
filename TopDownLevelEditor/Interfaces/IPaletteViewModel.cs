using System.Collections.ObjectModel;

namespace TopDownLevelEditor.Interfaces
{
    public interface IPaletteViewModel
    {
        string ImageSource { get; set; }
        ObservableCollection<ITileBrush> TileBrushItems { get; set; }
    }
}