using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownLevelEditor.Interfaces;
using TopDownLevelEditor.ViewModels;

namespace TopDownLevelEditor.Serialization
{
    public class TokenizeRoomSerializer : IBlueprintSerializer<string>
    {
        private static readonly Dictionary<char, int> TokenToTileIdMap = new Dictionary<char, int>()
        {
            {'P', 0 },
            {'F', 32 },
            {'X', 64 },
            {'C', 4 },
            {'B', 5 },
            {'K', 6 },
            {'H', 36 },
        };

        public ISerializableBlueprint DeserializeBlueprint(List<string> data)
        {
            var fileData = data[0];

            //if (Path.GetExtension(data[0]) == ".asset")
            //    fileData = GetRawDataFromAssetFile(data);

            //var roomBlueprint = new RoomBlueprintViewModel();

            //for(int y = 0; y < fileData.Count; y++)
            //{
            //    for(int x = 0; x < fileData[y].Length; x++)
            //    {
            //        roomBlueprint.AddTile(TokenToTileIdMap[fileData[y][x]], x, y);
            //    }
            //}

            return null;
        }

        

        public List<string> SerializeBlueprint(ISerializableBlueprint blueprint)
        {
            throw new NotImplementedException();
        }
    }
}
