namespace HslCommunication.MQTT
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class MqttCredential
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Password>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <UserName>k__BackingField;

        public MqttCredential()
        {
        }

        public MqttCredential(string name, string pwd)
        {
            this.UserName = name;
            this.Password = pwd;
        }

        public override string ToString()
        {
            return this.UserName;
        }

        public string Password { get; set; }

        public string UserName { get; set; }
    }
}

