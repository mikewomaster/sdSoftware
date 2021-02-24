namespace HslCommunication.MQTT
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class MqttApplicationMessage
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <Payload>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private MqttQualityOfServiceLevel <QualityOfServiceLevel>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <Retain>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Topic>k__BackingField;

        public override string ToString()
        {
            return (this.Topic + ":" + Encoding.UTF8.GetString(this.Payload));
        }

        public byte[] Payload { get; set; }

        public MqttQualityOfServiceLevel QualityOfServiceLevel { get; set; }

        public bool Retain { get; set; }

        public string Topic { get; set; }
    }
}

