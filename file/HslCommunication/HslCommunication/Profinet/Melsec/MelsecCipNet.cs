namespace HslCommunication.Profinet.Melsec
{
    using HslCommunication;
    using HslCommunication.Profinet.AllenBradley;
    using HslCommunication.Reflection;
    using System;
    using System.Runtime.InteropServices;

    public class MelsecCipNet : AllenBradleyNet
    {
        public MelsecCipNet()
        {
        }

        public MelsecCipNet(string ipAddress, [Optional, DefaultParameterValue(0xaf12)] int port) : base(ipAddress, port)
        {
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            string[] textArray1 = new string[] { address };
            int[] numArray1 = new int[] { length };
            return base.Read(textArray1, numArray1);
        }
    }
}

