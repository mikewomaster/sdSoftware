namespace HslCommunication.Enthernet
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class NetSimplifyClient : NetworkDoubleBase
    {
        public NetSimplifyClient()
        {
            base.ByteTransform = new RegularByteTransform();
        }

        public NetSimplifyClient(IPAddress ipAddress, int port)
        {
            base.ByteTransform = new RegularByteTransform();
            this.IpAddress = ipAddress.ToString();
            this.Port = port;
        }

        public NetSimplifyClient(string ipAddress, int port)
        {
            base.ByteTransform = new RegularByteTransform();
            this.IpAddress = ipAddress;
            this.Port = port;
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new HslMessage();
        }

        protected override OperateResult InitializationOnConnect(Socket socket)
        {
            if (base.isUseAccountCertificate)
            {
                return base.AccountCertificate(socket);
            }
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult<NetHandle, byte[]> ReadCustomerFromServer(NetHandle customer, byte[] send)
        {
            return this.ReadCustomerFromServerBase(HslProtocol.CommandBytes((int) customer, base.Token, send));
        }

        public OperateResult<NetHandle, string> ReadCustomerFromServer(NetHandle customer, string send)
        {
            OperateResult<NetHandle, byte[]> result = this.ReadCustomerFromServerBase(HslProtocol.CommandBytes((int) customer, base.Token, send));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<NetHandle, string>(result);
            }
            return OperateResult.CreateSuccessResult<NetHandle, string>(result.Content1, Encoding.Unicode.GetString(result.Content2));
        }

        public OperateResult<NetHandle, string[]> ReadCustomerFromServer(NetHandle customer, string[] send)
        {
            OperateResult<NetHandle, byte[]> result = this.ReadCustomerFromServerBase(HslProtocol.CommandBytes((int) customer, base.Token, send));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<NetHandle, string[]>(result);
            }
            return OperateResult.CreateSuccessResult<NetHandle, string[]>(result.Content1, HslProtocol.UnPackStringArrayFromByte(result.Content2));
        }

        private OperateResult<NetHandle, byte[]> ReadCustomerFromServerBase(byte[] send)
        {
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
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

        public OperateResult<string> ReadFromServer(NetHandle customer, string send)
        {
            OperateResult<byte[]> result = this.ReadFromServerBase(HslProtocol.CommandBytes((int) customer, base.Token, send));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            return OperateResult.CreateSuccessResult<string>(Encoding.Unicode.GetString(result.Content));
        }

        public OperateResult<string[]> ReadFromServer(NetHandle customer, string[] send)
        {
            OperateResult<byte[]> result = this.ReadFromServerBase(HslProtocol.CommandBytes((int) customer, base.Token, send));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string[]>(result);
            }
            return OperateResult.CreateSuccessResult<string[]>(HslProtocol.UnPackStringArrayFromByte(result.Content));
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
            return string.Format("NetSimplifyClient[{0}:{1}]", this.IpAddress, this.Port);
        }
    }
}

