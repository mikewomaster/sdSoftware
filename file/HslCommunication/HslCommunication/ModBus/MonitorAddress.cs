namespace HslCommunication.ModBus
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct MonitorAddress
    {
        public ushort Address;
        public short ValueOrigin;
        public short ValueNew;
    }
}

