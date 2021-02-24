namespace HslCommunication.Profinet.Panasonic
{
    using HslCommunication;
    using HslCommunication.Core.Address;
    using HslCommunication.Profinet.Melsec;
    using System;

    public class PanasonicMcNet : MelsecMcNet
    {
        public PanasonicMcNet()
        {
        }

        public PanasonicMcNet(string ipAddress, int port) : base(ipAddress, port)
        {
        }

        protected override OperateResult<McAddressData> McAnalysisAddress(string address, ushort length)
        {
            return McAddressData.ParsePanasonicFrom(address, length);
        }

        public override string ToString()
        {
            return string.Format("PanasonicMcNet[{0}:{1}]", this.IpAddress, this.Port);
        }
    }
}

