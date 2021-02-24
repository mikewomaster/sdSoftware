namespace HslCommunication
{
    using System;

    internal class HslSecurity
    {
        internal static byte[] ByteDecrypt(byte[] deBytes)
        {
            return ByteEncrypt(deBytes);
        }

        internal static byte[] ByteEncrypt(byte[] enBytes)
        {
            if (enBytes == null)
            {
                return null;
            }
            byte[] buffer = new byte[enBytes.Length];
            for (int i = 0; i < enBytes.Length; i++)
            {
                buffer[i] = (byte) (enBytes[i] ^ 0xb5);
            }
            return buffer;
        }

        internal static void ByteEncrypt(byte[] enBytes, int offset, int count)
        {
            for (int i = offset; i < (offset + count); i++)
            {
                if (i >= enBytes.Length)
                {
                    break;
                }
                enBytes[i] = (byte) (enBytes[i] ^ 0xb5);
            }
        }
    }
}

