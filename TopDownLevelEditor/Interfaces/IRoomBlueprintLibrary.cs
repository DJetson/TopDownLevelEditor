using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TopDownLevelEditor.ViewModels;

namespace TopDownLevelEditor.Interfaces
{
    public interface IRoomBlueprintLibrary /*: ISerializable*/
    {
        ILevelBlueprint ParentLevel { get; set; }
        ObservableCollection<RoomBlueprintViewModel> BlueprintItems { get; set; }
        void AddNewRoomBlueprint();
    }
}
