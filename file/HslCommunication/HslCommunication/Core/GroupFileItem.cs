namespace HslCommunication.Core
{
    using HslCommunication.BasicFramework;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class GroupFileItem
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Description>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long <DownloadTimes>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <FileName>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long <FileSize>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <MappingName>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Owner>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <UploadTime>k__BackingField;

        public string GetTextFromFileSize()
        {
            return SoftBasic.GetSizeDescription(this.FileSize);
        }

        public override string ToString()
        {
            string[] textArray1 = new string[] { "GroupFileItem[", this.FileName, ":", this.MappingName, "]" };
            return string.Concat(textArray1);
        }

        public string Description { get; set; }

        public long DownloadTimes { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public string MappingName { get; set; }

        public string Owner { get; set; }

        public DateTime UploadTime { get; set; }
    }
}

