namespace HslCommunication.Profinet.Inovance
{
    using HslCommunication;
    using HslCommunication.ModBus;
    using HslCommunication.Reflection;
    using System;
    using System.Runtime.InteropServices;

    public class InovanceH3UTcp : ModbusTcpNet
    {
        public InovanceH3UTcp()
        {
        }

        public InovanceH3UTcp(string ipAddress, [Optional, DefaultParameterValue(0x1f6)] int port, [Optional, DefaultParameterValue(1)] byte station) : base(ipAddress, port, station)
        {
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceH3UAddress(address, 3);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return base.Read(result.Content, length);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceH3UAddress(address, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.ReadBool(result.Content, length);
        }

        public override string ToString()
        {
            return string.Format("InovanceH3UTcp[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] values)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceH3UAddress(address, 15);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, values);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceH3UAddress(address, 0x10);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return base.Write(result.Content, value);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceH3UAddress(address, 5);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, value);
        }

        [HslMqttApi("WriteInt16", "")]
        public override OperateResult Write(string address, short value)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceH3UAddress(address, 6);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, value);
        }

        [HslMqttApi("WriteUInt16", "")]
        public override OperateResult Write(string address, ushort value)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceH3UAddress(address, 6);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, value);
        }
    }
}

