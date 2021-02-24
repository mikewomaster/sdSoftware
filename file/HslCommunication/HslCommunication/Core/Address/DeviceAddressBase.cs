namespace HslCommunication.Core.Address
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class DeviceAddressBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <Address>k__BackingField;

        public virtual void Parse(string address)
        {
            this.Address = ushort.Parse(address);
        }

        public override string ToString()
        {
            return this.Address.ToString();
        }

        public ushort Address { get; set; }
    }
}

