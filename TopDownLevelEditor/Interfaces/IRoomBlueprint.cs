using Prism.Commands;
using System.Collections.ObjectModel;

namespace TopDownLevelEditor.Interfaces
{
    public interface IRoomBlueprint /* : ISerializableBlueprint*/
    {
        ILevelBlueprint ParentLevel { get; set; }
        DelegateCommand<object> AddTileCommand { get; /*set; */}
        ObservableCollection<ITile> Tiles { get; set; }
    }
}