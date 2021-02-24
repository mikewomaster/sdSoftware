namespace HslCommunication.BasicFramework
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class MessageBoard
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Content>k__BackingField = "";
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <HasViewed>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <NameReceive>k__BackingField = "";
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <NameSend>k__BackingField = "";
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <SendTime>k__BackingField = DateTime.Now;

        public string Content { get; set; }

        public bool HasViewed { get; set; }

        public string NameReceive { get; set; }

        public string NameSend { get; set; }

        public DateTime SendTime { get; set; }
    }
}

