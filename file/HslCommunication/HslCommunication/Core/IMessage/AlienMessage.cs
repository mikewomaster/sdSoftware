namespace HslCommunication.Core.IMessage
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class AlienMessage : INetMessage
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <ContentBytes>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <HeadBytes>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <SendBytes>k__BackingField;

        public bool CheckHeadBytesLegal(byte[] token)
        {
            if (this.HeadBytes == null)
            {
                return false;
            }
            return (((this.HeadBytes[0] == 0x48) && (this.HeadBytes[1] == 0x73)) && (this.HeadBytes[2] == 110));
        }

        public int GetContentLengthByHeadBytes()
        {
            return this.HeadBytes[4];
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
                return 5;
            }
        }

        public byte[] SendBytes { get; set; }
    }
}

