namespace HslCommunication.BasicFramework
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public static class SoftSecurity
    {
        internal static string MD5Decrypt(string pToDecrypt)
        {
            return MD5Decrypt(pToDecrypt, "zxcvBNMM");
        }

        public static string MD5Decrypt(string pToDecrypt, string password)
        {
            if (pToDecrypt == "")
            {
                return pToDecrypt;
            }
            string s = password;
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] buffer = new byte[pToDecrypt.Length / 2];
            for (int i = 0; i < (pToDecrypt.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte) num2;
            }
            provider.Key = Encoding.ASCII.GetBytes(s);
            provider.IV = Encoding.ASCII.GetBytes(s);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            stream2.Dispose();
            return Encoding.Default.GetString(stream.ToArray());
        }

        internal static string MD5Encrypt(string pToEncrypt)
        {
            return MD5Encrypt(pToEncrypt, "zxcvBNMM");
        }

        public static string MD5Encrypt(string pToEncrypt, string Password)
        {
            string s = Password;
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(pToEncrypt);
            provider.Key = Encoding.ASCII.GetBytes(s);
            provider.IV = Encoding.ASCII.GetBytes(s);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num2 in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num2);
            }
            builder.ToString();
            return builder.ToString();
        }
    }
}

