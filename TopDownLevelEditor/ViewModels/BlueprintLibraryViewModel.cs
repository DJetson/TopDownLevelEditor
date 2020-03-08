using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private IRoomBlueprint _SelectedBlueprint;
        public IRoomBlueprint SelectedBlueprint
        {
            get => _SelectedBlueprint;
            set { _SelectedBlueprint = value; NotifyPropertyChanged(); }
        }

        private DelegateCommand _AddBlueprintCommand;
        public DelegateCommand AddBlueprintCommand
        {
            get => _AddBlueprintCommand;
            set { _AddBlueprintCommand = value; NotifyPropertyChanged(); }
        }

        private DelegateCommand _RemoveBlueprintCommand;
        public DelegateCommand RemoveBlueprintCommand
        {
            get => _RemoveBlueprintCommand;
            set { _RemoveBlueprintCommand = value; NotifyPropertyChanged(); }
        }

        public BlueprintLibraryViewModel(LevelViewModel parent)
        {
            ParentLevel = parent;
            AddNewRoomBlueprint();
            AddBlueprintCommand = new DelegateCommand(AddBlueprint_Execute);
            RemoveBlueprintCommand = new DelegateCommand(RemoveBlueprint_Execute, RemoveBlueprint_CanExecute);
        }

        private void RemoveBlueprint_Execute(object obj)
        {
            IRoomBlueprint selected = obj as IRoomBlueprint;

            RemoveRoomBlueprint(selected);
        }

        private bool RemoveBlueprint_CanExecute(object obj)
        {
            IRoomBlueprint selected = obj as IRoomBlueprint;

            if (selected == null)
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
            //if (BlueprintItems.Count() == 1)
            SelectedBlueprint = newBlueprint;
        }

        public void RemoveRoomBlueprint(IRoomBlueprint selected)
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
