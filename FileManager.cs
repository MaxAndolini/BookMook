

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BookMook
{
    [Serializable]
    public class FileManager
    {
        public static void Write<T>(string fileName, T objectToSerialize)
        {
            using (Stream stream = File.Open(fileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, objectToSerialize);
            }
        }

        public static T Read<T>(string fileName)
        {
            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                T obj = (T)formatter.Deserialize(stream);
                return obj;
            }
        }
    }
}
