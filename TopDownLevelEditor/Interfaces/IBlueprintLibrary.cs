using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownLevelEditor.Interfaces
{
    public interface IBlueprintLibrary
    {
        ObservableCollection<IRoomBlueprint> BlueprintItems { get; set; }
        void AddNewRoomBlueprint();
    }
}
