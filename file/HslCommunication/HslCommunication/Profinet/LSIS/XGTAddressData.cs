namespace HslCommunication.Profinet.LSIS
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class XGTAddressData
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Address>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <AddressString>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Data>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <DataByteArray>k__BackingField;

        public string Address { get; set; }

        public byte[] AddressByteArray
        {
            get
            {
                return Encoding.ASCII.GetBytes(this.AddressString);
            }
        }

        public string AddressString { get; set; }

        public string Data { get; set; }

        public byte[] DataByteArray { get; set; }

        public byte[] LengthByteArray
        {
            get
            {
                return BitConverter.GetBytes((short) this.AddressByteArray.Length);
            }
        }
    }
}

