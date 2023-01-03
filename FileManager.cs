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
            if (!File.Exists(fileName))
            {
                // File does not exist, create a new one
                return default(T);
            }

            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                if (stream.Length == 0)
                {
                    // Stream is empty, return default value for type T
                    return default(T);
                }

                BinaryFormatter formatter = new BinaryFormatter();
                T obj = (T)formatter.Deserialize(stream);
                return obj;
            }
        }

    }
}
