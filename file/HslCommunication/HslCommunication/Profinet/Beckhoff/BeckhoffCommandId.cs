namespace HslCommunication.Profinet.Beckhoff
{
    using System;

    public class BeckhoffCommandId
    {
        public const ushort AddDeviceNotification = 6;
        public const ushort DeleteDeviceNotification = 7;
        public const ushort DeviceNotification = 8;
        public const ushort Read = 2;
        public const ushort ReadDeviceInfo = 1;
        public const ushort ReadState = 4;
        public const ushort ReadWrite = 9;
        public const ushort Write = 3;
        public const ushort WriteControl = 5;
    }
}

