namespace HslCommunication.MQTT
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class MqttPublishMessage
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <Identifier>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsSendFirstTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private MqttApplicationMessage <Message>k__BackingField;

        public MqttPublishMessage()
        {
            this.IsSendFirstTime = true;
        }

        public int Identifier { get; set; }

        public bool IsSendFirstTime { get; set; }

        public MqttApplicationMessage Message { get; set; }
    }
}

