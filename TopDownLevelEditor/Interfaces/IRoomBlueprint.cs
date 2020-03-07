using Microsoft.VisualStudio.PlatformUI;
using System.Collections.ObjectModel;
using TopDownLevelEditor.ViewModels;

namespace TopDownLevelEditor.Interfaces
{
    public interface IRoomBlueprint
    {
        LevelViewModel ParentLevel { get; set; }
        DelegateCommand AddTileCommand { get; set; }
        ObservableCollection<ITile> Tiles { get; set; }
    }
}