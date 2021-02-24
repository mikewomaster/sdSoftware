namespace HslCommunication.BasicFramework
{
    using System;
    using System.IO;
    using System.IO.Compression;

    public class SoftZipped
    {
        public static byte[] CompressBytes(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress))
                {
                    stream2.Write(bytes, 0, bytes.Length);
                }
                return stream.ToArray();
            }
        }

        public static byte[] Decompress(byte[] bytes)
        {
            byte[] buffer2;
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress))
                {
                    using (MemoryStream stream3 = new MemoryStream())
                    {
                        int count = 0x400;
                        byte[] buffer = new byte[count];
                        int num2 = 0;
                        while ((num2 = stream2.Read(buffer, 0, count)) > 0)
                        {
                            stream3.Write(buffer, 0, num2);
                        }
                        buffer2 = stream3.ToArray();
                    }
                }
            }
            return buffer2;
        }
    }
}

