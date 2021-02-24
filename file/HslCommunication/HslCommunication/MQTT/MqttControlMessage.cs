namespace HslCommunication.MQTT
{
    using System;

    public class MqttControlMessage
    {
        public const byte CONNACK = 2;
        public const byte CONNECT = 1;
        public const byte DISCONNECT = 14;
        public const byte FAILED = 0;
        public const byte FileDelete = 0x67;
        public const byte FileDownload = 0x65;
        public const byte FileExists = 0x6b;
        public const byte FileFolderDelete = 0x68;
        public const byte FileFolderFiles = 0x69;
        public const byte FileFolderPaths = 0x6a;
        public const byte FileNoSense = 100;
        public const byte FileUpload = 0x66;
        public const byte PINGREQ = 12;
        public const byte PINGRESP = 13;
        public const byte PUBACK = 4;
        public const byte PUBCOMP = 7;
        public const byte PUBLISH = 3;
        public const byte PUBREC = 5;
        public const byte PUBREL = 6;
        public const byte REPORTPROGRESS = 15;
        public const byte SUBACK = 9;
        public const byte SUBSCRIBE = 8;
        public const byte UNSUBACK = 11;
        public const byte UNSUBSCRIBE = 10;
    }
}

