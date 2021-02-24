namespace HslCommunication.Enthernet
{
    using HslCommunication;
    using HslCommunication.Core.Net;
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class NetUdpClient : NetworkUdpBase
    {
        public NetUdpClient(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
        }

        public OperateResult<NetHandle, byte[]> ReadCustomerFromServer(NetHandle customer, byte[] send)
        {
            return this.ReadCustomerFromServerBase(HslProtocol.CommandBytes((int) customer, base.Token, send));
        }

        public OperateResult<NetHandle, string> ReadCustomerFromServer(NetHandle customer, [Optional, DefaultParameterValue(null)] string send)
        {
            OperateResult<NetHandle, byte[]> result = this.ReadCustomerFromServerBase(HslProtocol.CommandBytes((int) customer, base.Token, send));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<NetHandle, string>(result);
            }
            return OperateResult.CreateSuccessResult<NetHandle, string>(result.Content1, Encoding.Unicode.GetString(result.Content2));
        }

        private OperateResult<NetHandle, byte[]> ReadCustomerFromServerBase(byte[] send)
        {
            OperateResult<byte[]> result = this.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<NetHandle, byte[]>(result);
            }
            return HslProtocol.ExtractHslData(result.Content);
        }

        public OperateResult<byte[]> ReadFromServer(NetHandle customer, byte[] send)
        {
            return this.ReadFromServerBase(HslProtocol.CommandBytes((int) customer, base.Token, send));
        }

        public OperateResult<string> ReadFromServer(NetHandle customer, [Optional, DefaultParameterValue(null)] string send)
        {
            OperateResult<byte[]> result = this.ReadFromServerBase(HslProtocol.CommandBytes((int) customer, base.Token, send));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            return OperateResult.CreateSuccessResult<string>(Encoding.Unicode.GetString(result.Content));
        }

        private OperateResult<byte[]> ReadFromServerBase(byte[] send)
        {
            OperateResult<NetHandle, byte[]> result = this.ReadCustomerFromServerBase(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result.Content2);
        }

        public override string ToString()
        {
            return string.Format("NetUdpClient[{0}:{1}]", this.IpAddress, this.Port);
        }
    }
}

