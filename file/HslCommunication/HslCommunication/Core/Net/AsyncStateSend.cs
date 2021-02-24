namespace HslCommunication.Core.Net
{
    using HslCommunication.Core;
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;

    internal class AsyncStateSend
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <AlreadySendLength>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ClientId>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <Content>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private SimpleHybirdLock <HybirdLockSend>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Key>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Socket <WorkSocket>k__BackingField;

        internal int AlreadySendLength { get; set; }

        internal string ClientId { get; set; }

        internal byte[] Content { get; set; }

        internal SimpleHybirdLock HybirdLockSend { get; set; }

        internal string Key { get; set; }

        internal Socket WorkSocket { get; set; }
    }
}

