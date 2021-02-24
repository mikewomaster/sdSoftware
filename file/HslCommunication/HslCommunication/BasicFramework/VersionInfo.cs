namespace HslCommunication.BasicFramework
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public sealed class VersionInfo
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <ReleaseDate>k__BackingField = DateTime.Now;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private StringBuilder <UpdateDetails>k__BackingField = new StringBuilder();
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private SystemVersion <VersionNum>k__BackingField = new SystemVersion(1, 0, 0);

        public override string ToString()
        {
            return this.VersionNum.ToString();
        }

        public DateTime ReleaseDate { get; set; }

        public StringBuilder UpdateDetails { get; set; }

        public SystemVersion VersionNum { get; set; }
    }
}

