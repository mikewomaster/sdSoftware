namespace HslCommunication.Profinet.Melsec
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;

    public class MelsecA3CNet1 : SerialDeviceBase
    {
        private byte station = 0;

        public MelsecA3CNet1()
        {
            base.ByteTransform = new RegularByteTransform();
            base.WordLength = 1;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            return MelsecA3CNet1OverTcp.ReadHelper(address, length, station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            return MelsecA3CNet1OverTcp.ReadBoolHelper(address, length, station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
        }

        [HslMqttApi]
        public OperateResult<string> ReadPlcType()
        {
            return MelsecA3CNet1OverTcp.ReadPlcTypeHelper(this.station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
        }

        private OperateResult<byte[]> ReadWithPackCommand(byte[] command, byte station)
        {
            return base.ReadBase(MelsecA3CNet1OverTcp.PackCommand(command, station));
        }

        [HslMqttApi]
        public OperateResult RemoteRun()
        {
            return MelsecA3CNet1OverTcp.RemoteRunHelper(this.station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
        }

        [HslMqttApi]
        public OperateResult RemoteStop()
        {
            return MelsecA3CNet1OverTcp.RemoteStopHelper(this.station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
        }

        public override string ToString()
        {
            return string.Format("MelsecA3CNet1[{0}:{1}]", base.PortName, base.BaudRate);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            return MelsecA3CNet1OverTcp.WriteHelper(address, value, station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            return MelsecA3CNet1OverTcp.WriteHelper(address, value, station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
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

