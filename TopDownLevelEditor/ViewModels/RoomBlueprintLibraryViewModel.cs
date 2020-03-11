using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.ViewModels
{
    [Serializable]
    public class RoomBlueprintLibraryViewModel : NotifyBase, IRoomBlueprintLibrary
    {
        private ILevelBlueprint _ParentLevel;
        public ILevelBlueprint ParentLevel
        {
            get => _ParentLevel;
            set { _ParentLevel = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<RoomBlueprintViewModel> _BlueprintItems = new ObservableCollection<RoomBlueprintViewModel>();
        public ObservableCollection<RoomBlueprintViewModel> BlueprintItems
        {
            get => _BlueprintItems;
            set { _BlueprintItems = value; NotifyPropertyChanged(); }
        }

        private IRoomBlueprint _SelectedBlueprint;
        public IRoomBlueprint SelectedBlueprint
        {
            get => _SelectedBlueprint;
            set { _SelectedBlueprint = value; NotifyPropertyChanged(); }
        }

        public DelegateCommand<object> AddBlueprintCommand
        {
            get => new DelegateCommand<object>(AddBlueprint_Execute); //_AddBlueprintCommand;
        }

        public DelegateCommand<object> RemoveBlueprintCommand
        {
            get => new DelegateCommand<object>(RemoveBlueprint_Execute, RemoveBlueprint_CanExecute);//_RemoveBlueprintCommand;
        }

        public void InitializeCommands()
        {
        }

        public RoomBlueprintLibraryViewModel(LevelBlueprintViewModel parent)
        {
            ParentLevel = parent;
            AddNewRoomBlueprint();
            InitializeCommands();
        }

        public RoomBlueprintLibraryViewModel()
        {
            InitializeCommands();
        }

        private void RemoveBlueprint_Execute(object obj)
        {
            var selected = obj as RoomBlueprintViewModel;

            RemoveRoomBlueprint(selected);
        }

        private bool RemoveBlueprint_CanExecute(object obj)
        {
            if (!(obj is IRoomBlueprint selected))
                return false;

            return true;
        }

        private void AddBlueprint_Execute(object obj)
        {
            AddNewRoomBlueprint();
        }

        public void AddNewRoomBlueprint()
        {
            var newBlueprint = new RoomBlueprintViewModel() { ParentLevel = ParentLevel };
            BlueprintItems.Add(newBlueprint);
            SelectedBlueprint = newBlueprint;
        }

        public void RemoveRoomBlueprint(RoomBlueprintViewModel selected)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Delete the selected blueprint?", "Remove blueprint", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No))
            {
                int removedIndex = BlueprintItems.IndexOf(selected);
                BlueprintItems.Remove(selected);
                if (BlueprintItems.Count == 0)
                {
                    AddNewRoomBlueprint();
                }
                else
                {
                    if (removedIndex < BlueprintItems.Count)
                    {
                        SelectedBlueprint = BlueprintItems[removedIndex];
                    }
                    else
                    {
                        SelectedBlueprint = BlueprintItems[BlueprintItems.Count - 1];
                    }
                }
            }
        }
    }
}
