using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
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

        //[NonSerialized]
        //private DelegateCommand _AddBlueprintCommand;
        public DelegateCommand AddBlueprintCommand
        {
            get => new DelegateCommand(AddBlueprint_Execute); //_AddBlueprintCommand;
            //set { _AddBlueprintCommand = value; NotifyPropertyChanged(); }
        }

        //[NonSerialized]
        //private DelegateCommand _RemoveBlueprintCommand;
        public DelegateCommand RemoveBlueprintCommand
        {
            get => new DelegateCommand(RemoveBlueprint_Execute, RemoveBlueprint_CanExecute);//_RemoveBlueprintCommand;
            //set { _RemoveBlueprintCommand = value; NotifyPropertyChanged(); }
        }

        public void InitializeCommands()
        {
            //AddBlueprintCommand = new DelegateCommand(AddBlueprint_Execute);
            //RemoveBlueprintCommand = new DelegateCommand(RemoveBlueprint_Execute, RemoveBlueprint_CanExecute);
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

        //public RoomBlueprintLibraryViewModel(SerializationInfo info, StreamingContext context)
        //{
        //    //ParentLevel = parent;

        //    BlueprintItems = new ObservableCollection<RoomBlueprintViewModel>();
        //    var itemCount = info.GetValue<int>(nameof(BlueprintItems));
        //    for (int i = 0; i < itemCount; i++)
        //    {
        //        BlueprintItems.Add(new RoomBlueprintViewModel(info, context) { ParentLevel = this.ParentLevel });
        //    }
        //    InitializeCommands();
        //}

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
            //if (BlueprintItems.Count() == 1)
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

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue(nameof(BlueprintItems), _BlueprintItems.Count);
        //    foreach(var room in BlueprintItems)
        //    {
        //        room.GetObjectData(info, context);
        //    }
        //}
    }
}
