namespace HslCommunication.Core.IMessage
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class MelsecA1EBinaryMessage : INetMessage
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <ContentBytes>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <HeadBytes>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <SendBytes>k__BackingField;

        public bool CheckHeadBytesLegal(byte[] token)
        {
            return ((this.HeadBytes > null) && ((this.HeadBytes[0] - this.SendBytes[0]) == 0x80));
        }

        public int GetContentLengthByHeadBytes()
        {
            if (this.HeadBytes[1] == 0x5b)
            {
                return 2;
            }
            if (this.HeadBytes[1] == 0)
            {
                switch (this.HeadBytes[0])
                {
                    case 0x80:
                        return ((this.SendBytes[10] != 0) ? ((this.SendBytes[10] + 1) / 2) : 0x80);

                    case 0x81:
                        return (this.SendBytes[10] * 2);

                    case 130:
                    case 0x83:
                        return 0;
                }
                return 0;
            }
            return 0;
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
                return 2;
            }
        }

        public byte[] SendBytes { get; set; }
    }
}

