namespace HslCommunication.Enthernet
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;

    public class DeviceState
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <ConnectTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IPEndPoint <DeviceEndPoint>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <IpAddress>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <ReceiveTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Socket <WorkSocket>k__BackingField;
        internal byte[] Buffer = new byte[1];

        public DateTime ConnectTime { get; set; }

        public IPEndPoint DeviceEndPoint { get; set; }

        public string IpAddress { get; set; }

        public DateTime ReceiveTime { get; set; }

        internal Socket WorkSocket { get; set; }
    }
}

