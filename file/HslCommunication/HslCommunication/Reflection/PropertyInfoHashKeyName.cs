namespace HslCommunication.Reflection
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class PropertyInfoHashKeyName : PropertyInfoKeyName
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Field>k__BackingField;

        public PropertyInfoHashKeyName(PropertyInfo property, string key, string field) : base(property, key)
        {
            this.Field = field;
        }

        public PropertyInfoHashKeyName(PropertyInfo property, string key, string field, string value) : base(property, key, value)
        {
            this.Field = field;
        }

        public string Field { get; set; }
    }
}

