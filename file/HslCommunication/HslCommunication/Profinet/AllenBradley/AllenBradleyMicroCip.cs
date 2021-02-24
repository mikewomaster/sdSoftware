namespace HslCommunication.Profinet.AllenBradley
{
    using System;
    using System.Runtime.InteropServices;

    public class AllenBradleyMicroCip : AllenBradleyNet
    {
        public AllenBradleyMicroCip()
        {
        }

        public AllenBradleyMicroCip(string ipAddress, [Optional, DefaultParameterValue(0xaf12)] int port) : base(ipAddress, port)
        {
        }

        protected override byte[] PackCommandService(byte[] portSlot, params byte[][] cips)
        {
            return AllenBradleyHelper.PackCleanCommandService(portSlot, cips);
        }

        public override string ToString()
        {
            return string.Format("AllenBradleyMicroCip[{0}:{1}]", this.IpAddress, this.Port);
        }
    }
}

