using System.Runtime.Serialization.Formatters.Binary;

namespace TesteVericode.Services.Helpers
{
    public static class Utils
    {
        public static byte[] ObjectToByteArray(this Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();

            }
        }
        public static T ByteArrayToObject<T>(this byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                return (T)binaryFormatter.Deserialize(memStream);
            }
        }
    }
}
