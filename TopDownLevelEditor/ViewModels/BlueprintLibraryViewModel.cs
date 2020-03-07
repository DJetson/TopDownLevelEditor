using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    public class BlueprintLibraryViewModel : NotifyBase, IBlueprintLibrary
    {
        private LevelViewModel _ParentLevel;
        public LevelViewModel ParentLevel
        {
            get => _ParentLevel;
            set { _ParentLevel = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<IRoomBlueprint> _BlueprintItems = new ObservableCollection<IRoomBlueprint>();
        public ObservableCollection<IRoomBlueprint> BlueprintItems
        {
            get => _BlueprintItems;
            set { _BlueprintItems = value; NotifyPropertyChanged(); }
        }

        public void AddNewRoomBlueprint()
        {
            BlueprintItems.Add(new RoomBlueprintViewModel() { ParentLevel = ParentLevel });
        }
    }
}
