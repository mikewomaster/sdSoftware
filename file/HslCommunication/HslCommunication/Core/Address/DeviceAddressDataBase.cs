namespace HslCommunication.Core.Address
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class DeviceAddressDataBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <AddressStart>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <Length>k__BackingField;

        public virtual void Parse(string address, ushort length)
        {
            this.AddressStart = int.Parse(address);
            this.Length = length;
        }

        public override string ToString()
        {
            return this.AddressStart.ToString();
        }

        public int AddressStart { get; set; }

        public ushort Length { get; set; }
    }
}

