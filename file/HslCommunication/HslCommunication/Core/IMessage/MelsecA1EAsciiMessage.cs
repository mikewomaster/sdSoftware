namespace HslCommunication.Core.IMessage
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class MelsecA1EAsciiMessage : INetMessage
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <ContentBytes>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <HeadBytes>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <SendBytes>k__BackingField;

        public bool CheckHeadBytesLegal(byte[] token)
        {
            return ((this.HeadBytes > null) && ((this.HeadBytes[0] - this.SendBytes[0]) == 8));
        }

        public int GetContentLengthByHeadBytes()
        {
            if ((this.HeadBytes[2] == 0x35) && (this.HeadBytes[3] == 0x42))
            {
                return 4;
            }
            if ((this.HeadBytes[2] == 0x30) && (this.HeadBytes[3] == 0x30))
            {
                int num2 = Convert.ToInt32(Encoding.ASCII.GetString(this.SendBytes, 20, 2), 0x10);
                if (num2 == 0)
                {
                    num2 = 0x100;
                }
                switch (this.HeadBytes[1])
                {
                    case 0x30:
                        return (((num2 % 2) == 1) ? (num2 + 1) : num2);

                    case 0x31:
                        return (num2 * 4);

                    case 50:
                    case 0x33:
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
                return 4;
            }
        }

        public byte[] SendBytes { get; set; }
    }
}

