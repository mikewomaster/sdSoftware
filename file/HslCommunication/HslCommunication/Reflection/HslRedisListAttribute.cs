namespace HslCommunication.Reflection
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class HslRedisListAttribute : Attribute
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long <EndIndex>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ListKey>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long <StartIndex>k__BackingField;

        public HslRedisListAttribute(string listKey)
        {
            this.<EndIndex>k__BackingField = -1L;
            this.ListKey = listKey;
        }

        public HslRedisListAttribute(string listKey, long startIndex)
        {
            this.<EndIndex>k__BackingField = -1L;
            this.ListKey = listKey;
            this.StartIndex = startIndex;
        }

        public HslRedisListAttribute(string listKey, long startIndex, long endIndex)
        {
            this.<EndIndex>k__BackingField = -1L;
            this.ListKey = listKey;
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
        }

        public long EndIndex { get; set; }

        public string ListKey { get; set; }

        public long StartIndex { get; set; }
    }
}

