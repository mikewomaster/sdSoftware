namespace HslCommunication.MQTT
{
    using HslCommunication.Core;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class MqttConnectionOptions
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <CleanSession>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ClientId>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <ConnectTimeout>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private MqttCredential <Credentials>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TimeSpan <KeepAlivePeriod>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TimeSpan <KeepAliveSendInterval>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <Port>k__BackingField;
        private string ipAddress = "127.0.0.1";

        public MqttConnectionOptions()
        {
            this.ClientId = string.Empty;
            this.IpAddress = "127.0.0.1";
            this.Port = 0x75b;
            this.KeepAlivePeriod = TimeSpan.FromSeconds(100.0);
            this.KeepAliveSendInterval = TimeSpan.FromSeconds(30.0);
            this.CleanSession = true;
            this.ConnectTimeout = 0x1388;
        }

        public bool CleanSession { get; set; }

        public string ClientId { get; set; }

        public int ConnectTimeout { get; set; }

        public MqttCredential Credentials { get; set; }

        public string IpAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                this.ipAddress = HslHelper.GetIpAddressFromInput(value);
            }
        }

        public TimeSpan KeepAlivePeriod { get; set; }

        public TimeSpan KeepAliveSendInterval { get; set; }

        public int Port { get; set; }
    }
}

