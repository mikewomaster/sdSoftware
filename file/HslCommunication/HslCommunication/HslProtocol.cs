namespace HslCommunication
{
    using HslCommunication.BasicFramework;
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class HslProtocol
    {
        internal const int HeadByteLength = 0x20;
        internal const int ProtocolAccountLogin = 5;
        internal const int ProtocolAccountRejectLogin = 6;
        internal const int ProtocolBufferSize = 0x400;
        internal const int ProtocolCheckSecends = 1;
        internal const int ProtocolClientAllowLogin = 4;
        internal const int ProtocolClientQuit = 2;
        internal const int ProtocolClientRefuseLogin = 3;
        internal const int ProtocolErrorMsg = 0x7da;
        internal const int ProtocolFileCheckError = 0x7d5;
        internal const int ProtocolFileCheckRight = 0x7d4;
        internal const int ProtocolFileDelete = 0x7d3;
        internal const int ProtocolFileDirectories = 0x7d8;
        internal const int ProtocolFileDirectoryFiles = 0x7d7;
        internal const int ProtocolFileDownload = 0x7d1;
        internal const int ProtocolFileExists = 0x7dd;
        internal const int ProtocolFileSaveError = 0x7d6;
        internal const int ProtocolFilesDelete = 0x7db;
        internal const int ProtocolFileUpload = 0x7d2;
        internal const int ProtocolFolderDelete = 0x7dc;
        internal const int ProtocolNoZipped = 0xbb9;
        internal const int ProtocolProgressReport = 0x7d9;
        internal const int ProtocolUserBitmap = 0x3eb;
        internal const int ProtocolUserBytes = 0x3ea;
        internal const int ProtocolUserException = 0x3ec;
        internal const int ProtocolUserString = 0x3e9;
        internal const int ProtocolUserStringArray = 0x3ed;
        internal const int ProtocolZipped = 0xbba;

        internal static byte[] CommandAnalysis(byte[] head, byte[] content)
        {
            if (content > null)
            {
                if (BitConverter.ToInt32(head, 8) == 0xbba)
                {
                    content = SoftZipped.Decompress(content);
                }
                return HslSecurity.ByteDecrypt(content);
            }
            return null;
        }

        internal static byte[] CommandBytes(int customer, Guid token, byte[] data)
        {
            return CommandBytes(0x3ea, customer, token, data);
        }

        internal static byte[] CommandBytes(int customer, Guid token, string data)
        {
            if (data == null)
            {
                return CommandBytes(0x3e9, customer, token, null);
            }
            return CommandBytes(0x3e9, customer, token, Encoding.Unicode.GetBytes(data));
        }

        internal static byte[] CommandBytes(int customer, Guid token, string[] data)
        {
            return CommandBytes(0x3ed, customer, token, PackStringArrayToByte(data));
        }

        internal static byte[] CommandBytes(int command, int customer, Guid token, byte[] data)
        {
            int num = 0xbb9;
            int num2 = (data == null) ? 0 : data.Length;
            byte[] array = new byte[0x20 + num2];
            BitConverter.GetBytes(command).CopyTo(array, 0);
            BitConverter.GetBytes(customer).CopyTo(array, 4);
            BitConverter.GetBytes(num).CopyTo(array, 8);
            token.ToByteArray().CopyTo(array, 12);
            if (num2 > 0)
            {
                BitConverter.GetBytes(num2).CopyTo(array, 0x1c);
                Array.Copy(data, 0, array, 0x20, num2);
                HslSecurity.ByteEncrypt(array, 0x20, num2);
            }
            return array;
        }

        public static OperateResult<NetHandle, byte[]> ExtractHslData(byte[] content)
        {
            if (content.Length == 0)
            {
                return OperateResult.CreateSuccessResult<NetHandle, byte[]>(0, new byte[0]);
            }
            byte[] destinationArray = new byte[0x20];
            byte[] buffer2 = new byte[content.Length - 0x20];
            Array.Copy(content, 0, destinationArray, 0, 0x20);
            if (buffer2.Length > 0)
            {
                Array.Copy(content, 0x20, buffer2, 0, content.Length - 0x20);
            }
            if (BitConverter.ToInt32(destinationArray, 0) == 0x7da)
            {
                return new OperateResult<NetHandle, byte[]>(Encoding.Unicode.GetString(buffer2));
            }
            int num = BitConverter.ToInt32(destinationArray, 0);
            int num2 = BitConverter.ToInt32(destinationArray, 4);
            buffer2 = CommandAnalysis(destinationArray, buffer2);
            if (num == 6)
            {
                return new OperateResult<NetHandle, byte[]>(Encoding.Unicode.GetString(buffer2));
            }
            return OperateResult.CreateSuccessResult<NetHandle, byte[]>(num2, buffer2);
        }

        internal static byte[] PackStringArrayToByte(string data)
        {
            string[] textArray1 = new string[] { data };
            return PackStringArrayToByte(textArray1);
        }

        internal static byte[] PackStringArrayToByte(string[] data)
        {
            if (data == null)
            {
                data = new string[0];
            }
            List<byte> list = new List<byte>();
            list.AddRange(BitConverter.GetBytes(data.Length));
            for (int i = 0; i < data.Length; i++)
            {
                if (!string.IsNullOrEmpty(data[i]))
                {
                    byte[] bytes = Encoding.Unicode.GetBytes(data[i]);
                    list.AddRange(BitConverter.GetBytes(bytes.Length));
                    list.AddRange(bytes);
                }
                else
                {
                    list.AddRange(BitConverter.GetBytes(0));
                }
            }
            return list.ToArray();
        }

        internal static string[] UnPackStringArrayFromByte(byte[] content)
        {
            if ((content != null) ? (content.Length < 4) : false)
            {
                return null;
            }
            int startIndex = 0;
            int num2 = BitConverter.ToInt32(content, startIndex);
            string[] strArray = new string[num2];
            startIndex += 4;
            for (int i = 0; i < num2; i++)
            {
                int count = BitConverter.ToInt32(content, startIndex);
                startIndex += 4;
                if (count > 0)
                {
                    strArray[i] = Encoding.Unicode.GetString(content, startIndex, count);
                }
                else
                {
                    strArray[i] = string.Empty;
                }
                startIndex += count;
            }
            return strArray;
        }
    }
}

