namespace HslCommunication.Reflection
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class HslDeviceAddressAttribute : Attribute
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string <Address>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Type <DeviceType>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int <Length>k__BackingField;

        public HslDeviceAddressAttribute(string address)
        {
            this.<Address>k__BackingField = address;
            this.<Length>k__BackingField = -1;
            this.DeviceType = null;
        }

        public HslDeviceAddressAttribute(string address, int length)
        {
            this.<Address>k__BackingField = address;
            this.<Length>k__BackingField = length;
            this.DeviceType = null;
        }

        public HslDeviceAddressAttribute(string address, Type deviceType)
        {
            this.<Address>k__BackingField = address;
            this.<Length>k__BackingField = -1;
            this.DeviceType = deviceType;
        }

        public HslDeviceAddressAttribute(string address, int length, Type deviceType)
        {
            this.<Address>k__BackingField = address;
            this.<Length>k__BackingField = length;
            this.DeviceType = deviceType;
        }

        public override string ToString()
        {
            return string.Format("HslDeviceAddressAttribute[{0}:{1}]", this.Address, this.Length);
        }

        public string Address
        {
            [CompilerGenerated]
            get
            {
                return this.<Address>k__BackingField;
            }
        }

        public Type DeviceType { get; set; }

        public int Length
        {
            [CompilerGenerated]
            get
            {
                return this.<Length>k__BackingField;
            }
        }
    }
}

