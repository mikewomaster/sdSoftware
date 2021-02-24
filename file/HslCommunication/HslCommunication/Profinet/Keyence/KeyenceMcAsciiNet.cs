namespace HslCommunication.Profinet.Keyence
{
    using HslCommunication;
    using HslCommunication.Core.Address;
    using HslCommunication.Profinet.Melsec;
    using System;

    public class KeyenceMcAsciiNet : MelsecMcAsciiNet
    {
        public KeyenceMcAsciiNet()
        {
        }

        public KeyenceMcAsciiNet(string ipAddress, int port) : base(ipAddress, port)
        {
        }

        protected override OperateResult<McAddressData> McAnalysisAddress(string address, ushort length)
        {
            return McAddressData.ParseKeyenceFrom(address, length);
        }

        public override string ToString()
        {
            return string.Format("KeyenceMcAsciiNet[{0}:{1}]", this.IpAddress, this.Port);
        }
    }
}

