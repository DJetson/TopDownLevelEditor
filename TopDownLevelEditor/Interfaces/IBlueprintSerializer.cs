using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownLevelEditor.Interfaces
{
    public interface IBlueprintSerializer<T>
    {
        List<T> SerializeBlueprint(ISerializableBlueprint blueprint);
        ISerializableBlueprint DeserializeBlueprint(List<T> data);
    }
}
