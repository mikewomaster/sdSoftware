namespace HslCommunication.Profinet.GE
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Address;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;

    public class GeSRTPNet : NetworkDeviceBase
    {
        private SoftIncrementCount incrementCount;

        public GeSRTPNet()
        {
            this.incrementCount = new SoftIncrementCount(0xffffL, 0L, 1);
            base.ByteTransform = new RegularByteTransform();
            base.WordLength = 2;
        }

        public GeSRTPNet(string ipAddress, [Optional, DefaultParameterValue(0x4745)] int port)
        {
            this.incrementCount = new SoftIncrementCount(0xffffL, 0L, 1);
            base.ByteTransform = new RegularByteTransform();
            this.IpAddress = ipAddress;
            this.Port = port;
            base.WordLength = 2;
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new GeSRTPMessage();
        }

        protected override OperateResult InitializationOnConnect(Socket socket)
        {
            OperateResult<byte[]> result = this.ReadFromCoreServer(socket, new byte[0x38], true, true);
            if (!result.IsSuccess)
            {
                return result;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = GeHelper.BuildReadCommand(this.incrementCount.GetCurrentValue(), address, length, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return GeHelper.ExtraResponseContent(result2.Content);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<GeSRTPAddress> result = GeSRTPAddress.ParseFrom(address, length, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = GeHelper.BuildReadCommand(this.incrementCount.GetCurrentValue(), result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult<byte[]> result3 = base.ReadFromCoreServer(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            OperateResult<byte[]> result4 = GeHelper.ExtraResponseContent(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result4);
            }
            return OperateResult.CreateSuccessResult<bool[]>(result4.Content.ToBoolArray().SelectMiddle<bool>(result.Content.AddressStart % 8, length));
        }

        [HslMqttApi("ReadByte", "")]
        public OperateResult<byte> ReadByte(string address)
        {
            OperateResult<GeSRTPAddress> result = GeSRTPAddress.ParseFrom(address, 1, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte>(result);
            }
            if (((result.Content.DataCode == 10) || (result.Content.DataCode == 12)) || (result.Content.DataCode == 8))
            {
                return new OperateResult<byte>(StringResources.Language.GeSRTPNotSupportByteReadWrite);
            }
            return ByteTransformHelper.GetResultFromArray<byte>(this.Read(address, 1));
        }

        [HslMqttApi(Description="Read the current time of the PLC")]
        public OperateResult<DateTime> ReadPLCTime()
        {
            byte[] data = new byte[5];
            data[3] = 2;
            OperateResult<byte[]> result = GeHelper.BuildReadCoreCommand(this.incrementCount.GetCurrentValue(), 0x25, data);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<DateTime>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<DateTime>(result2);
            }
            OperateResult<byte[]> result3 = GeHelper.ExtraResponseContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<DateTime>(result3);
            }
            return GeHelper.ExtraDateTime(result3.Content);
        }

        [HslMqttApi(Description="Read the name of the current program of the PLC")]
        public OperateResult<string> ReadProgramName()
        {
            byte[] data = new byte[5];
            data[3] = 2;
            OperateResult<byte[]> result = GeHelper.BuildReadCoreCommand(this.incrementCount.GetCurrentValue(), 1, data);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            OperateResult<byte[]> result3 = GeHelper.ExtraResponseContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result3);
            }
            return GeHelper.ExtraProgramName(result3.Content);
        }

        public override string ToString()
        {
            return string.Format("GeSRTPNet[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result = GeHelper.BuildWriteCommand(this.incrementCount.GetCurrentValue(), address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return GeHelper.ExtraResponseContent(result2.Content);
        }

        [HslMqttApi("WriteByte", "")]
        public OperateResult Write(string address, byte value)
        {
            OperateResult<GeSRTPAddress> result = GeSRTPAddress.ParseFrom(address, 1, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte>(result);
            }
            if (((result.Content.DataCode == 10) || (result.Content.DataCode == 12)) || (result.Content.DataCode == 8))
            {
                return new OperateResult<byte>(StringResources.Language.GeSRTPNotSupportByteReadWrite);
            }
            byte[] buffer1 = new byte[] { value };
            return this.Write(address, buffer1);
        }

        [HslMqttApi(ApiTopic="WriteBoolArray", Description="In units of bits, write bool arrays in batches to the specified addresses")]
        public override OperateResult Write(string address, bool[] value)
        {
            OperateResult<byte[]> result = GeHelper.BuildWriteCommand(this.incrementCount.GetCurrentValue(), address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return GeHelper.ExtraResponseContent(result2.Content);
        }
    }
}

