namespace HslCommunication.Profinet.AllenBradley
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class AllenBradleyItemValue
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <Buffer>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsArray>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <TypeLength>k__BackingField = 1;

        public byte[] Buffer { get; set; }

        public bool IsArray { get; set; }

        public int TypeLength { get; set; }
    }
}

