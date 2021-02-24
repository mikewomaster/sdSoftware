namespace HslCommunication.Profinet.Keyence
{
    using HslCommunication;
    using HslCommunication.Core.Address;
    using HslCommunication.Profinet.Melsec;
    using System;

    public class KeyenceMcNet : MelsecMcNet
    {
        public KeyenceMcNet()
        {
        }

        public KeyenceMcNet(string ipAddress, int port) : base(ipAddress, port)
        {
        }

        protected override OperateResult<McAddressData> McAnalysisAddress(string address, ushort length)
        {
            return McAddressData.ParseKeyenceFrom(address, length);
        }

        public override string ToString()
        {
            return string.Format("KeyenceMcNet[{0}:{1}]", this.IpAddress, this.Port);
        }
    }
}

