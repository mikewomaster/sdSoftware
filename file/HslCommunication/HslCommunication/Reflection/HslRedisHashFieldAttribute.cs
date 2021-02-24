namespace HslCommunication.Reflection
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class HslRedisHashFieldAttribute : Attribute
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Field>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <HaskKey>k__BackingField;

        public HslRedisHashFieldAttribute(string hashKey, string filed)
        {
            this.HaskKey = hashKey;
            this.Field = filed;
        }

        public string Field { get; set; }

        public string HaskKey { get; set; }
    }
}

