namespace HslCommunication.Reflection
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple=false)]
    public class HslMqttApiAttribute : Attribute
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ApiTopic>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Description>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <HttpMethod>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <PropertyUnfold>k__BackingField;

        public HslMqttApiAttribute()
        {
            this.<PropertyUnfold>k__BackingField = false;
            this.<HttpMethod>k__BackingField = "POST";
        }

        public HslMqttApiAttribute(string description)
        {
            this.<PropertyUnfold>k__BackingField = false;
            this.<HttpMethod>k__BackingField = "POST";
            this.Description = description;
        }

        public HslMqttApiAttribute(string apiTopic, string description)
        {
            this.<PropertyUnfold>k__BackingField = false;
            this.<HttpMethod>k__BackingField = "POST";
            this.ApiTopic = apiTopic;
            this.Description = description;
        }

        public string ApiTopic { get; set; }

        public string Description { get; set; }

        public string HttpMethod { get; set; }

        public bool PropertyUnfold { get; set; }
    }
}

