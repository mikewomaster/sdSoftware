namespace HslCommunication.Profinet.LSIS
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class XGBCnet : SerialDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <Station>k__BackingField = 5;

        public XGBCnet()
        {
            base.ByteTransform = new RegularByteTransform();
            base.WordLength = 2;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = XGBCnetOverTcp.BuildReadCommand(station, address, length);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return XGBCnetOverTcp.ExtractActualData(result2.Content, true);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = XGBCnetOverTcp.BuildReadOneCommand(station, address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            return OperateResult.CreateSuccessResult<bool[]>(SoftBasic.ByteToBoolArray(XGBCnetOverTcp.ExtractActualData(result2.Content, true).Content, length));
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
            return string.Format("XGBCnet[{0}:{1}]", base.PortName, base.BaudRate);
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
            OperateResult<byte[]> result = XGBCnetOverTcp.BuildWriteCommand(station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return XGBCnetOverTcp.ExtractActualData(result2.Content, false);
        }

        public OperateResult WriteCoil(string address, bool value)
        {
            return this.Write(address, value);
        }

        public byte Station { get; set; }
    }
}

