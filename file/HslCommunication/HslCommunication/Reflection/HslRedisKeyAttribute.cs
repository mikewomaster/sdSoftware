namespace HslCommunication.Reflection
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class HslRedisKeyAttribute : Attribute
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <KeyName>k__BackingField;

        public HslRedisKeyAttribute(string key)
        {
            this.KeyName = key;
        }

        public string KeyName { get; set; }
    }
}

