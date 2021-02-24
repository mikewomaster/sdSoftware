namespace HslCommunication.Core
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class FileBaseInfo
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Name>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long <Size>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Tag>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Upload>k__BackingField;

        public string Name { get; set; }

        public long Size { get; set; }

        public string Tag { get; set; }

        public string Upload { get; set; }
    }
}

