namespace HslCommunication.Profinet.AllenBradley
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class AbStructHandle
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <Count>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <MemberCount>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <StructureHandle>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private uint <TemplateObjectDefinitionSize>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private uint <TemplateStructureSize>k__BackingField;

        public ushort Count { get; set; }

        public ushort MemberCount { get; set; }

        public ushort StructureHandle { get; set; }

        public uint TemplateObjectDefinitionSize { get; set; }

        public uint TemplateStructureSize { get; set; }
    }
}

