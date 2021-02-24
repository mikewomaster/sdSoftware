namespace HslCommunication.Profinet.Omron
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Profinet.AllenBradley;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class OmronConnectedCipNet : NetworkDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ProductName>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private uint <SessionHandle>k__BackingField;
        private SoftIncrementCount incrementCount;
        private uint OTConnectionId;
        private Random random;

        public OmronConnectedCipNet()
        {
            this.OTConnectionId = 0;
            this.incrementCount = new SoftIncrementCount(0xffffL, 0L, 1);
            this.random = new Random();
            base.WordLength = 2;
            base.ByteTransform = new RegularByteTransform();
        }

        public OmronConnectedCipNet(string ipAddress, [Optional, DefaultParameterValue(0xaf12)] int port)
        {
            this.OTConnectionId = 0;
            this.incrementCount = new SoftIncrementCount(0xffffL, 0L, 1);
            this.random = new Random();
            this.IpAddress = ipAddress;
            this.Port = port;
            base.WordLength = 2;
            base.ByteTransform = new RegularByteTransform();
        }

        private OperateResult<byte[]> BuildReadCommand(string[] address, ushort[] length)
        {
            try
            {
                List<byte[]> list = new List<byte[]>();
                for (int i = 0; i < address.Length; i++)
                {
                    list.Add(AllenBradleyHelper.PackRequsetRead(address[i], length[i], true));
                }
                return OperateResult.CreateSuccessResult<byte[]>(this.PackCommandService(list.ToArray()));
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>("Address Wrong:" + exception.Message);
            }
        }

        private OperateResult<byte[]> BuildWriteCommand(string address, ushort typeCode, byte[] data, [Optional, DefaultParameterValue(1)] int length)
        {
            try
            {
                byte[][] cip = new byte[][] { AllenBradleyHelper.PackRequestWrite(address, typeCode, data, length, true) };
                return OperateResult.CreateSuccessResult<byte[]>(this.PackCommandService(cip));
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>("Address Wrong:" + exception.Message);
            }
        }

        public static OperateResult<byte[], ushort, bool> ExtractActualData(byte[] response, bool isRead)
        {
            List<byte> list = new List<byte>();
            int startIndex = 0x2a;
            bool flag = false;
            ushort num2 = 0;
            ushort num3 = BitConverter.ToUInt16(response, startIndex);
            if (BitConverter.ToInt32(response, 0x2e) == 0x8a)
            {
                startIndex = 50;
                int num4 = BitConverter.ToUInt16(response, startIndex);
                for (int i = 0; i < num4; i++)
                {
                    int num6 = BitConverter.ToUInt16(response, (startIndex + 2) + (i * 2)) + startIndex;
                    int num7 = (i == (num4 - 1)) ? response.Length : (BitConverter.ToUInt16(response, (startIndex + 4) + (i * 2)) + startIndex);
                    ushort num8 = BitConverter.ToUInt16(response, num6 + 2);
                    switch (num8)
                    {
                        case 0:
                            break;

                        case 4:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley04
                            };

                        case 5:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley05
                            };

                        case 6:
                            if ((response[startIndex + 2] != 210) && (response[startIndex + 2] != 0xcc))
                            {
                                break;
                            }
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley06
                            };

                        case 10:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley0A
                            };

                        case 0x13:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley13
                            };

                        case 0x1c:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley1C
                            };

                        case 30:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley1E
                            };

                        case 0x26:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley26
                            };

                        default:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.UnknownError
                            };
                    }
                    if (isRead)
                    {
                        for (int j = num6 + 6; j < num7; j++)
                        {
                            list.Add(response[j]);
                        }
                    }
                }
            }
            else
            {
                byte num12 = response[startIndex + 6];
                switch (num12)
                {
                    case 0:
                        break;

                    case 4:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley04
                        };

                    case 5:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley05
                        };

                    case 6:
                        flag = true;
                        break;

                    case 10:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley0A
                        };

                    case 0x13:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley13
                        };

                    case 0x1c:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley1C
                        };

                    case 30:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley1E
                        };

                    case 0x26:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley26
                        };

                    default:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.UnknownError
                        };
                }
                if ((response[startIndex + 4] == 0xcd) || (response[startIndex + 4] == 0xd3))
                {
                    return OperateResult.CreateSuccessResult<byte[], ushort, bool>(list.ToArray(), num2, flag);
                }
                if ((response[startIndex + 4] == 0xcc) || (response[startIndex + 4] == 210))
                {
                    for (int k = startIndex + 10; k < ((startIndex + 2) + num3); k++)
                    {
                        list.Add(response[k]);
                    }
                    num2 = BitConverter.ToUInt16(response, startIndex + 8);
                }
                else if (response[startIndex + 4] == 0xd5)
                {
                    for (int m = startIndex + 8; m < ((startIndex + 2) + num3); m++)
                    {
                        list.Add(response[m]);
                    }
                }
            }
            return OperateResult.CreateSuccessResult<byte[], ushort, bool>(list.ToArray(), num2, flag);
        }

        protected override OperateResult ExtraOnDisconnect(Socket socket)
        {
            OperateResult<byte[]> result = this.ReadFromCoreServer(socket, AllenBradleyHelper.UnRegisterSessionHandle(this.SessionHandle), true, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            return OperateResult.CreateSuccessResult();
        }

        private byte[] GetAttributeAll()
        {
            return "00 00 00 00 00 00 02 00 00 00 00 00 b2 00 06 00 01 02 20 01 24 01".ToHexBytes();
        }

        private byte[] GetLargeForwardOpen()
        {
            return "00 00 00 00 00 00 02 00 00 00 00 00 b2 00 34 00\r\n5b 02 20 06 24 01 06 9c 02 00 00 80 01 00 fe 80\r\n02 00 1b 05 30 a7 2b 03 02 00 00 00 80 84 1e 00\r\ncc 07 00 42 80 84 1e 00 cc 07 00 42 a3 03 20 02\r\n24 01 2c 01".ToHexBytes();
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new AllenBradleyMessage();
        }

        private byte[] GetOTConnectionIdService()
        {
            byte[] array = new byte[8];
            array[0] = 0xa1;
            array[1] = 0;
            array[2] = 4;
            array[3] = 0;
            base.ByteTransform.TransByte(this.OTConnectionId).CopyTo(array, 4);
            return array;
        }

        protected override OperateResult InitializationOnConnect(Socket socket)
        {
            OperateResult<byte[]> result = this.ReadFromCoreServer(socket, AllenBradleyHelper.RegisterSessionHandle(), true, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult result2 = AllenBradleyHelper.CheckResponse(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            this.SessionHandle = base.ByteTransform.TransUInt32(result.Content, 4);
            OperateResult<byte[]> result3 = this.ReadFromCoreServer(socket, AllenBradleyHelper.PackRequestHeader(0x6f, this.SessionHandle, this.GetLargeForwardOpen()), true, false);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            if (result3.Content[0x2a] > 0)
            {
                if (base.ByteTransform.TransUInt16(result3.Content, 0x2c) == 0x100)
                {
                    return new OperateResult("Connection in use or duplicate Forward Open");
                }
                return new OperateResult("Forward Open failed, Code: " + base.ByteTransform.TransUInt16(result3.Content, 0x2c).ToString());
            }
            this.OTConnectionId = base.ByteTransform.TransUInt32(result3.Content, 0x2c);
            this.incrementCount.ResetCurrentValue();
            OperateResult<byte[]> result4 = this.ReadFromCoreServer(socket, AllenBradleyHelper.PackRequestHeader(0x6f, this.SessionHandle, this.GetAttributeAll()), true, false);
            if (!result4.IsSuccess)
            {
                return result4;
            }
            if (result4.Content.Length > 0x3b)
            {
                this.ProductName = Encoding.UTF8.GetString(result4.Content, 0x3b, result4.Content[0x3a]);
            }
            return OperateResult.CreateSuccessResult();
        }

        private byte[] PackCommandService(params byte[][] cip)
        {
            MemoryStream stream = new MemoryStream();
            stream.WriteByte(0xb1);
            stream.WriteByte(0);
            stream.WriteByte(0);
            stream.WriteByte(0);
            long currentValue = this.incrementCount.GetCurrentValue();
            stream.WriteByte(BitConverter.GetBytes(currentValue)[0]);
            stream.WriteByte(BitConverter.GetBytes(currentValue)[1]);
            if (cip.Length == 1)
            {
                stream.Write(cip[0], 0, cip[0].Length);
            }
            else
            {
                stream.Write(new byte[] { 10, 2, 0x20, 2, 0x24, 1 }, 0, 6);
                stream.WriteByte(BitConverter.GetBytes(cip.Length)[0]);
                stream.WriteByte(BitConverter.GetBytes(cip.Length)[1]);
                int num2 = 2 + (cip.Length * 2);
                for (int i = 0; i < cip.Length; i++)
                {
                    stream.WriteByte(BitConverter.GetBytes(num2)[0]);
                    stream.WriteByte(BitConverter.GetBytes(num2)[1]);
                    num2 += cip[i].Length;
                }
                for (int j = 0; j < cip.Length; j++)
                {
                    stream.Write(cip[j], 0, cip[j].Length);
                }
            }
            byte[] array = stream.ToArray();
            stream.Dispose();
            BitConverter.GetBytes((ushort) (array.Length - 4)).CopyTo(array, 2);
            return array;
        }

        protected override byte[] PackCommandWithHeader(byte[] command)
        {
            byte[][] service = new byte[][] { this.GetOTConnectionIdService(), command };
            return AllenBradleyHelper.PackRequestHeader(0x70, this.SessionHandle, AllenBradleyHelper.PackCommandSpecificData(service));
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            string[] textArray1 = new string[] { address };
            ushort[] numArray1 = new ushort[] { length };
            OperateResult<byte[], ushort, bool> result = this.ReadWithType(textArray1, numArray1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result.Content1);
        }

        [HslMqttApi("ReadMultiAddress", "")]
        public OperateResult<byte[]> Read(string[] address, ushort[] length)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<byte[]>(StringResources.Language.InsufficientPrivileges);
            }
            OperateResult<byte[], ushort, bool> result = this.ReadWithType(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result.Content1);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            if ((length == 1) && !Regex.IsMatch(address, @"\[[0-9]+\]$"))
            {
                OperateResult<byte[]> result = this.Read(address, length);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<bool[]>(result);
                }
                return OperateResult.CreateSuccessResult<bool[]>(SoftBasic.ByteToBoolArray(result.Content));
            }
            OperateResult<byte[]> result3 = this.Read(address, length);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            return OperateResult.CreateSuccessResult<bool[]>((from m in result3.Content select m > 0).Take<bool>(length).ToArray<bool>());
        }

        [HslMqttApi("ReadByte", "")]
        public OperateResult<byte> ReadByte(string address)
        {
            return ByteTransformHelper.GetResultFromArray<byte>(this.Read(address, 1));
        }

        public OperateResult<byte[]> ReadCipFromServer(params byte[][] cips)
        {
            byte[] send = this.PackCommandService(cips.ToArray<byte[]>());
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult result2 = AllenBradleyHelper.CheckResponse(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result.Content);
        }

        [HslMqttApi("ReadDoubleArray", "")]
        public override OperateResult<double[]> ReadDouble(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<double[]>(this.Read(address, length), m => this.ByteTransform.TransDouble(m, 0, length));
        }

        [HslMqttApi("ReadFloatArray", "")]
        public override OperateResult<float[]> ReadFloat(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<float[]>(this.Read(address, length), m => this.ByteTransform.TransSingle(m, 0, length));
        }

        [HslMqttApi("ReadInt16Array", "")]
        public override OperateResult<short[]> ReadInt16(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<short[]>(this.Read(address, length), m => this.ByteTransform.TransInt16(m, 0, length));
        }

        [HslMqttApi("ReadInt32Array", "")]
        public override OperateResult<int[]> ReadInt32(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<int[]>(this.Read(address, length), m => this.ByteTransform.TransInt32(m, 0, length));
        }

        [HslMqttApi("ReadInt64Array", "")]
        public override OperateResult<long[]> ReadInt64(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<long[]>(this.Read(address, length), m => this.ByteTransform.TransInt64(m, 0, length));
        }

        public OperateResult<string> ReadString(string address)
        {
            return this.ReadString(address, 1, Encoding.UTF8);
        }

        [HslMqttApi("ReadString", "")]
        public override OperateResult<string> ReadString(string address, ushort length)
        {
            return this.ReadString(address, length, Encoding.UTF8);
        }

        public override OperateResult<string> ReadString(string address, ushort length, Encoding encoding)
        {
            OperateResult<byte[]> result = this.Read(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            if (result.Content.Length >= 2)
            {
                int count = base.ByteTransform.TransUInt16(result.Content, 0);
                return OperateResult.CreateSuccessResult<string>(encoding.GetString(result.Content, 2, count));
            }
            return OperateResult.CreateSuccessResult<string>(encoding.GetString(result.Content));
        }

        public OperateResult<T> ReadStruct<T>(string address) where T: struct
        {
            OperateResult<byte[]> result = this.Read(address, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<T>(result);
            }
            return HslHelper.ByteArrayToStruct<T>(result.Content.RemoveBegin<byte>(2));
        }

        [HslMqttApi("ReadUInt16Array", "")]
        public override OperateResult<ushort[]> ReadUInt16(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<ushort[]>(this.Read(address, length), m => this.ByteTransform.TransUInt16(m, 0, length));
        }

        [HslMqttApi("ReadUInt32Array", "")]
        public override OperateResult<uint[]> ReadUInt32(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<uint[]>(this.Read(address, length), m => this.ByteTransform.TransUInt32(m, 0, length));
        }

        [HslMqttApi("ReadUInt64Array", "")]
        public override OperateResult<ulong[]> ReadUInt64(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<ulong[]>(this.Read(address, length), m => this.ByteTransform.TransUInt64(m, 0, length));
        }

        private OperateResult<byte[], ushort, bool> ReadWithType(string[] address, ushort[] length)
        {
            OperateResult<byte[]> result = this.BuildReadCommand(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[], ushort, bool>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[], ushort, bool>(result2);
            }
            OperateResult result3 = AllenBradleyHelper.CheckResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[], ushort, bool>(result3);
            }
            return ExtractActualData(result2.Content, true);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            return new OperateResult(StringResources.Language.NotSupportedFunction + " Please refer to use WriteTag instead ");
        }

        [HslMqttApi("WriteDoubleArray", "")]
        public override OperateResult Write(string address, double[] values)
        {
            return this.WriteTag(address, 0xcb, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteInt16Array", "")]
        public override OperateResult Write(string address, short[] values)
        {
            return this.WriteTag(address, 0xc3, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteInt32Array", "")]
        public override OperateResult Write(string address, int[] values)
        {
            return this.WriteTag(address, 0xc4, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteInt64Array", "")]
        public override OperateResult Write(string address, long[] values)
        {
            return this.WriteTag(address, 0xc5, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteFloatArray", "")]
        public override OperateResult Write(string address, float[] values)
        {
            return this.WriteTag(address, 0xca, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteUInt16Array", "")]
        public override OperateResult Write(string address, ushort[] values)
        {
            return this.WriteTag(address, 0xc7, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteUInt32Array", "")]
        public override OperateResult Write(string address, uint[] values)
        {
            return this.WriteTag(address, 200, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteUInt64Array", "")]
        public override OperateResult Write(string address, ulong[] values)
        {
            return this.WriteTag(address, 0xc9, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            return this.WriteTag(address, 0xc1, value ? new byte[] { 0xff, 0xff } : new byte[2], 1);
        }

        [HslMqttApi("WriteByte", "")]
        public OperateResult Write(string address, byte value)
        {
            byte[] buffer1 = new byte[] { value };
            return this.WriteTag(address, 0xc2, buffer1, 1);
        }

        [HslMqttApi("WriteString", "")]
        public override OperateResult Write(string address, string value)
        {
            byte[] buffer = string.IsNullOrEmpty(value) ? new byte[0] : Encoding.UTF8.GetBytes(value);
            byte[][] bytes = new byte[][] { BitConverter.GetBytes((ushort) buffer.Length), buffer };
            return this.WriteTag(address, 0xd0, SoftBasic.SpliceByteArray(bytes), 1);
        }

        public virtual OperateResult WriteTag(string address, ushort typeCode, byte[] value, [Optional, DefaultParameterValue(1)] int length)
        {
            OperateResult<byte[]> result = this.BuildWriteCommand(address, typeCode, value, length);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = AllenBradleyHelper.CheckResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return AllenBradleyHelper.ExtractActualData(result2.Content, false);
        }

        public string ProductName { get; private set; }

        public uint SessionHandle { get; protected set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OmronConnectedCipNet.<>c <>9 = new OmronConnectedCipNet.<>c();
            public static Func<byte, bool> <>9__24_0;

            internal bool <ReadBool>b__24_0(byte m)
            {
                return (m > 0);
            }
        }
    }
}

