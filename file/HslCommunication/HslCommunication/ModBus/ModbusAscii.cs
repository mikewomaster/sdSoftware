namespace HslCommunication.ModBus
{
    using HslCommunication;
    using System;
    using System.Runtime.InteropServices;

    public class ModbusAscii : ModbusRtu
    {
        public ModbusAscii()
        {
            base.LogMsgFormatBinary = false;
        }

        public ModbusAscii([Optional, DefaultParameterValue(1)] byte station) : base(station)
        {
            base.LogMsgFormatBinary = false;
        }

        protected override OperateResult<byte[]> CheckModbusTcpResponse(byte[] send)
        {
            byte[] buffer = ModbusInfo.TransModbusCoreToAsciiPackCommand(send);
            OperateResult<byte[]> result = base.ReadBase(buffer);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = ModbusInfo.TransAsciiPackCommandToCore(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            if ((send[1] + 0x80) == result2.Content[1])
            {
                return new OperateResult<byte[]>(result2.Content[2], ModbusInfo.GetDescriptionByErrorCode(result2.Content[2]));
            }
            return ModbusInfo.ExtractActualData(result2.Content);
        }

        public override string ToString()
        {
            return string.Format("ModbusAscii[{0}:{1}]", base.PortName, base.BaudRate);
        }
    }
}

