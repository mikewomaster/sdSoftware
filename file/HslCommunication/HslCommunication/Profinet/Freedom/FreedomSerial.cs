namespace HslCommunication.Profinet.Freedom
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;

    public class FreedomSerial : SerialDeviceBase
    {
        public FreedomSerial()
        {
            base.ByteTransform = new RegularByteTransform();
        }

        [HslMqttApi("ReadByteArray", "特殊的地址格式，需要采用解析包起始地址的报文，例如 modbus 协议为 stx=9;00 00 00 00 00 06 01 03 00 64 00 01")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[], int> result = FreedomTcpNet.AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content1);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            if (result.Content2 >= result2.Content.Length)
            {
                return new OperateResult<byte[]>(StringResources.Language.ReceiveDataLengthTooShort);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result2.Content.RemoveBegin<byte>(result.Content2));
        }

        public override string ToString()
        {
            return string.Format("FreedomSerial<{0}>[{1}:{2}]", base.ByteTransform.GetType(), base.PortName, base.BaudRate);
        }

        public override OperateResult Write(string address, byte[] value)
        {
            return this.Read(address, 0);
        }
    }
}

