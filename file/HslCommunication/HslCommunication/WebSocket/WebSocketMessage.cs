namespace HslCommunication.WebSocket
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class WebSocketMessage
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <HasMask>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <OpCode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <Payload>k__BackingField;

        public override string ToString()
        {
            return string.Format("OpCode[{0}] HasMask[{1}] Payload: {2}", this.OpCode, this.HasMask, Encoding.UTF8.GetString(this.Payload));
        }

        public bool HasMask { get; set; }

        public int OpCode { get; set; }

        public byte[] Payload { get; set; }
    }
}

