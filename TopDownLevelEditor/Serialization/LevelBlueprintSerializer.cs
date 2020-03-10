using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownLevelEditor.Interfaces;

namespace TopDownLevelEditor.Serialization
{
    //public class LevelBlueprintSerializer : IBlueprintSerializer<string>
    //{
    //    public ISerializableBlueprint DeserializeBlueprint(List<string> data)
    //    {
    //        foreach (var roomFile in data)
    //        {
    //            //var levelBlueprintFileData = File.ReadAllLines(data[0]);

    //            var fileData = GetRawDataFromAssetFile(roomFile);

    //            if (Path.GetExtension(roomFile) == ".asset")
    //                fileData = GetRawDataFromAssetFile(roomFile);
    //        }
    //        return null;
    //    }

    //    public List<string> SerializeBlueprint(ISerializableBlueprint blueprint)
    //    {
    //        var levelBlueprint = blueprint as ILevelBlueprint;

    //        var properties = SerializeLevelProperties();

    //        return null;
    //    }

    //    private object SerializeLevelProperties()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private List<string> GetRawDataFromAssetFile(string data)
    //    {
    //        var assetFileData = File.ReadAllLines(data);
    //        var RawData = new List<string>();
    //        bool foundRawData = false;
    //        foreach (var line in assetFileData)
    //        {
    //            if (foundRawData)
    //            {
    //                RawData.Add(line.Substring(4));
    //            }
    //            else if (line.Contains("RawData"))
    //            {
    //                foundRawData = true;
    //            }
    //        }
    //        return RawData;
    //    }
    //}
}
