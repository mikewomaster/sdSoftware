namespace HslCommunication.Reflection
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class PropertyInfoKeyName
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <KeyName>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private System.Reflection.PropertyInfo <PropertyInfo>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Value>k__BackingField;

        public PropertyInfoKeyName(System.Reflection.PropertyInfo property, string key)
        {
            this.PropertyInfo = property;
            this.KeyName = key;
        }

        public PropertyInfoKeyName(System.Reflection.PropertyInfo property, string key, string value)
        {
            this.PropertyInfo = property;
            this.KeyName = key;
            this.Value = value;
        }

        public string KeyName { get; set; }

        public System.Reflection.PropertyInfo PropertyInfo { get; set; }

        public string Value { get; set; }
    }
}

