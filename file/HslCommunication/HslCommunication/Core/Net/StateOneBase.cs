namespace HslCommunication.Core.Net
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class StateOneBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <AlreadyDealLength>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <Buffer>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <DataLength>k__BackingField = 0x20;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ErrerMsg>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsError>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ManualResetEvent <WaitDone>k__BackingField;

        public int AlreadyDealLength { get; set; }

        public byte[] Buffer { get; set; }

        public int DataLength { get; set; }

        public string ErrerMsg { get; set; }

        public bool IsError { get; set; }

        public ManualResetEvent WaitDone { get; set; }
    }
}

