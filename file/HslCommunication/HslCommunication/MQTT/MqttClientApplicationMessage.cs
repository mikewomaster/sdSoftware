namespace HslCommunication.MQTT
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class MqttClientApplicationMessage : MqttApplicationMessage
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ClientId>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <CreateTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsCancelPublish>k__BackingField = false;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <UserName>k__BackingField;

        public MqttClientApplicationMessage()
        {
            this.CreateTime = DateTime.Now;
        }

        public string ClientId { get; set; }

        public DateTime CreateTime { get; set; }

        public bool IsCancelPublish { get; set; }

        public string UserName { get; set; }
    }
}

