namespace HslCommunication.Profinet.AllenBradley
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class AbTagItem
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <ArrayDimension>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private uint <InstanceID>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsStruct>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Name>k__BackingField;
        private ushort symbolType = 0;

        public int ArrayDimension { get; set; }

        public uint InstanceID { get; set; }

        public bool IsStruct { get; set; }

        public string Name { get; set; }

        public ushort SymbolType
        {
            get
            {
                return this.symbolType;
            }
            set
            {
                this.symbolType = value;
                this.ArrayDimension = ((this.symbolType & 0x4000) == 0x4000) ? 2 : (((this.symbolType & 0x2000) == 0x2000) ? 1 : 0);
                this.IsStruct = (this.symbolType & 0x8000) == 0x8000;
            }
        }
    }
}

