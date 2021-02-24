namespace HslCommunication.Core
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class HslHelper
    {
        public static OperateResult<T> ByteArrayToStruct<T>(byte[] content) where T: struct
        {
            if (!HslCommunication.Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<T>(StringResources.Language.InsufficientPrivileges);
            }
            int cb = Marshal.SizeOf(typeof(T));
            IntPtr destination = Marshal.AllocHGlobal(cb);
            try
            {
                Marshal.Copy(content, 0, destination, cb);
                T local = (T) Marshal.PtrToStructure(destination, typeof(T));
                Marshal.FreeHGlobal(destination);
                return OperateResult.CreateSuccessResult<T>(local);
            }
            catch (Exception exception)
            {
                Marshal.FreeHGlobal(destination);
                return new OperateResult<T>(exception.Message);
            }
        }

        public static void CalculateStartBitIndexAndLength(int addressStart, ushort length, out int newStart, out ushort byteLength, out int offset)
        {
            byteLength = (ushort) (((((addressStart + length) - 1) / 8) - (addressStart / 8)) + 1);
            offset = addressStart % 8;
            newStart = addressStart - offset;
        }

        public static OperateResult<int> ExtractParameter(ref string address, string paraName)
        {
            try
            {
                Match match = Regex.Match(address, paraName + "=[0-9A-Fa-fx]+;");
                if (!match.Success)
                {
                    string[] textArray1 = new string[] { "Address [", address, "] can't find [", paraName, "] Parameters. for example : ", paraName, "=1;100" };
                    return new OperateResult<int>(string.Concat(textArray1));
                }
                string str = match.Value.Substring(paraName.Length + 1, (match.Value.Length - paraName.Length) - 2);
                int num = str.StartsWith("0x") ? Convert.ToInt32(str.Substring(2), 0x10) : (str.StartsWith("0") ? Convert.ToInt32(str, 8) : Convert.ToInt32(str));
                address = address.Replace(match.Value, "");
                return OperateResult.CreateSuccessResult<int>(num);
            }
            catch (Exception exception)
            {
                string[] textArray2 = new string[] { "Address [", address, "] Get [", paraName, "] Parameters failed: ", exception.Message };
                return new OperateResult<int>(string.Concat(textArray2));
            }
        }

        public static int ExtractParameter(ref string address, string paraName, int defaultValue)
        {
            OperateResult<int> result = ExtractParameter(ref address, paraName);
            return (result.IsSuccess ? result.Content : defaultValue);
        }

        public static int ExtractStartIndex(ref string address)
        {
            try
            {
                Match match = Regex.Match(address, @"\[[0-9]+\]$");
                if (!match.Success)
                {
                    return -1;
                }
                int num = Convert.ToInt32(match.Value.Substring(1, match.Value.Length - 2));
                address = address.Remove(address.Length - match.Value.Length);
                return num;
            }
            catch
            {
                return -1;
            }
        }

        public static IByteTransform ExtractTransformParameter(ref string address, IByteTransform defaultTransform)
        {
            IByteTransform transform;
            if (!HslCommunication.Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return defaultTransform;
            }
            try
            {
                string str = "format";
                Match match = Regex.Match(address, str + "=(ABCD|BADC|DCBA|CDAB);", RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return defaultTransform;
                }
                string str2 = match.Value.Substring(str.Length + 1, (match.Value.Length - str.Length) - 2);
                DataFormat dataFormat = defaultTransform.DataFormat;
                string str3 = str2.ToUpper();
                if (str3 != null)
                {
                    if (str3 != "ABCD")
                    {
                        if (str3 == "BADC")
                        {
                            goto Label_00C7;
                        }
                        if (str3 == "DCBA")
                        {
                            goto Label_00CC;
                        }
                        if (str3 == "CDAB")
                        {
                            goto Label_00D1;
                        }
                    }
                    else
                    {
                        dataFormat = DataFormat.ABCD;
                    }
                }
                goto Label_00D8;
            Label_00C7:
                dataFormat = DataFormat.BADC;
                goto Label_00D8;
            Label_00CC:
                dataFormat = DataFormat.DCBA;
                goto Label_00D8;
            Label_00D1:
                dataFormat = DataFormat.CDAB;
            Label_00D8:
                address = address.Replace(match.Value, "");
                if (dataFormat != defaultTransform.DataFormat)
                {
                    return defaultTransform.CreateByDateFormat(dataFormat);
                }
                transform = defaultTransform;
            }
            catch
            {
                throw;
            }
            return transform;
        }

        public static int GetBitIndexInformation(ref string address)
        {
            int num = 0;
            int length = address.LastIndexOf('.');
            if ((length > 0) && (length < (address.Length - 1)))
            {
                string str = address.Substring(length + 1);
                if ((((str.Contains("A") || str.Contains("B")) || (str.Contains("C") || str.Contains("D"))) || str.Contains("E")) || str.Contains("F"))
                {
                    num = Convert.ToInt32(str, 0x10);
                }
                else
                {
                    num = Convert.ToInt32(str);
                }
                address = address.Substring(0, length);
            }
            return num;
        }

        public static string GetIpAddressFromInput(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (Regex.IsMatch(value, @"^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$"))
                {
                    IPAddress address;
                    if (!IPAddress.TryParse(value, out address))
                    {
                        throw new Exception(StringResources.Language.IpAddressError);
                    }
                    return value;
                }
                IPAddress[] addressList = Dns.GetHostEntry(value).AddressList;
                if (addressList.Length > 0)
                {
                    return addressList[0].ToString();
                }
            }
            return "127.0.0.1";
        }

        public static byte[] GetUTF8Bytes(string message)
        {
            return (string.IsNullOrEmpty(message) ? new byte[0] : Encoding.UTF8.GetBytes(message));
        }

        public static string PathCombine(params string[] paths)
        {
            if (paths == null)
            {
                return string.Empty;
            }
            if (paths.Length == 0)
            {
                return string.Empty;
            }
            string str = paths[0];
            for (int i = 1; i < paths.Length; i++)
            {
                if (!string.IsNullOrEmpty(paths[i]))
                {
                    str = Path.Combine(str, paths[i]);
                }
            }
            return str;
        }

        public static byte[] ReadBinaryFromStream(Stream stream)
        {
            int length = BitConverter.ToInt32(ReadSpecifiedLengthFromStream(stream, 4), 0);
            if (length <= 0)
            {
                return new byte[0];
            }
            return ReadSpecifiedLengthFromStream(stream, length);
        }

        public static byte[] ReadSpecifiedLengthFromStream(Stream stream, int length)
        {
            byte[] buffer = new byte[length];
            int offset = 0;
            while (offset < length)
            {
                int num2 = stream.Read(buffer, offset, buffer.Length - offset);
                offset += num2;
                if (num2 == 0)
                {
                    return buffer;
                }
            }
            return buffer;
        }

        public static string ReadStringFromStream(Stream stream)
        {
            byte[] bytes = ReadBinaryFromStream(stream);
            return Encoding.UTF8.GetString(bytes);
        }

        public static OperateResult<int[], int[]> SplitReadLength(int address, ushort length, ushort segment)
        {
            int[] numArray = SoftBasic.SplitIntegerToArray(length, segment);
            int[] numArray2 = new int[numArray.Length];
            for (int i = 0; i < numArray2.Length; i++)
            {
                if (i == 0)
                {
                    numArray2[i] = address;
                }
                else
                {
                    numArray2[i] = numArray2[i - 1] + numArray[i - 1];
                }
            }
            return OperateResult.CreateSuccessResult<int[], int[]>(numArray2, numArray);
        }

        public static OperateResult<int[], List<T[]>> SplitWriteData<T>(int address, T[] value, ushort segment, int addressLength)
        {
            List<T[]> list = SoftBasic.ArraySplitByLength<T>(value, segment * addressLength);
            int[] numArray = new int[list.Count];
            for (int i = 0; i < numArray.Length; i++)
            {
                if (i == 0)
                {
                    numArray[i] = address;
                }
                else
                {
                    numArray[i] = numArray[i - 1] + (list[i - 1].Length / addressLength);
                }
            }
            return OperateResult.CreateSuccessResult<int[], List<T[]>>(numArray, list);
        }

        public static void WriteBinaryToStream(Stream stream, byte[] value)
        {
            stream.Write(BitConverter.GetBytes(value.Length), 0, 4);
            stream.Write(value, 0, value.Length);
        }

        public static void WriteStringToStream(Stream stream, string value)
        {
            byte[] buffer = string.IsNullOrEmpty(value) ? new byte[0] : Encoding.UTF8.GetBytes(value);
            WriteBinaryToStream(stream, buffer);
        }
    }
}

