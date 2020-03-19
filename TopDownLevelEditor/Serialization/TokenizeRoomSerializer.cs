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
            {'R', 64 },
            {'C', 4 },
            {'B', 5 },
            {'K', 6 },
            {'H', 36 },
        };

        private static string HeaderYaml = "%YAML 1.1";
        private static string HeaderTag = "%TAG !u! tag:unity3d.com,2011:";
        private static string HeaderSomething = "--- !u!114 &11400000";
        private static string BehaviorDeclaration = "MonoBehaviour:";

        //m_ObjectHideFlags: 0
        private int _ObjectHideFlags = 0;
        public int m_ObjectHideFlags => _ObjectHideFlags;
        public string ObjectHideFlagsString { get => $"{nameof(m_ObjectHideFlags)}: {_ObjectHideFlags}"; }

        //m_CorrespondingSourceObject: {fileID: 0}
        private static string CorrespondingSourceObjectItemHeader = "m_CorrespondingSourceObject: ";
        private int _fileID = 0;
        public int fileID => _fileID;
        public string fileIDString { get => "{" + $"{nameof(fileID)}: {fileID}" + "}"; }
        //m_PrefabInstance: {fileID: 0}
        private static string PrefabInstanceItemHeader = "m_PrefabInstance: ";
        //m_PrefabAsset: {fileID: 0}
        private static string PrefabAssetItemHeader = "m_PrefabAsset: ";
        //m_GameObject: {fileID: 0}
        private static string GameObjectItemHeader = "m_GameObject: ";
        //m_Enabled: 1
        private int _Enabled = 1;
        public int m_Enabled => _Enabled;
        public string EnabledString { get => $"{nameof(m_Enabled)}: {m_Enabled}"; }
        //m_EditorHideFlags: 0
        private int _EditorHideFlags = 0;
        public int m_EditorHideFlags => _EditorHideFlags;
        public string EditorHideFlagsString { get => $"{nameof(m_EditorHideFlags)}: {m_EditorHideFlags}"; }
        //m_Script: {fileID: 11500000, guid: f27c81a9560807845a9d35d0bd322629, type: 3}
        private static string ScriptItemHeader = "m_Script: ";
        private int _ScriptFileID = 11500000;
        public int m_ScriptFileID => _ScriptFileID;
        public string ScriptFileIDString { get => $"{nameof(fileID)}: {_ScriptFileID}"; }

        private string _guid = "f27c81a9560807845a9d35d0bd322629";
        public string guid => _guid;
        public string guidString { get => $"{nameof(guid)}: {_guid}"; }

        private int _ScriptType = 3;
        public int type => _ScriptType;
        public string typeString { get => $"{nameof(type)}: {type}"; }

        //m_Name: RoomBlueprint 1
        private string _Name = "Blueprint 1";
        public string m_Name
        {
            get => _Name;
            set { _Name = value; }
        }

        public string NameString { get => $"{nameof(m_Name)}: {m_Name}"; }

        //m_EditorClassIdentifier: 
        private string _EditorClassIdentifier = "";
        public string m_EditorClassIdentifier => _EditorClassIdentifier;
        public string EditorClassIdentifierString { get => $"{nameof(m_EditorClassIdentifier)}: {_EditorClassIdentifier}"; }

        //BlueprintId: 0
        private int _BlueprintId = 0;
        public int BlueprintId
        {
            get => _BlueprintId;
            set { _BlueprintId = value; }
        }

        public string BlueprintIdString { get => $"{nameof(BlueprintId)}: {_BlueprintId}"; }

        //RoomShapeId: 0
        private int _RoomShapeId = 0;
        public int RoomShapeId => _RoomShapeId;
        public string RoomShapeIdString { get => $"{nameof(RoomShapeId)}: {_RoomShapeId}"; }

        //RawData:
        //- FP---------PF
        //- P-----X-----P
        //- ------B------
        //- ----R---R----
        //- ------B------
        //- R---R---R---R
        //- FR---------RF
        private static string RawDataItemHeader = "RawData:";

        private List<string> _RawData = new List<string>();
        public List<string> RawData
        {
            get => _RawData;
            set { _RawData = value; }
        }
        public string RawDataString
        {
            get
            {
                StringBuilder s = new StringBuilder($"{nameof(RawData)}:");

                RawData.ForEach(e => s.Append($"{Environment.NewLine}- {e}"));

                return s.ToString();
            }
        }

        public List<string> GenerateRawDataString(RoomBlueprintViewModel room)
        {
            var roomData = new List<string>();

            //Iterate through tiles, convert to characters, new string for each row
            for (int y = 1; y <= room.ParentLevel.LevelProperties.RoomSize.Y; y++)
            {
                StringBuilder row = new StringBuilder();
                for (int x = 1; x <= room.ParentLevel.LevelProperties.RoomSize.X; x++)
                {
                    var tile = room.Tiles.Where(e => e.RoomGridX == x && e.RoomGridY == y).FirstOrDefault();
                    if (tile == null)
                    {
                        row.Append("-");
                    }
                    else
                    {
                        row.Append($"{TokenToTileIdMap.Where(e => e.Value == tile.Id).First().Key}");
                    }
                }
                roomData.Add(row.ToString());
            }

            return roomData;
        }

        public List<string> GenerateFileLines()
        {
            List<string> lines = new List<string>();

            //%YAML 1.1
            lines.Add(HeaderYaml);
            //%TAG !u! tag:unity3d.com,2011:
            lines.Add(HeaderTag);
            //--- !u!114 &11400000
            lines.Add(HeaderSomething);
            //MonoBehaviour:
            lines.Add(BehaviorDeclaration);
            //  m_ObjectHideFlags: 0
            lines.Add(ObjectHideFlagsString);
            //  m_CorrespondingSourceObject: {fileID: 0}
            lines.Add($"{CorrespondingSourceObjectItemHeader}{fileIDString}");
            //  m_PrefabInstance: {fileID: 0}
            lines.Add($"{PrefabInstanceItemHeader}{fileIDString}");
            //  m_PrefabAsset: {fileID: 0}
            lines.Add($"{PrefabAssetItemHeader}{fileIDString}");
            //  m_GameObject: {fileID: 0}
            lines.Add($"{GameObjectItemHeader}{fileIDString}");
            //  m_Enabled: 1
            lines.Add(EnabledString);
            //  m_EditorHideFlags: 0
            lines.Add(EditorHideFlagsString);
            //  m_Script: {fileID: 11500000, guid: f27c81a9560807845a9d35d0bd322629, type: 3}
            lines.Add(ScriptItemHeader + "{" + $"{ScriptFileIDString}, {guidString}, {typeString}" + "}");
            //  m_Name: RoomBlueprint 1
            lines.Add($"{NameString}");
            //  m_EditorClassIdentifier:
            lines.Add($"{EditorClassIdentifierString}");
            //  BlueprintId: 0
            lines.Add($"{BlueprintIdString}");
            //  RoomShapeId: 0
            lines.Add($"{RoomShapeIdString}");
            //  RawData:
            lines.Add($"{RawDataString}");
            //  - FP---------PF
            //  - P-----X-----P
            //  - ------B------
            //  - ----R---R----
            //  - ------B------
            //  - R---R---R---R
            //  - FR---------RF
            return lines;
        }

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
