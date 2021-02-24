namespace HslCommunication.Core.IMessage
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class MelsecQnA3EAsciiMessage : INetMessage
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
            return ((((this.HeadBytes[0] == 0x44) && (this.HeadBytes[1] == 0x30)) && (this.HeadBytes[2] == 0x30)) && (this.HeadBytes[3] == 0x30));
        }

        public int GetContentLengthByHeadBytes()
        {
            byte[] bytes = new byte[] { this.HeadBytes[14], this.HeadBytes[15], this.HeadBytes[0x10], this.HeadBytes[0x11] };
            return Convert.ToInt32(Encoding.ASCII.GetString(bytes), 0x10);
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
                return 0x12;
            }
        }

        public byte[] SendBytes { get; set; }
    }
}

