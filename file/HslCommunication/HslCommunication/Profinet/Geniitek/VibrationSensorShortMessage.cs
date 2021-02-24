namespace HslCommunication.Profinet.Geniitek
{
    using HslCommunication.Core.IMessage;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class VibrationSensorShortMessage : INetMessage
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
            return (this.HeadBytes[0] == 170);
        }

        public int GetContentLengthByHeadBytes()
        {
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
                return 9;
            }
        }

        public byte[] SendBytes { get; set; }
    }
}

