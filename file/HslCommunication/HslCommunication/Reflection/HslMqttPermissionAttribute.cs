namespace HslCommunication.Reflection
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
    public class HslMqttPermissionAttribute : Attribute
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ClientID>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <UserName>k__BackingField;

        public virtual bool CheckClientID(string clientID)
        {
            return (string.IsNullOrEmpty(this.ClientID) || (this.ClientID == clientID));
        }

        public virtual bool CheckUserName(string name)
        {
            return (string.IsNullOrEmpty(this.UserName) || (this.UserName == name));
        }

        public string ClientID { get; set; }

        public string UserName { get; set; }
    }
}

