namespace HslCommunication.Reflection
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class HslRedisListItemAttribute : Attribute
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long <Index>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ListKey>k__BackingField;

        public HslRedisListItemAttribute(string listKey, long index)
        {
            this.ListKey = listKey;
            this.Index = index;
        }

        public long Index { get; set; }

        public string ListKey { get; set; }
    }
}

