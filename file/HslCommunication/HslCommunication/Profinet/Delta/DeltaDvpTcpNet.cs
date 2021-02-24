namespace HslCommunication.Profinet.Delta
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.ModBus;
    using HslCommunication.Reflection;
    using System;
    using System.Runtime.InteropServices;

    public class DeltaDvpTcpNet : ModbusTcpNet
    {
        public DeltaDvpTcpNet()
        {
            base.ByteTransform.DataFormat = DataFormat.CDAB;
        }

        public DeltaDvpTcpNet(string ipAddress, [Optional, DefaultParameterValue(0x1f6)] int port, [Optional, DefaultParameterValue(1)] byte station) : base(ipAddress, port, station)
        {
            base.ByteTransform.DataFormat = DataFormat.CDAB;
        }

        [HslMqttApi("ReadByteArray", "Read the original byte data content from the register, the address is mainly D, T, C")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<string> result = DeltaHelper.PraseDeltaDvpAddress(address, 3);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return base.Read(result.Content, length);
        }

        [HslMqttApi("ReadBoolArray", "Read the contents of bool data in batches from the coil, the address is mainly X, Y, S, M, T, C")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<string> result = DeltaHelper.PraseDeltaDvpAddress(address, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.ReadBool(result.Content, length);
        }

        public override string ToString()
        {
            return string.Format("DeltaDvpTcpNet[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "Read the contents of bool data in batches from the coil, the address is mainly X, Y, S, M, T, C")]
        public override OperateResult Write(string address, bool[] values)
        {
            OperateResult<string> result = DeltaHelper.PraseDeltaDvpAddress(address, 15);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, values);
        }

        [HslMqttApi("WriteByteArray", "Write the original byte data content to the register, the address is mainly D, T, C")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<string> result = DeltaHelper.PraseDeltaDvpAddress(address, 0x10);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return base.Write(result.Content, value);
        }

        [HslMqttApi("WriteBool", "Write bool data content to the coil, the address is mainly Y, S, M, T, C")]
        public override OperateResult Write(string address, bool value)
        {
            OperateResult<string> result = DeltaHelper.PraseDeltaDvpAddress(address, 5);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, value);
        }

        [HslMqttApi("WriteInt16", "Write short data, returns whether success")]
        public override OperateResult Write(string address, short value)
        {
            OperateResult<string> result = DeltaHelper.PraseDeltaDvpAddress(address, 6);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, value);
        }

        [HslMqttApi("WriteUInt16", "Write ushort data, return whether the write was successful")]
        public override OperateResult Write(string address, ushort value)
        {
            OperateResult<string> result = DeltaHelper.PraseDeltaDvpAddress(address, 6);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, value);
        }
    }
}

