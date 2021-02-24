namespace HslCommunication.Core.IMessage
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class AllenBradleySLCMessage : INetMessage
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
            if (this.HeadBytes == null)
            {
                return 0;
            }
            return ((this.HeadBytes[2] * 0x100) + this.HeadBytes[3]);
        }

        public int GetHeadBytesIdentity()
        {
            return 0;
        }

        public byte[] ContentBytes { get; set; }

        public byte[] HeadBytes { get; set; }

        public int ProtocolHeadBytesLength
        {
            get
            {
                return 0x1c;
            }
        }

        public byte[] SendBytes { get; set; }
    }
}

