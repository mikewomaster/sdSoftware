namespace HslCommunication.Instrument.Temperature
{
    using HslCommunication;
    using HslCommunication.ModBus;
    using System;
    using System.Linq;

    public class DAM3601 : ModbusRtu
    {
        public DAM3601()
        {
            base.SleepTime = 200;
        }

        public DAM3601(byte station) : base(station)
        {
            base.SleepTime = 200;
        }

        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = ModbusInfo.BuildReadModbusCommand(address, length, base.Station, base.AddressStartWithZero, 0x10);
            if (!result.IsSuccess)
            {
                return result;
            }
            return this.CheckModbusTcpResponse(result.Content);
        }

        public OperateResult<float[]> ReadAllTemperature()
        {
            string address = "x=4;1";
            if (base.AddressStartWithZero)
            {
                address = "x=4;0";
            }
            OperateResult<short[]> result = this.ReadInt16(address, 0x80);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<float[]>(result);
            }
            return OperateResult.CreateSuccessResult<float[]>((from m in result.Content select this.TransformValue(m)).ToArray<float>());
        }

        public override string ToString()
        {
            return string.Format("DAM3601[{0}:{1}]", base.PortName, base.BaudRate);
        }

        private float TransformValue(short value)
        {
            if ((value & 0x800) > 0)
            {
                return ((((value & 0xfff) ^ 0xfff) + 1) * -0.0625f);
            }
            return ((value & 0x7ff) * 0.0625f);
        }
    }
}

