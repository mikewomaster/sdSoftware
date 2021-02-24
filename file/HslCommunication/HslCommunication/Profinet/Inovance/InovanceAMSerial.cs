namespace HslCommunication.Profinet.Inovance
{
    using HslCommunication;
    using HslCommunication.ModBus;
    using HslCommunication.Reflection;
    using System;
    using System.Runtime.InteropServices;

    public class InovanceAMSerial : ModbusRtu
    {
        public InovanceAMSerial()
        {
        }

        public InovanceAMSerial([Optional, DefaultParameterValue(1)] byte station) : base(station)
        {
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceAMAddress(address, 3);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return base.Read(result.Content, length);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceAMAddress(address, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.ReadBool(result.Content, length);
        }

        public override string ToString()
        {
            return string.Format("InovanceAMSerial[{0}:{1}]", base.PortName, base.BaudRate);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] values)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceAMAddress(address, 15);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, values);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceAMAddress(address, 0x10);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return base.Write(result.Content, value);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceAMAddress(address, 5);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, value);
        }

        [HslMqttApi("WriteInt16", "")]
        public override OperateResult Write(string address, short value)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceAMAddress(address, 6);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, value);
        }

        [HslMqttApi("WriteUInt16", "")]
        public override OperateResult Write(string address, ushort value)
        {
            OperateResult<string> result = InovanceHelper.PraseInovanceAMAddress(address, 6);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, value);
        }
    }
}

