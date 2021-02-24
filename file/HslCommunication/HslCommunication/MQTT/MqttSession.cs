namespace HslCommunication.MQTT
{
    using HslCommunication.BasicFramework;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;

    public class MqttSession
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <ActiveTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TimeSpan <ActiveTimeSpan>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ClientId>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IPEndPoint <EndPoint>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <ForbidPublishTopic>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Socket <MqttSocket>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <OnlineTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Protocol>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<string> <Topics>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <UserName>k__BackingField;
        private object objLock = new object();

        public MqttSession(IPEndPoint endPoint, string protocol)
        {
            this.Topics = new List<string>();
            this.ActiveTime = DateTime.Now;
            this.OnlineTime = DateTime.Now;
            this.ActiveTimeSpan = TimeSpan.FromSeconds(1000000.0);
            this.EndPoint = endPoint;
            this.Protocol = protocol;
        }

        public void AddSubscribe(string topic)
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

        public void AddSubscribe(string[] topics)
        {
            if (topics != null)
            {
                object objLock = this.objLock;
                lock (objLock)
                {
                    for (int i = 0; i < topics.Length; i++)
                    {
                        if (!this.Topics.Contains(topics[i]))
                        {
                            this.Topics.Add(topics[i]);
                        }
                    }
                }
            }
        }

        public string[] GetTopics()
        {
            object objLock = this.objLock;
            lock (objLock)
            {
                return this.Topics.ToArray();
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

        public void RemoveSubscribe(string topic)
        {
            object objLock = this.objLock;
            lock (objLock)
            {
                if (this.Topics.Contains(topic))
                {
                    this.Topics.Remove(topic);
                }
            }
        }

        public void RemoveSubscribe(string[] topics)
        {
            if (topics != null)
            {
                object objLock = this.objLock;
                lock (objLock)
                {
                    for (int i = 0; i < topics.Length; i++)
                    {
                        if (this.Topics.Contains(topics[i]))
                        {
                            this.Topics.Remove(topics[i]);
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0} Session[IP:{1}][ID:{2}][Name:{3}][{4}]", new object[] { this.Protocol, this.EndPoint, this.ClientId, this.UserName, SoftBasic.GetTimeSpanDescription((TimeSpan) (DateTime.Now - this.OnlineTime)) });
        }

        public DateTime ActiveTime { get; set; }

        public TimeSpan ActiveTimeSpan { get; set; }

        public string ClientId { get; set; }

        public IPEndPoint EndPoint { get; set; }

        public bool ForbidPublishTopic { get; set; }

        internal Socket MqttSocket { get; set; }

        public DateTime OnlineTime { get; private set; }

        public string Protocol { get; private set; }

        private List<string> Topics { get; set; }

        public string UserName { get; set; }
    }
}

