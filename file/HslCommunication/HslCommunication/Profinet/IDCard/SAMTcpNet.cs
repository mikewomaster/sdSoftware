namespace HslCommunication.Profinet.IDCard
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;

    public class SAMTcpNet : NetworkDoubleBase
    {
        public SAMTcpNet()
        {
            base.ByteTransform = new RegularByteTransform();
        }

        public SAMTcpNet(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
        }

        [HslMqttApi]
        public OperateResult CheckSafeModuleStatus()
        {
            byte[] send = SAMSerial.PackToSAMCommand(SAMSerial.BuildReadCommand(0x12, 0xff, null));
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult result2 = SAMSerial.CheckADSCommandAndSum(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            if (result.Content[9] != 0x90)
            {
                return new OperateResult(SAMSerial.GetErrorDescription(result.Content[9]));
            }
            return OperateResult.CreateSuccessResult();
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new SAMMessage();
        }

        [HslMqttApi]
        public OperateResult<IdentityCard> ReadCard()
        {
            byte[] send = SAMSerial.PackToSAMCommand(SAMSerial.BuildReadCommand(0x30, 1, null));
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<IdentityCard>(result);
            }
            OperateResult result2 = SAMSerial.CheckADSCommandAndSum(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<IdentityCard>(result2);
            }
            return SAMSerial.ExtractIdentityCard(result.Content);
        }

        [HslMqttApi]
        public OperateResult<string> ReadSafeModuleNumber()
        {
            byte[] send = SAMSerial.PackToSAMCommand(SAMSerial.BuildReadCommand(0x12, 0xff, null));
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult result2 = SAMSerial.CheckADSCommandAndSum(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            return SAMSerial.ExtractSafeModuleNumber(result.Content);
        }

        [HslMqttApi]
        public OperateResult SearchCard()
        {
            byte[] send = SAMSerial.PackToSAMCommand(SAMSerial.BuildReadCommand(0x20, 1, null));
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult result2 = SAMSerial.CheckADSCommandAndSum(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            if (result.Content[9] != 0x9f)
            {
                return new OperateResult(SAMSerial.GetErrorDescription(result.Content[9]));
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi]
        public OperateResult SelectCard()
        {
            byte[] send = SAMSerial.PackToSAMCommand(SAMSerial.BuildReadCommand(0x20, 2, null));
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult result2 = SAMSerial.CheckADSCommandAndSum(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            if (result.Content[9] != 0x90)
            {
                return new OperateResult(SAMSerial.GetErrorDescription(result.Content[9]));
            }
            return OperateResult.CreateSuccessResult();
        }

        public override string ToString()
        {
            return string.Format("SAMTcpNet[{0}:{1}]", this.IpAddress, this.Port);
        }
    }
}

