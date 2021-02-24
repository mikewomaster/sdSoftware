namespace HslCommunication.Profinet.Melsec
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Core.Address;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class MelsecMcNet : NetworkDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <NetworkNumber>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <NetworkStationNumber>k__BackingField;

        public MelsecMcNet()
        {
            this.<NetworkNumber>k__BackingField = 0;
            this.<NetworkStationNumber>k__BackingField = 0;
            base.WordLength = 1;
            base.ByteTransform = new RegularByteTransform();
        }

        public MelsecMcNet(string ipAddress, int port)
        {
            this.<NetworkNumber>k__BackingField = 0;
            this.<NetworkStationNumber>k__BackingField = 0;
            base.WordLength = 1;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
        }

        public static OperateResult CheckResponseContent(byte[] content)
        {
            ushort err = BitConverter.ToUInt16(content, 9);
            if (err > 0)
            {
                return new OperateResult<byte[]>(err, MelsecHelper.GetErrorDescription(err));
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi]
        public OperateResult ErrorStateReset()
        {
            byte[] mcCore = new byte[4];
            mcCore[0] = 0x17;
            mcCore[1] = 0x16;
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult result2 = CheckResponseContent(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return OperateResult.CreateSuccessResult();
        }

        public static OperateResult<byte[]> ExtractActualData(byte[] response, bool isBit)
        {
            if (isBit)
            {
                byte[] buffer = new byte[response.Length * 2];
                for (int i = 0; i < response.Length; i++)
                {
                    if ((response[i] & 0x10) == 0x10)
                    {
                        buffer[i * 2] = 1;
                    }
                    if ((response[i] & 1) == 1)
                    {
                        buffer[(i * 2) + 1] = 1;
                    }
                }
                return OperateResult.CreateSuccessResult<byte[]>(buffer);
            }
            return OperateResult.CreateSuccessResult<byte[]>(response);
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new MelsecQnA3EBinaryMessage();
        }

        protected virtual OperateResult<McAddressData> McAnalysisAddress(string address, ushort length)
        {
            return McAddressData.ParseMelsecFrom(address, length);
        }

        public static byte[] PackMcCommand(byte[] mcCore, [Optional, DefaultParameterValue(0)] byte networkNumber, [Optional, DefaultParameterValue(0)] byte networkStationNumber)
        {
            byte[] array = new byte[11 + mcCore.Length];
            array[0] = 80;
            array[1] = 0;
            array[2] = networkNumber;
            array[3] = 0xff;
            array[4] = 0xff;
            array[5] = 3;
            array[6] = networkStationNumber;
            array[7] = (byte) ((array.Length - 9) % 0x100);
            array[8] = (byte) ((array.Length - 9) / 0x100);
            array[9] = 10;
            array[10] = 0;
            mcCore.CopyTo(array, 11);
            return array;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            if (address.StartsWith("s=") || address.StartsWith("S="))
            {
                return this.ReadTags(address.Substring(2), length);
            }
            if (Regex.IsMatch(address, "ext=[0-9]+;", RegexOptions.IgnoreCase))
            {
                string input = Regex.Match(address, "ext=[0-9]+;").Value;
                ushort extend = ushort.Parse(Regex.Match(input, "[0-9]+").Value);
                return this.ReadExtend(extend, address.Substring(input.Length), length);
            }
            if (Regex.IsMatch(address, "mem=", RegexOptions.IgnoreCase))
            {
                return this.ReadMemory(address.Substring(4), length);
            }
            OperateResult<McAddressData> result = this.McAnalysisAddress(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            List<byte> list = new List<byte>();
            ushort num2 = 0;
            while (num2 < length)
            {
                ushort num3 = (ushort) Math.Min(length - num2, 900);
                result.Content.Length = num3;
                OperateResult<byte[]> result3 = this.ReadAddressData(result.Content);
                if (!result3.IsSuccess)
                {
                    return result3;
                }
                list.AddRange(result3.Content);
                num2 = (ushort) (num2 + num3);
                if (result.Content.McDataType.DataType == 0)
                {
                    McAddressData content = result.Content;
                    content.AddressStart += num3;
                }
                else
                {
                    McAddressData local2 = result.Content;
                    local2.AddressStart += num3 * 0x10;
                }
            }
            return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
        }

        private OperateResult<byte[]> ReadAddressData(McAddressData addressData)
        {
            byte[] mcCore = MelsecHelper.BuildReadMcCoreCommand(addressData, false);
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult result2 = CheckResponseContent(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            return ExtractActualData(result.Content.RemoveBegin<byte>(11), false);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<McAddressData> result = this.McAnalysisAddress(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            byte[] mcCore = MelsecHelper.BuildReadMcCoreCommand(result.Content, true);
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult result3 = CheckResponseContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            OperateResult<byte[]> result4 = ExtractActualData(result2.Content.RemoveBegin<byte>(11), true);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result4);
            }
            return OperateResult.CreateSuccessResult<bool[]>((from m in result4.Content select m == 1).Take<bool>(length).ToArray<bool>());
        }

        public OperateResult<byte[]> ReadExtend(ushort extend, string address, ushort length)
        {
            OperateResult<McAddressData> result = this.McAnalysisAddress(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] mcCore = MelsecHelper.BuildReadMcCoreExtendCommand(result.Content, extend, false);
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult result3 = CheckResponseContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            OperateResult<byte[]> result4 = ExtractActualData(result2.Content.RemoveBegin<byte>(11), false);
            if (!result4.IsSuccess)
            {
                return result4;
            }
            return MelsecHelper.ExtraTagData(result4.Content);
        }

        public OperateResult<byte[]> ReadMemory(string address, ushort length)
        {
            OperateResult<byte[]> result = MelsecHelper.BuildReadMemoryCommand(address, length);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(PackMcCommand(result.Content, this.NetworkNumber, this.NetworkStationNumber));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult result3 = CheckResponseContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return ExtractActualData(result2.Content.RemoveBegin<byte>(11), false);
        }

        [HslMqttApi]
        public OperateResult<string> ReadPlcType()
        {
            byte[] mcCore = new byte[4];
            mcCore[0] = 1;
            mcCore[1] = 1;
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult result2 = CheckResponseContent(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            return OperateResult.CreateSuccessResult<string>(Encoding.ASCII.GetString(result.Content, 11, 0x10).TrimEnd(new char[0]));
        }

        public OperateResult<byte[]> ReadRandom(string[] address)
        {
            McAddressData[] dataArray = new McAddressData[address.Length];
            for (int i = 0; i < address.Length; i++)
            {
                OperateResult<McAddressData> result3 = McAddressData.ParseMelsecFrom(address[i], 1);
                if (!result3.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result3);
                }
                dataArray[i] = result3.Content;
            }
            byte[] mcCore = MelsecHelper.BuildReadRandomWordCommand(dataArray);
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult result2 = CheckResponseContent(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            return ExtractActualData(result.Content.RemoveBegin<byte>(11), false);
        }

        public OperateResult<byte[]> ReadRandom(string[] address, ushort[] length)
        {
            if (length.Length != address.Length)
            {
                return new OperateResult<byte[]>(StringResources.Language.TwoParametersLengthIsNotSame);
            }
            McAddressData[] dataArray = new McAddressData[address.Length];
            for (int i = 0; i < address.Length; i++)
            {
                OperateResult<McAddressData> result4 = McAddressData.ParseMelsecFrom(address[i], length[i]);
                if (!result4.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result4);
                }
                dataArray[i] = result4.Content;
            }
            byte[] mcCore = MelsecHelper.BuildReadRandomCommand(dataArray);
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult result2 = CheckResponseContent(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            return ExtractActualData(result.Content.RemoveBegin<byte>(11), false);
        }

        public OperateResult<short[]> ReadRandomInt16(string[] address)
        {
            OperateResult<byte[]> result = this.ReadRandom(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<short[]>(result);
            }
            return OperateResult.CreateSuccessResult<short[]>(base.ByteTransform.TransInt16(result.Content, 0, address.Length));
        }

        [HslMqttApi]
        public OperateResult<byte[]> ReadTags(string tag, ushort length)
        {
            string[] tags = new string[] { tag };
            ushort[] numArray1 = new ushort[] { length };
            return this.ReadTags(tags, numArray1);
        }

        public OperateResult<byte[]> ReadTags(string[] tags, ushort[] length)
        {
            byte[] mcCore = MelsecHelper.BuildReadTag(tags, length);
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult result2 = CheckResponseContent(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult<byte[]> result3 = ExtractActualData(result.Content.RemoveBegin<byte>(11), false);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return MelsecHelper.ExtraTagData(result3.Content);
        }

        [HslMqttApi]
        public OperateResult RemoteReset()
        {
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(new byte[] { 6, 0x10, 0, 0, 1, 0 }, this.NetworkNumber, this.NetworkStationNumber));
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult result2 = CheckResponseContent(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi]
        public OperateResult RemoteRun([Optional, DefaultParameterValue(false)] bool force)
        {
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(new byte[] { 1, 0x10, 0, 0, 1, 0, 0, 0 }, this.NetworkNumber, this.NetworkStationNumber));
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult result2 = CheckResponseContent(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi]
        public OperateResult RemoteStop()
        {
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(new byte[] { 2, 0x10, 0, 0, 1, 0 }, this.NetworkNumber, this.NetworkStationNumber));
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult result2 = CheckResponseContent(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return OperateResult.CreateSuccessResult();
        }

        public override string ToString()
        {
            return string.Format("MelsecMcNet[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] values)
        {
            OperateResult<McAddressData> result = this.McAnalysisAddress(address, 0);
            if (!result.IsSuccess)
            {
                return result;
            }
            byte[] mcCore = MelsecHelper.BuildWriteBitCoreCommand(result.Content, values);
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = CheckResponseContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<McAddressData> result = this.McAnalysisAddress(address, 0);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return this.WriteAddressData(result.Content, value);
        }

        private OperateResult WriteAddressData(McAddressData addressData, byte[] value)
        {
            byte[] mcCore = MelsecHelper.BuildWriteWordCoreCommand(addressData, value);
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult result2 = CheckResponseContent(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            return OperateResult.CreateSuccessResult();
        }

        public byte NetworkNumber { get; set; }

        public byte NetworkStationNumber { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MelsecMcNet.<>c <>9 = new MelsecMcNet.<>c();
            public static Func<byte, bool> <>9__19_0;

            internal bool <ReadBool>b__19_0(byte m)
            {
                return (m == 1);
            }
        }
    }
}

