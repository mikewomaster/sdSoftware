namespace HslCommunication.Core.IMessage
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class FetchWriteMessage : INetMessage
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
            return ((this.HeadBytes[0] == 0x53) && (this.HeadBytes[1] == 0x35));
        }

        public int GetContentLengthByHeadBytes()
        {
            if ((this.HeadBytes[5] != 5) && (this.HeadBytes[5] != 4))
            {
                if (this.HeadBytes[5] == 6)
                {
                    if (this.SendBytes == null)
                    {
                        return 0;
                    }
                    if (this.HeadBytes[8] > 0)
                    {
                        return 0;
                    }
                    if (((this.SendBytes[8] == 1) || (this.SendBytes[8] == 6)) || (this.SendBytes[8] == 7))
                    {
                        return (((this.SendBytes[12] * 0x100) + this.SendBytes[13]) * 2);
                    }
                    return ((this.SendBytes[12] * 0x100) + this.SendBytes[13]);
                }
                if (this.HeadBytes[5] == 3)
                {
                    if (((this.HeadBytes[8] == 1) || (this.HeadBytes[8] == 6)) || (this.HeadBytes[8] == 7))
                    {
                        return (((this.HeadBytes[12] * 0x100) + this.HeadBytes[13]) * 2);
                    }
                    return ((this.HeadBytes[12] * 0x100) + this.HeadBytes[13]);
                }
            }
            return 0;
        }

        public int GetHeadBytesIdentity()
        {
            return this.HeadBytes[3];
        }

        public byte[] ContentBytes { get; set; }

        public byte[] HeadBytes { get; set; }

        public int ProtocolHeadBytesLength
        {
            get
            {
                return 0x10;
            }
        }

        public byte[] SendBytes { get; set; }
    }
}

