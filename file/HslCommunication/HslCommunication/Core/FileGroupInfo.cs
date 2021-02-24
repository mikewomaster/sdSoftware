namespace HslCommunication.Core
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class FileGroupInfo
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <Command>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Factory>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <FileName>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <FileNames>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Group>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Identify>k__BackingField;

        public int Command { get; set; }

        public string Factory { get; set; }

        public string FileName { get; set; }

        public string[] FileNames { get; set; }

        public string Group { get; set; }

        public string Identify { get; set; }
    }
}

