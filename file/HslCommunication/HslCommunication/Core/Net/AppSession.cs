namespace HslCommunication.Core.Net
{
    using HslCommunication.Core;
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;

    public class AppSession
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <AlreadyReceivedContent>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <AlreadyReceivedHead>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <BytesContent>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <BytesHead>k__BackingField = new byte[0x20];
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ClientType>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ClientUniqueID>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <HeartTime>k__BackingField = DateTime.Now;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private SimpleHybirdLock <HybirdLockSend>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <IpAddress>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IPEndPoint <IpEndPoint>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <KeyGroup>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <LoginAlias>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Socket <WorkSocket>k__BackingField;
        internal EndPoint UdpEndPoint = null;

        public AppSession()
        {
            this.ClientUniqueID = Guid.NewGuid().ToString("N");
            this.HybirdLockSend = new SimpleHybirdLock();
        }

        internal void Clear()
        {
            this.BytesHead = new byte[0x20];
            this.AlreadyReceivedHead = 0;
            this.BytesContent = null;
            this.AlreadyReceivedContent = 0;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.LoginAlias))
            {
                return string.Format("AppSession[{0}]", this.IpEndPoint);
            }
            return string.Format("AppSession[{0}] [{1}]", this.IpEndPoint, this.LoginAlias);
        }

        internal int AlreadyReceivedContent { get; set; }

        internal int AlreadyReceivedHead { get; set; }

        internal byte[] BytesContent { get; set; }

        internal byte[] BytesHead { get; set; }

        public string ClientType { get; set; }

        public string ClientUniqueID { get; private set; }

        public DateTime HeartTime { get; set; }

        internal SimpleHybirdLock HybirdLockSend { get; set; }

        public string IpAddress { get; internal set; }

        public IPEndPoint IpEndPoint { get; internal set; }

        internal string KeyGroup { get; set; }

        public string LoginAlias { get; set; }

        internal Socket WorkSocket { get; set; }
    }
}

