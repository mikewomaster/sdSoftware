namespace HslCommunication.Profinet.Beckhoff
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class BeckhoffAdsNet : NetworkDeviceBase
    {
        private readonly SoftIncrementCount incrementCount;
        private string senderAMSNetId;
        private byte[] sourceAMSNetId;
        private readonly Dictionary<string, uint> tagCaches;
        private readonly object tagLock;
        private byte[] targetAMSNetId;
        private bool useTagCache;

        public BeckhoffAdsNet()
        {
            this.targetAMSNetId = new byte[8];
            this.sourceAMSNetId = new byte[8];
            this.senderAMSNetId = string.Empty;
            this.useTagCache = false;
            this.tagCaches = new Dictionary<string, uint>();
            this.tagLock = new object();
            this.incrementCount = new SoftIncrementCount(0x7fffffffL, 1L, 1);
            base.WordLength = 2;
            this.targetAMSNetId[4] = 1;
            this.targetAMSNetId[5] = 1;
            this.targetAMSNetId[6] = 0x21;
            this.targetAMSNetId[7] = 3;
            this.sourceAMSNetId[4] = 1;
            this.sourceAMSNetId[5] = 1;
            base.ByteTransform = new RegularByteTransform();
        }

        public BeckhoffAdsNet(string ipAddress, int port)
        {
            this.targetAMSNetId = new byte[8];
            this.sourceAMSNetId = new byte[8];
            this.senderAMSNetId = string.Empty;
            this.useTagCache = false;
            this.tagCaches = new Dictionary<string, uint>();
            this.tagLock = new object();
            this.incrementCount = new SoftIncrementCount(0x7fffffffL, 1L, 1);
            this.IpAddress = ipAddress;
            this.Port = port;
            base.WordLength = 2;
            this.targetAMSNetId[4] = 1;
            this.targetAMSNetId[5] = 1;
            this.targetAMSNetId[6] = 0x21;
            this.targetAMSNetId[7] = 3;
            this.sourceAMSNetId[4] = 1;
            this.sourceAMSNetId[5] = 1;
            base.ByteTransform = new RegularByteTransform();
        }

        public static OperateResult<uint, uint> AnalysisAddress(string address, bool isBit)
        {
            OperateResult<uint, uint> result = new OperateResult<uint, uint>();
            try
            {
                if (address.StartsWith("i="))
                {
                    result.Content1 = 0xf005;
                    result.Content2 = uint.Parse(address.Substring(2));
                }
                else if (address.StartsWith("s="))
                {
                    result.Content1 = 0xf003;
                    result.Content2 = 0;
                }
                else
                {
                    switch (address[0])
                    {
                        case 'i':
                        case 'I':
                            if (isBit)
                            {
                                result.Content1 = 0xf021;
                            }
                            else
                            {
                                result.Content1 = 0xf020;
                            }
                            break;

                        case 'm':
                        case 'M':
                            if (isBit)
                            {
                                result.Content1 = 0x4021;
                            }
                            else
                            {
                                result.Content1 = 0x4020;
                            }
                            break;

                        case 'q':
                        case 'Q':
                            if (isBit)
                            {
                                result.Content1 = 0xf031;
                            }
                            else
                            {
                                result.Content1 = 0xf030;
                            }
                            break;

                        default:
                            throw new Exception(StringResources.Language.NotSupportedDataType);
                    }
                    result.Content2 = uint.Parse(address.Substring(1));
                }
            }
            catch (Exception exception)
            {
                result.Message = exception.Message;
                return result;
            }
            result.IsSuccess = true;
            result.Message = StringResources.Language.SuccessText;
            return result;
        }

        public byte[] BuildAmsHeaderCommand(ushort commandId, byte[] data)
        {
            if (data == null)
            {
                data = new byte[0];
            }
            uint currentValue = (uint) this.incrementCount.GetCurrentValue();
            byte[] array = new byte[0x20 + data.Length];
            this.targetAMSNetId.CopyTo(array, 0);
            this.sourceAMSNetId.CopyTo(array, 8);
            array[0x10] = BitConverter.GetBytes(commandId)[0];
            array[0x11] = BitConverter.GetBytes(commandId)[1];
            array[0x12] = 4;
            array[0x13] = 0;
            array[20] = BitConverter.GetBytes(data.Length)[0];
            array[0x15] = BitConverter.GetBytes(data.Length)[1];
            array[0x16] = BitConverter.GetBytes(data.Length)[2];
            array[0x17] = BitConverter.GetBytes(data.Length)[3];
            array[0x18] = 0;
            array[0x19] = 0;
            array[0x1a] = 0;
            array[0x1b] = 0;
            array[0x1c] = BitConverter.GetBytes(currentValue)[0];
            array[0x1d] = BitConverter.GetBytes(currentValue)[1];
            array[30] = BitConverter.GetBytes(currentValue)[2];
            array[0x1f] = BitConverter.GetBytes(currentValue)[3];
            data.CopyTo(array, 0x20);
            return PackAmsTcpHelper(array);
        }

        public OperateResult<byte[]> BuildReadCommand(string address, int length, bool isBit)
        {
            OperateResult<uint, uint> result = AnalysisAddress(address, isBit);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] array = new byte[12];
            BitConverter.GetBytes(result.Content1).CopyTo(array, 0);
            BitConverter.GetBytes(result.Content2).CopyTo(array, 4);
            BitConverter.GetBytes(length).CopyTo(array, 8);
            return OperateResult.CreateSuccessResult<byte[]>(this.BuildAmsHeaderCommand(2, array));
        }

        public OperateResult<byte[]> BuildReadDeviceInfoCommand()
        {
            return OperateResult.CreateSuccessResult<byte[]>(this.BuildAmsHeaderCommand(1, null));
        }

        public OperateResult<byte[]> BuildReadStateCommand()
        {
            return OperateResult.CreateSuccessResult<byte[]>(this.BuildAmsHeaderCommand(4, null));
        }

        public OperateResult<byte[]> BuildReadWriteCommand(string address, int length, bool isBit, byte[] value)
        {
            OperateResult<uint, uint> result = AnalysisAddress(address, isBit);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] array = new byte[0x10 + value.Length];
            BitConverter.GetBytes(result.Content1).CopyTo(array, 0);
            BitConverter.GetBytes(result.Content2).CopyTo(array, 4);
            BitConverter.GetBytes(length).CopyTo(array, 8);
            BitConverter.GetBytes(value.Length).CopyTo(array, 12);
            value.CopyTo(array, 0x10);
            return OperateResult.CreateSuccessResult<byte[]>(this.BuildAmsHeaderCommand(9, array));
        }

        public OperateResult<byte[]> BuildReleaseSystemHandle(uint handle)
        {
            byte[] array = new byte[0x10];
            BitConverter.GetBytes(0xf006).CopyTo(array, 0);
            BitConverter.GetBytes(4).CopyTo(array, 8);
            BitConverter.GetBytes(handle).CopyTo(array, 12);
            return OperateResult.CreateSuccessResult<byte[]>(this.BuildAmsHeaderCommand(3, array));
        }

        public OperateResult<byte[]> BuildWriteCommand(string address, bool[] value, bool isBit)
        {
            OperateResult<uint, uint> result = AnalysisAddress(address, isBit);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] buffer = SoftBasic.BoolArrayToByte(value);
            byte[] array = new byte[12 + buffer.Length];
            BitConverter.GetBytes(result.Content1).CopyTo(array, 0);
            BitConverter.GetBytes(result.Content2).CopyTo(array, 4);
            BitConverter.GetBytes(buffer.Length).CopyTo(array, 8);
            buffer.CopyTo(array, 12);
            return OperateResult.CreateSuccessResult<byte[]>(this.BuildAmsHeaderCommand(3, array));
        }

        public OperateResult<byte[]> BuildWriteCommand(string address, byte[] value, bool isBit)
        {
            OperateResult<uint, uint> result = AnalysisAddress(address, isBit);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] array = new byte[12 + value.Length];
            BitConverter.GetBytes(result.Content1).CopyTo(array, 0);
            BitConverter.GetBytes(result.Content2).CopyTo(array, 4);
            BitConverter.GetBytes(value.Length).CopyTo(array, 8);
            value.CopyTo(array, 12);
            return OperateResult.CreateSuccessResult<byte[]>(this.BuildAmsHeaderCommand(3, array));
        }

        public OperateResult<byte[]> BuildWriteControlCommand(short state, short deviceState, byte[] data)
        {
            if (data == null)
            {
                data = new byte[0];
            }
            byte[] buffer = new byte[8 + data.Length];
            byte[][] bytes = new byte[][] { BitConverter.GetBytes(state), BitConverter.GetBytes(deviceState), BitConverter.GetBytes(data.Length), data };
            return OperateResult.CreateSuccessResult<byte[]>(this.BuildAmsHeaderCommand(5, SoftBasic.SpliceByteArray(bytes)));
        }

        public static OperateResult CheckResponse(byte[] response)
        {
            try
            {
                int err = BitConverter.ToInt32(response, 0x26);
                if (err > 0)
                {
                    return new OperateResult(err, StringResources.Language.UnknownError + " Source:" + response.ToHexString(' '));
                }
            }
            catch (Exception exception)
            {
                return new OperateResult(exception.Message + " Source:" + response.ToHexString(' '));
            }
            return OperateResult.CreateSuccessResult();
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new AdsNetMessage();
        }

        protected override OperateResult InitializationOnConnect(Socket socket)
        {
            if (string.IsNullOrEmpty(this.senderAMSNetId))
            {
                IPEndPoint localEndPoint = (IPEndPoint) socket.LocalEndPoint;
                this.sourceAMSNetId[6] = BitConverter.GetBytes(localEndPoint.Port)[0];
                this.sourceAMSNetId[7] = BitConverter.GetBytes(localEndPoint.Port)[1];
                localEndPoint.Address.GetAddressBytes().CopyTo(this.sourceAMSNetId, 0);
            }
            return base.InitializationOnConnect(socket);
        }

        public static byte[] PackAmsTcpHelper(byte[] command)
        {
            byte[] array = new byte[6 + command.Length];
            BitConverter.GetBytes(command.Length).CopyTo(array, 2);
            command.CopyTo(array, 6);
            return array;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<string> result = this.TransValueHandle(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            address = result.Content;
            OperateResult<byte[]> result2 = this.BuildReadCommand(address, length, false);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = base.ReadFromCoreServer(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            OperateResult result4 = CheckResponse(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result4);
            }
            return OperateResult.CreateSuccessResult<byte[]>(SoftBasic.ArrayRemoveBegin<byte>(result3.Content, 0x2e));
        }

        [HslMqttApi("ReadAdsDeviceInfo", "读取Ads设备的设备信息。主要是版本号，设备名称")]
        public OperateResult<AdsDeviceInfo> ReadAdsDeviceInfo()
        {
            OperateResult<byte[]> result = this.BuildReadDeviceInfoCommand();
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<AdsDeviceInfo>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<AdsDeviceInfo>(result2);
            }
            OperateResult result3 = CheckResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<AdsDeviceInfo>(result3);
            }
            return OperateResult.CreateSuccessResult<AdsDeviceInfo>(new AdsDeviceInfo(result2.Content.RemoveBegin<byte>(0x2a)));
        }

        public OperateResult<ushort, ushort> ReadAdsState()
        {
            OperateResult<byte[]> result = this.BuildReadStateCommand();
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<ushort, ushort>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<ushort, ushort>(result2);
            }
            OperateResult result3 = CheckResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<ushort, ushort>(result3);
            }
            return OperateResult.CreateSuccessResult<ushort, ushort>(BitConverter.ToUInt16(result2.Content, 0x2a), BitConverter.ToUInt16(result2.Content, 0x2c));
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<string> result = this.TransValueHandle(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            address = result.Content;
            OperateResult<byte[]> result2 = this.BuildReadCommand(address, length, true);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult<byte[]> result3 = base.ReadFromCoreServer(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            OperateResult result4 = CheckResponse(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result4);
            }
            return OperateResult.CreateSuccessResult<bool[]>(SoftBasic.ByteToBoolArray(SoftBasic.ArrayRemoveBegin<byte>(result3.Content, 0x2e)));
        }

        [HslMqttApi("ReadByte", "")]
        public OperateResult<byte> ReadByte(string address)
        {
            return ByteTransformHelper.GetResultFromArray<byte>(this.Read(address, 1));
        }

        public OperateResult<uint> ReadValueHandle(string address)
        {
            if (!address.StartsWith("s="))
            {
                return new OperateResult<uint>(StringResources.Language.SAMAddressStartWrong);
            }
            OperateResult<byte[]> result = this.BuildReadWriteCommand(address, 4, false, StrToAdsBytes(address.Substring(2)));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<uint>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<uint>(result2);
            }
            OperateResult result3 = CheckResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<uint>(result3);
            }
            return OperateResult.CreateSuccessResult<uint>(BitConverter.ToUInt32(result2.Content, 0x2e));
        }

        public OperateResult ReleaseSystemHandle(uint handle)
        {
            OperateResult<byte[]> result = this.BuildReleaseSystemHandle(handle);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = CheckResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        public void SetSenderAMSNetId(string amsNetId)
        {
            if (!string.IsNullOrEmpty(amsNetId))
            {
                StrToAMSNetId(amsNetId).CopyTo(this.sourceAMSNetId, 0);
                this.senderAMSNetId = amsNetId;
            }
        }

        public void SetTargetAMSNetId(string amsNetId)
        {
            if (!string.IsNullOrEmpty(amsNetId))
            {
                StrToAMSNetId(amsNetId).CopyTo(this.targetAMSNetId, 0);
            }
        }

        public static byte[] StrToAdsBytes(string value)
        {
            return SoftBasic.SpliceTwoByteArray(Encoding.ASCII.GetBytes(value), new byte[1]);
        }

        public static byte[] StrToAMSNetId(string amsNetId)
        {
            byte[] buffer;
            string str = amsNetId;
            if (amsNetId.IndexOf(':') > 0)
            {
                buffer = new byte[8];
                char[] chArray1 = new char[] { ':' };
                string[] strArray2 = amsNetId.Split(chArray1, StringSplitOptions.RemoveEmptyEntries);
                str = strArray2[0];
                buffer[6] = BitConverter.GetBytes(int.Parse(strArray2[1]))[0];
                buffer[7] = BitConverter.GetBytes(int.Parse(strArray2[1]))[1];
            }
            else
            {
                buffer = new byte[6];
            }
            char[] separator = new char[] { '.' };
            string[] strArray = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                buffer[i] = byte.Parse(strArray[i]);
            }
            return buffer;
        }

        public override string ToString()
        {
            return string.Format("BeckhoffAdsNet[{0}:{1}]", this.IpAddress, this.Port);
        }

        public OperateResult<string> TransValueHandle(string address)
        {
            if (address.StartsWith("s="))
            {
                if (this.useTagCache)
                {
                    object tagLock = this.tagLock;
                    lock (tagLock)
                    {
                        if (this.tagCaches.ContainsKey(address))
                        {
                            return OperateResult.CreateSuccessResult<string>(string.Format("i={0}", this.tagCaches[address]));
                        }
                    }
                }
                OperateResult<uint> result = this.ReadValueHandle(address);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<string>(result);
                }
                if (this.useTagCache)
                {
                    object obj3 = this.tagLock;
                    lock (obj3)
                    {
                        if (!this.tagCaches.ContainsKey(address))
                        {
                            this.tagCaches.Add(address, result.Content);
                        }
                    }
                }
                return OperateResult.CreateSuccessResult<string>(string.Format("i={0}", result.Content));
            }
            return OperateResult.CreateSuccessResult<string>(address);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] value)
        {
            OperateResult<string> result = this.TransValueHandle(address);
            if (!result.IsSuccess)
            {
                return result;
            }
            address = result.Content;
            OperateResult<byte[]> result2 = this.BuildWriteCommand(address, value, true);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = base.ReadFromCoreServer(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            OperateResult result4 = CheckResponse(result3.Content);
            if (!result4.IsSuccess)
            {
                return result4;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<string> result = this.TransValueHandle(address);
            if (!result.IsSuccess)
            {
                return result;
            }
            address = result.Content;
            OperateResult<byte[]> result2 = this.BuildWriteCommand(address, value, false);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = base.ReadFromCoreServer(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            OperateResult result4 = CheckResponse(result3.Content);
            if (!result4.IsSuccess)
            {
                return result4;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("WriteByte", "")]
        public OperateResult Write(string address, byte value)
        {
            byte[] buffer1 = new byte[] { value };
            return this.Write(address, buffer1);
        }

        public OperateResult WriteAdsState(short state, short deviceState, byte[] data)
        {
            OperateResult<byte[]> result = this.BuildWriteControlCommand(state, deviceState, data);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = CheckResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi(HttpMethod="GET", Description="Get or set the IP address of the remote server. If it is a local test, then it needs to be set to 127.0.0.1")]
        public override string IpAddress
        {
            get
            {
                return base.IpAddress;
            }
            set
            {
                base.IpAddress = value;
                char[] separator = new char[] { '.' };
                string[] strArray = base.IpAddress.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    this.targetAMSNetId[i] = byte.Parse(strArray[i]);
                }
            }
        }

        public bool UseTagCache
        {
            get
            {
                return this.useTagCache;
            }
            set
            {
                this.useTagCache = value;
            }
        }
    }
}

