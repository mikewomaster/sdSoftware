namespace HslCommunication.Core.IMessage
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class FinsMessage : INetMessage
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
            return ((((this.HeadBytes[0] == 70) && (this.HeadBytes[1] == 0x49)) && (this.HeadBytes[2] == 0x4e)) && (this.HeadBytes[3] == 0x53));
        }

        public int GetContentLengthByHeadBytes()
        {
            return BitConverter.ToInt32(new byte[] { this.HeadBytes[7], this.HeadBytes[6], this.HeadBytes[5], this.HeadBytes[4] }, 0);
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
                return 8;
            }
        }

        public byte[] SendBytes { get; set; }
    }
}

