namespace HslCommunication.MQTT
{
    using HslCommunication.BasicFramework;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class MqttSubscribeMessage
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <Identifier>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private MqttQualityOfServiceLevel <QualityOfServiceLevel>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <Topics>k__BackingField;

        public MqttSubscribeMessage()
        {
            this.QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce;
        }

        public override string ToString()
        {
            return ("MqttSubcribeMessage" + SoftBasic.ArrayFormat<string>(this.Topics));
        }

        public int Identifier { get; set; }

        public MqttQualityOfServiceLevel QualityOfServiceLevel { get; set; }

        public string[] Topics { get; set; }
    }
}

