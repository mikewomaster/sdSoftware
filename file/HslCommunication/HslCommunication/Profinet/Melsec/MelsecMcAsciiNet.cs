namespace HslCommunication.Profinet.Melsec
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
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

    public class MelsecMcAsciiNet : NetworkDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <NetworkNumber>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <NetworkStationNumber>k__BackingField;

        public MelsecMcAsciiNet()
        {
            this.<NetworkNumber>k__BackingField = 0;
            this.<NetworkStationNumber>k__BackingField = 0;
            base.WordLength = 1;
            base.LogMsgFormatBinary = false;
            base.ByteTransform = new RegularByteTransform();
        }

        public MelsecMcAsciiNet(string ipAddress, int port)
        {
            this.<NetworkNumber>k__BackingField = 0;
            this.<NetworkStationNumber>k__BackingField = 0;
            base.WordLength = 1;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.LogMsgFormatBinary = false;
            base.ByteTransform = new RegularByteTransform();
        }

        public static OperateResult CheckResponseContent(byte[] content)
        {
            ushort err = Convert.ToUInt16(Encoding.ASCII.GetString(content, 0x12, 4), 0x10);
            if (err > 0)
            {
                return new OperateResult(err, MelsecHelper.GetErrorDescription(err));
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi]
        public OperateResult ErrorStateReset()
        {
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(Encoding.ASCII.GetBytes("01010000"), this.NetworkNumber, this.NetworkStationNumber));
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
                return OperateResult.CreateSuccessResult<byte[]>((from m in response.RemoveBegin<byte>(0x16) select (m == 0x30) ? ((IEnumerable<byte>) ((byte) 0)) : ((IEnumerable<byte>) ((byte) 1))).ToArray<byte>());
            }
            return OperateResult.CreateSuccessResult<byte[]>(MelsecHelper.TransAsciiByteArrayToByteArray(response.RemoveBegin<byte>(0x16)));
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new MelsecQnA3EAsciiMessage();
        }

        protected virtual OperateResult<McAddressData> McAnalysisAddress(string address, ushort length)
        {
            return McAddressData.ParseMelsecFrom(address, length);
        }

        public static byte[] PackMcCommand(byte[] mcCore, [Optional, DefaultParameterValue(0)] byte networkNumber, [Optional, DefaultParameterValue(0)] byte networkStationNumber)
        {
            byte[] array = new byte[0x16 + mcCore.Length];
            array[0] = 0x35;
            array[1] = 0x30;
            array[2] = 0x30;
            array[3] = 0x30;
            array[4] = SoftBasic.BuildAsciiBytesFrom(networkNumber)[0];
            array[5] = SoftBasic.BuildAsciiBytesFrom(networkNumber)[1];
            array[6] = 70;
            array[7] = 70;
            array[8] = 0x30;
            array[9] = 0x33;
            array[10] = 70;
            array[11] = 70;
            array[12] = SoftBasic.BuildAsciiBytesFrom(networkStationNumber)[0];
            array[13] = SoftBasic.BuildAsciiBytesFrom(networkStationNumber)[1];
            array[14] = SoftBasic.BuildAsciiBytesFrom((ushort) (array.Length - 0x12))[0];
            array[15] = SoftBasic.BuildAsciiBytesFrom((ushort) (array.Length - 0x12))[1];
            array[0x10] = SoftBasic.BuildAsciiBytesFrom((ushort) (array.Length - 0x12))[2];
            array[0x11] = SoftBasic.BuildAsciiBytesFrom((ushort) (array.Length - 0x12))[3];
            array[0x12] = 0x30;
            array[0x13] = 0x30;
            array[20] = 0x31;
            array[0x15] = 0x30;
            mcCore.CopyTo(array, 0x16);
            return array;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<McAddressData> result = this.McAnalysisAddress(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            List<byte> list = new List<byte>();
            ushort num = 0;
            while (num < length)
            {
                ushort num2 = (ushort) Math.Min(length - num, 450);
                result.Content.Length = num2;
                OperateResult<byte[]> result3 = this.ReadAddressData(result.Content);
                if (!result3.IsSuccess)
                {
                    return result3;
                }
                list.AddRange(result3.Content);
                num = (ushort) (num + num2);
                if (result.Content.McDataType.DataType == 0)
                {
                    McAddressData content = result.Content;
                    content.AddressStart += num2;
                }
                else
                {
                    McAddressData local2 = result.Content;
                    local2.AddressStart += num2 * 0x10;
                }
            }
            return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
        }

        private OperateResult<byte[]> ReadAddressData(McAddressData addressData)
        {
            byte[] mcCore = MelsecHelper.BuildAsciiReadMcCoreCommand(addressData, false);
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
            return ExtractActualData(result.Content, false);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<McAddressData> result = this.McAnalysisAddress(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            byte[] mcCore = MelsecHelper.BuildAsciiReadMcCoreCommand(result.Content, true);
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
            OperateResult<byte[]> result4 = ExtractActualData(result2.Content, true);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result4);
            }
            return OperateResult.CreateSuccessResult<bool[]>((from m in result4.Content select m == 1).Take<bool>(length).ToArray<bool>());
        }

        [HslMqttApi]
        public OperateResult<string> ReadPlcType()
        {
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(Encoding.ASCII.GetBytes("01010000"), this.NetworkNumber, this.NetworkStationNumber));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult result2 = CheckResponseContent(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            return OperateResult.CreateSuccessResult<string>(Encoding.ASCII.GetString(result.Content, 0x16, 0x10).TrimEnd(new char[0]));
        }

        [HslMqttApi]
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
            byte[] mcCore = MelsecHelper.BuildAsciiReadRandomWordCommand(dataArray);
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
            return ExtractActualData(result.Content, false);
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
            byte[] mcCore = MelsecHelper.BuildAsciiReadRandomCommand(dataArray);
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
            return ExtractActualData(result.Content, false);
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
        public OperateResult RemoteReset()
        {
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(Encoding.ASCII.GetBytes("100600000001"), this.NetworkNumber, this.NetworkStationNumber));
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
        public OperateResult RemoteRun()
        {
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(Encoding.ASCII.GetBytes("1001000000010000"), this.NetworkNumber, this.NetworkStationNumber));
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
            OperateResult<byte[]> result = base.ReadFromCoreServer(PackMcCommand(Encoding.ASCII.GetBytes("100200000001"), this.NetworkNumber, this.NetworkStationNumber));
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
            return string.Format("MelsecMcAsciiNet[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] values)
        {
            OperateResult<McAddressData> result = this.McAnalysisAddress(address, 0);
            if (!result.IsSuccess)
            {
                return result;
            }
            byte[] mcCore = MelsecHelper.BuildAsciiWriteBitCoreCommand(result.Content, values);
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
            byte[] mcCore = MelsecHelper.BuildAsciiWriteWordCoreCommand(result.Content, value);
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

        public byte NetworkNumber { get; set; }

        public byte NetworkStationNumber { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MelsecMcAsciiNet.<>c <>9 = new MelsecMcAsciiNet.<>c();
            public static Func<byte, bool> <>9__18_0;
            public static Func<byte, byte> <>9__27_0;

            internal byte <ExtractActualData>b__27_0(byte m)
            {
                return ((m == 0x30) ? ((byte) 0) : ((byte) 1));
            }

            internal bool <ReadBool>b__18_0(byte m)
            {
                return (m == 1);
            }
        }
    }
}

