namespace HslCommunication.CNC.Fanuc
{
    using HslCommunication.Core.IMessage;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class CNCFanucSeriesMessage : INetMessage
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <ContentBytes>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <HeadBytes>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <SendBytes>k__BackingField;

        public bool CheckHeadBytesLegal(byte[] token)
        {
            return true;
        }

        public int GetContentLengthByHeadBytes()
        {
            return ((this.HeadBytes[8] * 0x100) + this.HeadBytes[9]);
        }

        public int GetHeadBytesIdentity()
        {
            return 0;
        }

        public override string ToString()
        {
            return "CNCFanucSeriesMessage";
        }

        public byte[] ContentBytes { get; set; }

        public byte[] HeadBytes { get; set; }

        public int ProtocolHeadBytesLength
        {
            get
            {
                return 10;
            }
        }

        public byte[] SendBytes { get; set; }
    }
}

