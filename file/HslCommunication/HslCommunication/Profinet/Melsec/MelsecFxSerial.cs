namespace HslCommunication.Profinet.Melsec
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class MelsecFxSerial : SerialDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsNewVersion>k__BackingField;

        public MelsecFxSerial()
        {
            base.ByteTransform = new RegularByteTransform();
            base.WordLength = 1;
            this.IsNewVersion = true;
            base.ByteTransform.IsStringReverseByteWord = true;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            return MelsecFxSerialOverTcp.ReadHelper(address, length, new Func<byte[], OperateResult<byte[]>>(this.ReadBase), this.IsNewVersion);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            return MelsecFxSerialOverTcp.ReadBoolHelper(address, length, new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        public override string ToString()
        {
            return string.Format("MelsecFxSerial[{0}:{1}]", base.PortName, base.BaudRate);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            return MelsecFxSerialOverTcp.WriteHelper(address, value, new Func<byte[], OperateResult<byte[]>>(this.ReadBase), this.IsNewVersion);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            return MelsecFxSerialOverTcp.WriteHelper(address, value, new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        public bool IsNewVersion { get; set; }
    }
}

