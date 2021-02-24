namespace HslCommunication.MQTT
{
    using HslCommunication.Reflection;
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class MqttRpcApiInfo
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ApiTopic>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Description>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ExamplePayload>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <HttpMethod>k__BackingField = "GET";
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsMethodApi>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsOperateResultApi>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private HslMqttPermissionAttribute <PermissionAttribute>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private object <SourceObject>k__BackingField;
        private long calledCount = 0L;
        private MethodInfo method;
        private PropertyInfo property;
        private long spendTotalTime = 0L;

        public void CalledCountAddOne(long timeSpend)
        {
            Interlocked.Increment(ref this.calledCount);
            Interlocked.Add(ref this.spendTotalTime, timeSpend);
        }

        public override string ToString()
        {
            return this.ApiTopic;
        }

        public string ApiTopic { get; set; }

        public long CalledCount
        {
            get
            {
                return this.calledCount;
            }
            set
            {
                this.calledCount = value;
            }
        }

        public string Description { get; set; }

        public string ExamplePayload { get; set; }

        public string HttpMethod { get; set; }

        public bool IsMethodApi { get; set; }

        [JsonIgnore]
        public bool IsOperateResultApi { get; set; }

        [JsonIgnore]
        public MethodInfo Method
        {
            get
            {
                return this.method;
            }
            set
            {
                this.method = value;
                this.IsMethodApi = true;
            }
        }

        [JsonIgnore]
        public HslMqttPermissionAttribute PermissionAttribute { get; set; }

        [JsonIgnore]
        public PropertyInfo Property
        {
            get
            {
                return this.property;
            }
            set
            {
                this.property = value;
                this.IsMethodApi = false;
            }
        }

        [JsonIgnore]
        public object SourceObject { get; set; }

        public double SpendTotalTime
        {
            get
            {
                return (((double) this.spendTotalTime) / 100000.0);
            }
            set
            {
                this.spendTotalTime = (long) (value * 100000.0);
            }
        }
    }
}

