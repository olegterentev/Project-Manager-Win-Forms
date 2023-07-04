using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Model
{
    internal static class Helpers
    {
        public static void SerializeFile(string fileName, object toSave)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, toSave);
                stream.Close();
            }
        }

        public static bool DeserializeFile(string fileName, out object obj)
        {
            obj = null;
            if (File.Exists(fileName))
            {
                IFormatter formatter = new BinaryFormatter();
                using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    obj = formatter.Deserialize(stream);
                }
            }
            return obj != null;
        }
    }
}
