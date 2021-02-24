namespace HslCommunication.MQTT
{
    using System;

    public enum MqttQualityOfServiceLevel
    {
        AtMostOnce,
        AtLeastOnce,
        ExactlyOnce,
        OnlyTransfer
    }
}

