using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownLevelEditor.ViewModels;

namespace TopDownLevelEditor.Interfaces
{
    public interface ILevelBlueprintProperties : ISerializableBlueprint
    {
        //double ActualHeight { get; }
        //double ActualWidth { get; }
        //double BackgroundHeight { get; }
        //double BackgroundWidth { get; }
        string RoomBackgroundImageSource { get; set; }
        Point2 RoomSize { get; set; }
        //int RoomHeight { get; set; }
        //int RoomWidth { get; set; }
        //Rect TileGridViewport { get; set; }
        double TileHeight { get; set; }
        double TileWidth { get; set; }
    }
}
