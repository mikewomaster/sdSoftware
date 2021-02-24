namespace HslCommunication.Profinet.XINJE
{
    using HslCommunication;
    using HslCommunication.ModBus;
    using HslCommunication.Reflection;
    using System;
    using System.Runtime.InteropServices;

    public class XinJEXCSerial : ModbusRtu
    {
        public XinJEXCSerial()
        {
        }

        public XinJEXCSerial([Optional, DefaultParameterValue(1)] byte station) : base(station)
        {
        }

        [HslMqttApi("ReadByteArray", "Read the original byte data content from the register, the address is mainly D, F, E, T, C")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<string> result = XinJEHelper.PraseXinJEXCAddress(address, 3);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return base.Read(result.Content, length);
        }

        [HslMqttApi("ReadBoolArray", "Read the contents of bool data in batches from the coil, the address is mainly X, Y, S, M, T, C")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<string> result = XinJEHelper.PraseXinJEXCAddress(address, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.ReadBool(result.Content, length);
        }

        public override string ToString()
        {
            return string.Format("XinJEXCSerial[{0}:{1}]", base.PortName, base.BaudRate);
        }

        [HslMqttApi("WriteBoolArray", "Read the contents of bool data in batches from the coil, the address is mainly X, Y, S, M, T, C")]
        public override OperateResult Write(string address, bool[] values)
        {
            OperateResult<string> result = XinJEHelper.PraseXinJEXCAddress(address, 15);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, values);
        }

        [HslMqttApi("WriteByteArray", "Write the original byte data content to the register, the address is mainly D, F, E, T, C")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<string> result = XinJEHelper.PraseXinJEXCAddress(address, 0x10);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return base.Write(result.Content, value);
        }

        [HslMqttApi("WriteBool", "Write bool data content to the coil, the address is mainly X, Y, S, M, T, C")]
        public override OperateResult Write(string address, bool value)
        {
            OperateResult<string> result = XinJEHelper.PraseXinJEXCAddress(address, 5);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, value);
        }

        [HslMqttApi("WriteInt16", "Write short data, returns whether success")]
        public override OperateResult Write(string address, short value)
        {
            OperateResult<string> result = XinJEHelper.PraseXinJEXCAddress(address, 6);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, value);
        }

        [HslMqttApi("WriteUInt16", "Write ushort data, return whether the write was successful")]
        public override OperateResult Write(string address, ushort value)
        {
            OperateResult<string> result = XinJEHelper.PraseXinJEXCAddress(address, 6);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return base.Write(result.Content, value);
        }
    }
}

