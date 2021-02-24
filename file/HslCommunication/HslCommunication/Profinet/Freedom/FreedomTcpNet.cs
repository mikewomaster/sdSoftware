namespace HslCommunication.Profinet.Freedom
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;

    public class FreedomTcpNet : NetworkDeviceBase
    {
        public FreedomTcpNet()
        {
            base.ByteTransform = new RegularByteTransform();
        }

        public FreedomTcpNet(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
        }

        public static OperateResult<byte[], int> AnalysisAddress(string address)
        {
            try
            {
                int num = 0;
                byte[] buffer = null;
                if (address.IndexOf(';') > 0)
                {
                    char[] separator = new char[] { ';' };
                    string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (strArray[i].StartsWith("stx="))
                        {
                            num = Convert.ToInt32(strArray[i].Substring(4));
                        }
                        else
                        {
                            buffer = strArray[i].ToHexBytes();
                        }
                    }
                }
                else
                {
                    buffer = address.ToHexBytes();
                }
                return OperateResult.CreateSuccessResult<byte[], int>(buffer, num);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[], int>(exception.Message);
            }
        }

        [HslMqttApi("ReadByteArray", "特殊的地址格式，需要采用解析包起始地址的报文，例如 modbus 协议为 stx=9;00 00 00 00 00 06 01 03 00 64 00 01")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[], int> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content1);
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
            return string.Format("FreedomTcpNet<{0}>[{1}:{2}]", base.ByteTransform.GetType(), this.IpAddress, this.Port);
        }

        public override OperateResult Write(string address, byte[] value)
        {
            return this.Read(address, 0);
        }
    }
}

