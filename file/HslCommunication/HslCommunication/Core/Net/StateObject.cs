namespace HslCommunication.Core.Net
{
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;

    internal class StateObject : StateOneBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsClose>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <UniqueId>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Socket <WorkSocket>k__BackingField;

        public StateObject()
        {
        }

        public StateObject(int length)
        {
            base.DataLength = length;
            base.Buffer = new byte[length];
        }

        public void Clear()
        {
            base.IsError = false;
            this.IsClose = false;
            base.AlreadyDealLength = 0;
            base.Buffer = null;
        }

        public bool IsClose { get; set; }

        public string UniqueId { get; set; }

        public Socket WorkSocket { get; set; }
    }
}

