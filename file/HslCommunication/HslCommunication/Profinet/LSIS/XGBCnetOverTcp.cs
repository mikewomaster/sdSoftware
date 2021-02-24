namespace HslCommunication.Profinet.LSIS
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class XGBCnetOverTcp : NetworkDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <Station>k__BackingField = 5;

        public XGBCnetOverTcp()
        {
            base.WordLength = 2;
            base.ByteTransform = new RegularByteTransform();
            base.SleepTime = 20;
        }

        public static OperateResult<string> AnalysisAddress(string address)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.Append("%");
                char[] chArray = new char[] { 'P', 'M', 'L', 'K', 'F', 'T', 'C', 'D', 'S', 'Q', 'I', 'N', 'U', 'Z', 'R' };
                bool flag = false;
                for (int i = 0; i < chArray.Length; i++)
                {
                    if (chArray[i] == address[0])
                    {
                        builder.Append(chArray[i]);
                        char ch2 = address[1];
                        if (ch2 == 'X')
                        {
                            builder.Append("X");
                            if ((address[0] == 'I') || (address[0] == 'Q'))
                            {
                                builder.Append(CalculateAddressStarted(address.Substring(2), true));
                            }
                            else if (IsHex(address.Substring(2)))
                            {
                                builder.Append(address.Substring(2));
                            }
                            else
                            {
                                builder.Append(CalculateAddressStarted(address.Substring(2), false));
                            }
                        }
                        else
                        {
                            builder.Append("B");
                            int num2 = 0;
                            if (address[1] == 'B')
                            {
                                num2 = CalculateAddressStarted(address.Substring(2), false);
                                builder.Append((num2 == 0) ? num2 : (num2 *= 2));
                            }
                            else if (address[1] == 'W')
                            {
                                num2 = CalculateAddressStarted(address.Substring(2), false);
                                builder.Append((num2 == 0) ? num2 : (num2 *= 2));
                            }
                            else if (address[1] == 'D')
                            {
                                num2 = CalculateAddressStarted(address.Substring(2), false);
                                builder.Append((num2 == 0) ? num2 : (num2 *= 4));
                            }
                            else if (address[1] == 'L')
                            {
                                num2 = CalculateAddressStarted(address.Substring(2), false);
                                builder.Append((num2 == 0) ? num2 : (num2 *= 8));
                            }
                            else if ((address[0] == 'I') || (address[0] == 'Q'))
                            {
                                builder.Append(CalculateAddressStarted(address.Substring(1), true));
                            }
                            else if (IsHex(address.Substring(1)))
                            {
                                builder.Append(address.Substring(1));
                            }
                            else
                            {
                                builder.Append(CalculateAddressStarted(address.Substring(1), false));
                            }
                        }
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    throw new Exception(StringResources.Language.NotSupportedDataType);
                }
            }
            catch (Exception exception)
            {
                return new OperateResult<string>(exception.Message);
            }
            return OperateResult.CreateSuccessResult<string>(builder.ToString());
        }

        public static OperateResult<byte[]> BuildReadByteCommand(byte station, string address, ushort length)
        {
            OperateResult<string> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            List<byte> list = new List<byte> { 5 };
            list.AddRange(SoftBasic.BuildAsciiBytesFrom(station));
            list.Add(0x72);
            list.Add(0x53);
            list.Add(0x42);
            list.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) result.Content.Length));
            list.AddRange(Encoding.ASCII.GetBytes(result.Content));
            list.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) length));
            list.Add(4);
            int num = 0;
            for (int i = 0; i < list.Count; i++)
            {
                num += list[i];
            }
            list.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) num));
            return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
        }

        public static OperateResult<byte[]> BuildReadCommand(byte station, string address, ushort length)
        {
            OperateResult<string> dataTypeToAddress = XGBFastEnet.GetDataTypeToAddress(address);
            if (!dataTypeToAddress.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(dataTypeToAddress);
            }
            switch (dataTypeToAddress.Content)
            {
                case "Bit":
                    return BuildReadOneCommand(station, address, length);

                case "Word":
                case "DWord":
                case "LWord":
                case "Continuous":
                    return BuildReadByteCommand(station, address, length);
            }
            return new OperateResult<byte[]>(StringResources.Language.NotSupportedDataType);
        }

        public static OperateResult<byte[]> BuildReadOneCommand(byte station, string address, ushort length)
        {
            OperateResult<string> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            List<byte> list = new List<byte> { 5 };
            list.AddRange(SoftBasic.BuildAsciiBytesFrom(station));
            list.Add(0x72);
            list.Add(0x53);
            list.Add(0x53);
            list.Add(0x30);
            list.Add(0x31);
            list.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) result.Content.Length));
            list.AddRange(Encoding.ASCII.GetBytes(result.Content));
            list.Add(4);
            int num = 0;
            for (int i = 0; i < list.Count; i++)
            {
                num += list[i];
            }
            list.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) num));
            return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
        }

        public static OperateResult<byte[]> BuildWriteByteCommand(byte station, string address, byte[] value)
        {
            OperateResult<string> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            List<byte> list = new List<byte> { 5 };
            list.AddRange(SoftBasic.BuildAsciiBytesFrom(station));
            list.Add(0x77);
            list.Add(0x53);
            list.Add(0x42);
            list.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) result.Content.Length));
            list.AddRange(Encoding.ASCII.GetBytes(result.Content));
            list.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) value.Length));
            list.AddRange(SoftBasic.BytesToAsciiBytes(value));
            list.Add(4);
            int num = 0;
            for (int i = 0; i < list.Count; i++)
            {
                num += list[i];
            }
            list.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) num));
            return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
        }

        public static OperateResult<byte[]> BuildWriteCommand(byte station, string address, byte[] value)
        {
            OperateResult<string> dataTypeToAddress = XGBFastEnet.GetDataTypeToAddress(address);
            if (!dataTypeToAddress.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(dataTypeToAddress);
            }
            switch (dataTypeToAddress.Content)
            {
                case "Bit":
                    return BuildWriteOneCommand(station, address, value);

                case "Word":
                case "DWord":
                case "LWord":
                case "Continuous":
                    return BuildWriteByteCommand(station, address, value);
            }
            return new OperateResult<byte[]>(StringResources.Language.NotSupportedDataType);
        }

        public static OperateResult<byte[]> BuildWriteOneCommand(byte station, string address, byte[] value)
        {
            OperateResult<string> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            List<byte> list = new List<byte> { 5 };
            list.AddRange(SoftBasic.BuildAsciiBytesFrom(station));
            list.Add(0x77);
            list.Add(0x53);
            list.Add(0x53);
            list.Add(0x30);
            list.Add(0x31);
            list.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) result.Content.Length));
            list.AddRange(Encoding.ASCII.GetBytes(result.Content));
            list.AddRange(SoftBasic.BytesToAsciiBytes(value));
            list.Add(4);
            int num = 0;
            for (int i = 0; i < list.Count; i++)
            {
                num += list[i];
            }
            list.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) num));
            return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
        }

        public static int CalculateAddressStarted(string address, [Optional, DefaultParameterValue(false)] bool QI)
        {
            if (address.IndexOf('.') < 0)
            {
                return Convert.ToInt32(address);
            }
            char[] separator = new char[] { '.' };
            string[] strArray = address.Split(separator);
            if (!QI)
            {
                return Convert.ToInt32(strArray[0]);
            }
            return Convert.ToInt32(strArray[2]);
        }

        public static OperateResult<byte[]> ExtractActualData(byte[] response, bool isRead)
        {
            try
            {
                if (isRead)
                {
                    if (response[0] == 6)
                    {
                        byte[] buffer = new byte[response.Length - 13];
                        Array.Copy(response, 10, buffer, 0, buffer.Length);
                        return OperateResult.CreateSuccessResult<byte[]>(SoftBasic.AsciiBytesToBytes(buffer));
                    }
                    byte[] buffer2 = new byte[response.Length - 9];
                    Array.Copy(response, 6, buffer2, 0, buffer2.Length);
                    return new OperateResult<byte[]>(BitConverter.ToUInt16(SoftBasic.AsciiBytesToBytes(buffer2), 0), "Data:" + SoftBasic.ByteToHexString(response));
                }
                if (response[0] == 6)
                {
                    return OperateResult.CreateSuccessResult<byte[]>(new byte[0]);
                }
                byte[] destinationArray = new byte[response.Length - 9];
                Array.Copy(response, 6, destinationArray, 0, destinationArray.Length);
                return new OperateResult<byte[]>(BitConverter.ToUInt16(SoftBasic.AsciiBytesToBytes(destinationArray), 0), "Data:" + SoftBasic.ByteToHexString(response));
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message);
            }
        }

        private static bool IsHex(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            bool flag = false;
            for (int i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                        flag = true;
                        break;
                }
            }
            return flag;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = BuildReadCommand(station, address, length);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return ExtractActualData(result2.Content, true);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = BuildReadOneCommand(station, address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            return OperateResult.CreateSuccessResult<bool[]>(SoftBasic.ByteToBoolArray(ExtractActualData(result2.Content, true).Content, length));
        }

        [HslMqttApi("ReadByte", "")]
        public OperateResult<byte> ReadByte(string address)
        {
            return ByteTransformHelper.GetResultFromArray<byte>(this.Read(address, 2));
        }

        public OperateResult<bool> ReadCoil(string address)
        {
            return this.ReadBool(address);
        }

        public OperateResult<bool[]> ReadCoil(string address, ushort length)
        {
            return this.ReadBool(address, length);
        }

        public override string ToString()
        {
            return string.Format("XGBCnetOverTcp[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            byte[] buffer1 = new byte[] { value ? ((byte) 1) : ((byte) 0) };
            return this.Write(address, buffer1);
        }

        [HslMqttApi("WriteByte", "")]
        public OperateResult Write(string address, byte value)
        {
            byte[] buffer1 = new byte[] { value };
            return this.Write(address, buffer1);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = BuildWriteCommand(station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return ExtractActualData(result2.Content, false);
        }

        public OperateResult WriteCoil(string address, bool value)
        {
            return this.Write(address, value);
        }

        public byte Station { get; set; }
    }
}

