namespace HslCommunication.Profinet.Fuji
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Core.Address;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;
    using System.Text;

    public class FujiSPB : SerialDeviceBase
    {
        private byte station = 1;

        public FujiSPB()
        {
            base.ByteTransform = new RegularByteTransform();
            base.WordLength = 1;
            base.LogMsgFormatBinary = false;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = FujiSPBOverTcp.BuildReadCommand(this.station, address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult<byte[]> result3 = FujiSPBOverTcp.CheckResponseData(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetString(result3.Content.RemoveBegin<byte>(4)).ToHexBytes());
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            OperateResult<FujiSPBAddress> result = FujiSPBAddress.ParseFrom(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            if (((((address.StartsWith("X") || address.StartsWith("Y")) || (address.StartsWith("M") || address.StartsWith("L"))) || address.StartsWith("TC")) || address.StartsWith("CC")) && (address.IndexOf('.') < 0))
            {
                result.Content.BitIndex = result.Content.Address % 0x10;
                result.Content.Address = (ushort) (result.Content.Address / 0x10);
            }
            ushort num2 = (ushort) (((((result.Content.GetBitIndex() + length) - 1) / 0x10) - (result.Content.GetBitIndex() / 0x10)) + 1);
            OperateResult<byte[]> result2 = FujiSPBOverTcp.BuildReadCommand(station, result.Content, num2);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult<byte[]> result3 = base.ReadBase(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            OperateResult<byte[]> result4 = FujiSPBOverTcp.CheckResponseData(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result4);
            }
            return OperateResult.CreateSuccessResult<bool[]>(Encoding.ASCII.GetString(result4.Content.RemoveBegin<byte>(4)).ToHexBytes().ToBoolArray().SelectMiddle<bool>(result.Content.BitIndex, length));
        }

        public override string ToString()
        {
            return string.Format("FujiSPB[{0}:{1}]", base.PortName, base.BaudRate);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result = FujiSPBOverTcp.BuildWriteByteCommand(this.station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return FujiSPBOverTcp.CheckResponseData(result2.Content);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            OperateResult<byte[]> result = FujiSPBOverTcp.BuildWriteBoolCommand(this.station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return FujiSPBOverTcp.CheckResponseData(result2.Content);
        }

        public byte Station
        {
            get
            {
                return this.station;
            }
            set
            {
                this.station = value;
            }
        }
    }
}

