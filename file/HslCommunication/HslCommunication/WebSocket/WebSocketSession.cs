namespace HslCommunication.WebSocket
{
    using HslCommunication.BasicFramework;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;

    public class WebSocketSession
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <ActiveTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsQASession>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <OnlineTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IPEndPoint <Remote>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<string> <Topics>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Url>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Socket <WsSocket>k__BackingField;
        private object objLock = new object();

        public WebSocketSession()
        {
            this.Topics = new List<string>();
            this.ActiveTime = DateTime.Now;
            this.OnlineTime = DateTime.Now;
        }

        public void AddTopic(string topic)
        {
            object objLock = this.objLock;
            lock (objLock)
            {
                if (!this.Topics.Contains(topic))
                {
                    this.Topics.Add(topic);
                }
            }
        }

        public bool IsClientSubscribe(string topic)
        {
            object objLock = this.objLock;
            lock (objLock)
            {
                return this.Topics.Contains(topic);
            }
        }

        public bool RemoveTopic(string topic)
        {
            object objLock = this.objLock;
            lock (objLock)
            {
                return this.Topics.Remove(topic);
            }
        }

        public override string ToString()
        {
            return string.Format("WebSocketSession[{0}][{1}]", this.Remote, SoftBasic.GetTimeSpanDescription((TimeSpan) (DateTime.Now - this.OnlineTime)));
        }

        public DateTime ActiveTime { get; set; }

        public bool IsQASession { get; set; }

        public DateTime OnlineTime { get; private set; }

        public IPEndPoint Remote { get; set; }

        public List<string> Topics { get; set; }

        public string Url { get; set; }

        internal Socket WsSocket { get; set; }
    }
}

