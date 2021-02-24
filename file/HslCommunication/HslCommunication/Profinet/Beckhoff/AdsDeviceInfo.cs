namespace HslCommunication.Profinet.Beckhoff
{
    using HslCommunication;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class AdsDeviceInfo
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <Build>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <DeviceName>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <Major>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <Minor>k__BackingField;

        public AdsDeviceInfo()
        {
        }

        public AdsDeviceInfo(byte[] data)
        {
            this.Major = data[0];
            this.Minor = data[1];
            this.Build = BitConverter.ToUInt16(data, 2);
            char[] trimChars = new char[2];
            trimChars[1] = ' ';
            this.DeviceName = Encoding.ASCII.GetString(data.RemoveBegin<byte>(4)).Trim(trimChars);
        }

        public ushort Build { get; set; }

        public string DeviceName { get; set; }

        public byte Major { get; set; }

        public byte Minor { get; set; }
    }
}

