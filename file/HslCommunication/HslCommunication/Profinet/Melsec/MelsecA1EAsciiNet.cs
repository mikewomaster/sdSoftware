namespace HslCommunication.Profinet.Melsec
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class MelsecA1EAsciiNet : NetworkDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <PLCNumber>k__BackingField;

        public MelsecA1EAsciiNet()
        {
            this.<PLCNumber>k__BackingField = 0xff;
            base.WordLength = 1;
            base.LogMsgFormatBinary = false;
            base.ByteTransform = new RegularByteTransform();
        }

        public MelsecA1EAsciiNet(string ipAddress, int port)
        {
            this.<PLCNumber>k__BackingField = 0xff;
            base.WordLength = 1;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.LogMsgFormatBinary = false;
            base.ByteTransform = new RegularByteTransform();
        }

        public static OperateResult<byte[]> BuildReadCommand(string address, ushort length, bool isBit, byte plcNumber)
        {
            OperateResult<MelsecA1EDataType, int> result = MelsecHelper.McA1EAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte num = isBit ? ((byte) 0) : ((byte) 1);
            return OperateResult.CreateSuccessResult<byte[]>(new byte[] { 
                SoftBasic.BuildAsciiBytesFrom(num)[0], SoftBasic.BuildAsciiBytesFrom(num)[1], SoftBasic.BuildAsciiBytesFrom(plcNumber)[0], SoftBasic.BuildAsciiBytesFrom(plcNumber)[1], 0x30, 0x30, 0x30, 0x41, SoftBasic.BuildAsciiBytesFrom(result.Content1.DataCode[0])[0], SoftBasic.BuildAsciiBytesFrom(result.Content1.DataCode[0])[1], SoftBasic.BuildAsciiBytesFrom(result.Content1.DataCode[1])[0], SoftBasic.BuildAsciiBytesFrom(result.Content1.DataCode[1])[1], SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[3])[0], SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[3])[1], SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[2])[0], SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[2])[1],
                SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[1])[0], SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[1])[1], SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[0])[0], SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[0])[1], SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes((int) (length % 0x100))[0])[0], SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes((int) (length % 0x100))[0])[1], 0x30, 0x30
            });
        }

        public static OperateResult<byte[]> BuildWriteBoolCommand(string address, bool[] value, byte plcNumber)
        {
            OperateResult<MelsecA1EDataType, int> result = MelsecHelper.McA1EAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] buffer = (from m in value select m ? ((IEnumerable<byte>) ((byte) 0x31)) : ((IEnumerable<byte>) ((byte) 0x30))).ToArray<byte>();
            if ((buffer.Length % 2) == 1)
            {
                byte[][] bytes = new byte[2][];
                bytes[0] = buffer;
                bytes[1] = new byte[] { 0x30 };
                buffer = SoftBasic.SpliceByteArray(bytes);
            }
            byte[] array = new byte[0x18 + buffer.Length];
            array[0] = 0x30;
            array[1] = 50;
            array[2] = SoftBasic.BuildAsciiBytesFrom(plcNumber)[0];
            array[3] = SoftBasic.BuildAsciiBytesFrom(plcNumber)[1];
            array[4] = 0x30;
            array[5] = 0x30;
            array[6] = 0x30;
            array[7] = 0x41;
            array[8] = SoftBasic.BuildAsciiBytesFrom(result.Content1.DataCode[0])[0];
            array[9] = SoftBasic.BuildAsciiBytesFrom(result.Content1.DataCode[0])[1];
            array[10] = SoftBasic.BuildAsciiBytesFrom(result.Content1.DataCode[1])[0];
            array[11] = SoftBasic.BuildAsciiBytesFrom(result.Content1.DataCode[1])[1];
            array[12] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[3])[0];
            array[13] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[3])[1];
            array[14] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[2])[0];
            array[15] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[2])[1];
            array[0x10] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[1])[0];
            array[0x11] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[1])[1];
            array[0x12] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[0])[0];
            array[0x13] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[0])[1];
            array[20] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(value.Length)[0])[0];
            array[0x15] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(value.Length)[0])[1];
            array[0x16] = 0x30;
            array[0x17] = 0x30;
            buffer.CopyTo(array, 0x18);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteWordCommand(string address, byte[] value, byte plcNumber)
        {
            OperateResult<MelsecA1EDataType, int> result = MelsecHelper.McA1EAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            value = MelsecHelper.TransByteArrayToAsciiByteArray(value);
            byte[] array = new byte[0x18 + value.Length];
            array[0] = 0x30;
            array[1] = 0x33;
            array[2] = SoftBasic.BuildAsciiBytesFrom(plcNumber)[0];
            array[3] = SoftBasic.BuildAsciiBytesFrom(plcNumber)[1];
            array[4] = 0x30;
            array[5] = 0x30;
            array[6] = 0x30;
            array[7] = 0x41;
            array[8] = SoftBasic.BuildAsciiBytesFrom(result.Content1.DataCode[0])[0];
            array[9] = SoftBasic.BuildAsciiBytesFrom(result.Content1.DataCode[0])[1];
            array[10] = SoftBasic.BuildAsciiBytesFrom(result.Content1.DataCode[1])[0];
            array[11] = SoftBasic.BuildAsciiBytesFrom(result.Content1.DataCode[1])[1];
            array[12] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[3])[0];
            array[13] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[3])[1];
            array[14] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[2])[0];
            array[15] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[2])[1];
            array[0x10] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[1])[0];
            array[0x11] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[1])[1];
            array[0x12] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[0])[0];
            array[0x13] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes(result.Content2)[0])[1];
            array[20] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes((int) (value.Length / 4))[0])[0];
            array[0x15] = SoftBasic.BuildAsciiBytesFrom(BitConverter.GetBytes((int) (value.Length / 4))[0])[1];
            array[0x16] = 0x30;
            array[0x17] = 0x30;
            value.CopyTo(array, 0x18);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult CheckResponseLegal(byte[] response)
        {
            if (response.Length < 4)
            {
                return new OperateResult(StringResources.Language.ReceiveDataLengthTooShort);
            }
            if ((response[2] == 0x30) && (response[3] == 0x30))
            {
                return OperateResult.CreateSuccessResult();
            }
            if ((response[2] == 0x35) && (response[3] == 0x42))
            {
                return new OperateResult(Convert.ToInt32(Encoding.ASCII.GetString(response, 4, 2), 0x10), StringResources.Language.MelsecPleaseReferToManualDocument);
            }
            return new OperateResult(Convert.ToInt32(Encoding.ASCII.GetString(response, 2, 2), 0x10), StringResources.Language.MelsecPleaseReferToManualDocument);
        }

        public static OperateResult<byte[]> ExtractActualData(byte[] response, bool isBit)
        {
            if (isBit)
            {
                return OperateResult.CreateSuccessResult<byte[]>((from m in response.RemoveBegin<byte>(4) select (m == 0x30) ? ((IEnumerable<byte>) ((byte) 0)) : ((IEnumerable<byte>) ((byte) 1))).ToArray<byte>());
            }
            return OperateResult.CreateSuccessResult<byte[]>(MelsecHelper.TransAsciiByteArrayToByteArray(response.RemoveBegin<byte>(4)));
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new MelsecA1EAsciiMessage();
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = BuildReadCommand(address, length, false, this.PLCNumber);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult result3 = CheckResponseLegal(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return ExtractActualData(result2.Content, false);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<byte[]> result = BuildReadCommand(address, length, true, this.PLCNumber);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult result3 = CheckResponseLegal(result2.Content);
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

        public override string ToString()
        {
            return string.Format("MelsecA1ENet[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] values)
        {
            OperateResult<byte[]> result = BuildWriteBoolCommand(address, values, this.PLCNumber);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return CheckResponseLegal(result2.Content);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result = BuildWriteWordCommand(address, value, this.PLCNumber);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = CheckResponseLegal(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return OperateResult.CreateSuccessResult();
        }

        public byte PLCNumber { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MelsecA1EAsciiNet.<>c <>9 = new MelsecA1EAsciiNet.<>c();
            public static Func<bool, byte> <>9__14_0;
            public static Func<byte, byte> <>9__16_0;
            public static Func<byte, bool> <>9__9_0;

            internal byte <BuildWriteBoolCommand>b__14_0(bool m)
            {
                return (m ? ((byte) 0x31) : ((byte) 0x30));
            }

            internal byte <ExtractActualData>b__16_0(byte m)
            {
                return ((m == 0x30) ? ((byte) 0) : ((byte) 1));
            }

            internal bool <ReadBool>b__9_0(byte m)
            {
                return (m == 1);
            }
        }
    }
}

