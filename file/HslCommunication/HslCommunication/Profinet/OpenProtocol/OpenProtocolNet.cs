namespace HslCommunication.Profinet.OpenProtocol
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Text;

    public class OpenProtocolNet : NetworkDoubleBase
    {
        public OpenProtocolNet()
        {
            base.ByteTransform = new RegularByteTransform();
        }

        public OpenProtocolNet(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
        }

        public static OperateResult<byte[]> BuildReadCommand(int mid, int revison, int stationId, int spindleId, List<string> parameters)
        {
            if ((mid < 0) || (mid > 0x270f))
            {
                return new OperateResult<byte[]>("Mid must be between 0 - 9999");
            }
            if ((revison < 0) || (revison > 0x3e7))
            {
                return new OperateResult<byte[]>("revison must be between 0 - 999");
            }
            if ((stationId < 0) || (stationId > 9))
            {
                return new OperateResult<byte[]>("stationId must be between 0 - 9");
            }
            if ((spindleId < 0) || (spindleId > 0x63))
            {
                return new OperateResult<byte[]>("spindleId must be between 0 - 99");
            }
            int count = 0;
            if (parameters > null)
            {
                parameters.ForEach(delegate (string m) {
                    count += m.Length;
                });
            }
            StringBuilder builder = new StringBuilder();
            builder.Append((20 + count).ToString("D4"));
            builder.Append(mid.ToString("D4"));
            builder.Append(revison.ToString("D3"));
            builder.Append('\0');
            builder.Append(stationId.ToString("D1"));
            builder.Append(spindleId.ToString("D2"));
            builder.Append('\0');
            builder.Append('\0');
            builder.Append('\0');
            builder.Append('\0');
            builder.Append('\0');
            if (parameters > null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    builder.Append(parameters[i]);
                }
            }
            builder.Append('\0');
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(builder.ToString()));
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new OpenProtocolMessage();
        }

        protected override OperateResult InitializationOnConnect(Socket socket)
        {
            OperateResult<string> result = this.ReadCustomer(1, 0, 0, 0, null);
            if (!result.IsSuccess)
            {
                return result;
            }
            if (result.Content.Substring(4, 4) == "0002")
            {
                return OperateResult.CreateSuccessResult();
            }
            return new OperateResult("Failed:" + result.Content.Substring(4, 4));
        }

        public OperateResult<string> ReadCustomer(int mid, int revison, int stationId, int spindleId, List<string> parameters)
        {
            if (parameters > null)
            {
                parameters = new List<string>();
            }
            OperateResult<byte[]> result = BuildReadCommand(mid, revison, stationId, spindleId, parameters);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            return OperateResult.CreateSuccessResult<string>(Encoding.ASCII.GetString(result2.Content));
        }

        public override string ToString()
        {
            return string.Format("OpenProtocolNet[{0}:{1}]", this.IpAddress, this.Port);
        }
    }
}

