using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace System.Runtime.Serialization
{
    public static class SerializationInfoExtensions
    {
        public static T GetValue<T>(this SerializationInfo info, string name)
        {
            return (T)info.GetValue(name, typeof(T));
        }

        public static void AddImageValue(this SerializationInfo info, string imageSource, StreamingContext context)
        {
            //Load image at specified path, cache in memory, spool from memory into serializer
        }

        private static string GetImageValue(this SerializationInfo info, string name)
        {
            string filepath = "GetImageValue() Not Implemented";
            //Deserialize the named value for image data, cache into a temp file, return filepath
            return filepath;
        }
    }
}
