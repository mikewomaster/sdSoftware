namespace HslCommunication.Profinet.Melsec
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class MelsecA1ENet : NetworkDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <PLCNumber>k__BackingField;

        public MelsecA1ENet()
        {
            this.<PLCNumber>k__BackingField = 0xff;
            base.WordLength = 1;
            base.ByteTransform = new RegularByteTransform();
        }

        public MelsecA1ENet(string ipAddress, int port)
        {
            this.<PLCNumber>k__BackingField = 0xff;
            base.WordLength = 1;
            this.IpAddress = ipAddress;
            this.Port = port;
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
            return OperateResult.CreateSuccessResult<byte[]>(new byte[] { num, plcNumber, 10, 0, BitConverter.GetBytes(result.Content2)[0], BitConverter.GetBytes(result.Content2)[1], BitConverter.GetBytes(result.Content2)[2], BitConverter.GetBytes(result.Content2)[3], result.Content1.DataCode[1], result.Content1.DataCode[0], (byte) (length % 0x100), 0 });
        }

        public static OperateResult<byte[]> BuildWriteBoolCommand(string address, bool[] value, byte plcNumber)
        {
            OperateResult<MelsecA1EDataType, int> result = MelsecHelper.McA1EAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] sourceArray = MelsecHelper.TransBoolArrayToByteData(value);
            byte[] destinationArray = new byte[12 + sourceArray.Length];
            destinationArray[0] = 2;
            destinationArray[1] = plcNumber;
            destinationArray[2] = 10;
            destinationArray[3] = 0;
            destinationArray[4] = BitConverter.GetBytes(result.Content2)[0];
            destinationArray[5] = BitConverter.GetBytes(result.Content2)[1];
            destinationArray[6] = BitConverter.GetBytes(result.Content2)[2];
            destinationArray[7] = BitConverter.GetBytes(result.Content2)[3];
            destinationArray[8] = result.Content1.DataCode[1];
            destinationArray[9] = result.Content1.DataCode[0];
            destinationArray[10] = BitConverter.GetBytes(value.Length)[0];
            destinationArray[11] = BitConverter.GetBytes(value.Length)[1];
            Array.Copy(sourceArray, 0, destinationArray, 12, sourceArray.Length);
            return OperateResult.CreateSuccessResult<byte[]>(destinationArray);
        }

        public static OperateResult<byte[]> BuildWriteWordCommand(string address, byte[] value, byte plcNumber)
        {
            OperateResult<MelsecA1EDataType, int> result = MelsecHelper.McA1EAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] destinationArray = new byte[12 + value.Length];
            destinationArray[0] = 3;
            destinationArray[1] = plcNumber;
            destinationArray[2] = 10;
            destinationArray[3] = 0;
            destinationArray[4] = BitConverter.GetBytes(result.Content2)[0];
            destinationArray[5] = BitConverter.GetBytes(result.Content2)[1];
            destinationArray[6] = BitConverter.GetBytes(result.Content2)[2];
            destinationArray[7] = BitConverter.GetBytes(result.Content2)[3];
            destinationArray[8] = result.Content1.DataCode[1];
            destinationArray[9] = result.Content1.DataCode[0];
            destinationArray[10] = BitConverter.GetBytes((int) (value.Length / 2))[0];
            destinationArray[11] = BitConverter.GetBytes((int) (value.Length / 2))[1];
            Array.Copy(value, 0, destinationArray, 12, value.Length);
            return OperateResult.CreateSuccessResult<byte[]>(destinationArray);
        }

        public static OperateResult CheckResponseLegal(byte[] response)
        {
            if (response.Length < 2)
            {
                return new OperateResult(StringResources.Language.ReceiveDataLengthTooShort);
            }
            if (response[1] == 0)
            {
                return OperateResult.CreateSuccessResult();
            }
            if (response[1] == 0x5b)
            {
                return new OperateResult(response[2], StringResources.Language.MelsecPleaseReferToManualDocument);
            }
            return new OperateResult(response[1], StringResources.Language.MelsecPleaseReferToManualDocument);
        }

        public static OperateResult<byte[]> ExtractActualData(byte[] response, bool isBit)
        {
            if (isBit)
            {
                byte[] buffer = new byte[(response.Length - 2) * 2];
                for (int i = 2; i < response.Length; i++)
                {
                    if ((response[i] & 0x10) == 0x10)
                    {
                        buffer[(i - 2) * 2] = 1;
                    }
                    if ((response[i] & 1) == 1)
                    {
                        buffer[((i - 2) * 2) + 1] = 1;
                    }
                }
                return OperateResult.CreateSuccessResult<byte[]>(buffer);
            }
            return OperateResult.CreateSuccessResult<byte[]>(response.RemoveBegin<byte>(2));
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new MelsecA1EBinaryMessage();
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
        public override OperateResult Write(string address, bool[] value)
        {
            OperateResult<byte[]> result = BuildWriteBoolCommand(address, value, this.PLCNumber);
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
            public static readonly MelsecA1ENet.<>c <>9 = new MelsecA1ENet.<>c();
            public static Func<byte, bool> <>9__9_0;

            internal bool <ReadBool>b__9_0(byte m)
            {
                return (m == 1);
            }
        }
    }
}

