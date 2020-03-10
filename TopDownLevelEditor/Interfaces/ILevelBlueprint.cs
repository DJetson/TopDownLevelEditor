using TopDownLevelEditor.ViewModels;

namespace TopDownLevelEditor.Interfaces
{
    public interface ILevelBlueprint : ISerializableBlueprint
    {
        IRoomBlueprintLibrary BlueprintLibrary { get; set; }
        LevelBlueprintPropertiesViewModel LevelProperties { get; set; }
    }
}