namespace HslCommunication.MQTT
{
    using HslCommunication;
    using HslCommunication.Algorithms.ConnectPool;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class MqttSyncClientPool
    {
        private HslCommunication.Algorithms.ConnectPool.ConnectPool<IMqttSyncConnector> mqttConnectPool;

        public MqttSyncClientPool(MqttConnectionOptions options)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                throw new Exception(StringResources.Language.InsufficientPrivileges);
            }
            MqttSyncClient client = new MqttSyncClient(options);
            this.mqttConnectPool = new HslCommunication.Algorithms.ConnectPool.ConnectPool<IMqttSyncConnector>(() => new IMqttSyncConnector { SyncClient = client });
            this.mqttConnectPool.MaxConnector = 0x7fffffff;
        }

        public MqttSyncClientPool(MqttConnectionOptions options, Action<MqttSyncClient> initialize)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                throw new Exception(StringResources.Language.InsufficientPrivileges);
            }
            MqttSyncClient client = new MqttSyncClient(options);
            initialize(client);
            this.mqttConnectPool = new HslCommunication.Algorithms.ConnectPool.ConnectPool<IMqttSyncConnector>(() => new IMqttSyncConnector { SyncClient = client });
            this.mqttConnectPool.MaxConnector = 0x7fffffff;
        }

        private OperateResult<T> ConnectPoolExecute<T>(Func<MqttSyncClient, OperateResult<T>> exec)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                throw new Exception(StringResources.Language.InsufficientPrivileges);
            }
            IMqttSyncConnector availableConnector = this.mqttConnectPool.GetAvailableConnector();
            OperateResult<T> result = exec(availableConnector.SyncClient);
            this.mqttConnectPool.ReturnConnector(availableConnector);
            return result;
        }

        private OperateResult<T1, T2> ConnectPoolExecute<T1, T2>(Func<MqttSyncClient, OperateResult<T1, T2>> exec)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                throw new Exception(StringResources.Language.InsufficientPrivileges);
            }
            IMqttSyncConnector availableConnector = this.mqttConnectPool.GetAvailableConnector();
            OperateResult<T1, T2> result = exec(availableConnector.SyncClient);
            this.mqttConnectPool.ReturnConnector(availableConnector);
            return result;
        }

        public OperateResult<string, byte[]> Read(string topic, byte[] payload, [Optional, DefaultParameterValue(null)] Action<long, long> sendProgress, [Optional, DefaultParameterValue(null)] Action<string, string> handleProgress, [Optional, DefaultParameterValue(null)] Action<long, long> receiveProgress)
        {
            return this.ConnectPoolExecute<string, byte[]>(m => m.Read(topic, payload, sendProgress, handleProgress, receiveProgress));
        }

        public OperateResult<string[]> ReadRetainTopics()
        {
            return this.ConnectPoolExecute<string[]>(m => m.ReadRetainTopics());
        }

        public OperateResult<MqttRpcApiInfo[]> ReadRpcApis()
        {
            return this.ConnectPoolExecute<MqttRpcApiInfo[]>(m => m.ReadRpcApis());
        }

        public OperateResult<string, string> ReadString(string topic, string payload, [Optional, DefaultParameterValue(null)] Action<long, long> sendProgress, [Optional, DefaultParameterValue(null)] Action<string, string> handleProgress, [Optional, DefaultParameterValue(null)] Action<long, long> receiveProgress)
        {
            return this.ConnectPoolExecute<string, string>(m => m.ReadString(topic, payload, sendProgress, handleProgress, receiveProgress));
        }

        public OperateResult<MqttClientApplicationMessage> ReadTopicPayload(string topic, [Optional, DefaultParameterValue(null)] Action<long, long> receiveProgress)
        {
            return this.ConnectPoolExecute<MqttClientApplicationMessage>(m => m.ReadTopicPayload(topic, receiveProgress));
        }

        public override string ToString()
        {
            return string.Format("MqttSyncClientPool[{0}]", this.mqttConnectPool.MaxConnector);
        }

        public HslCommunication.Algorithms.ConnectPool.ConnectPool<IMqttSyncConnector> GetMqttSyncConnectPool
        {
            get
            {
                return this.mqttConnectPool;
            }
        }

        public int MaxConnector
        {
            get
            {
                return this.mqttConnectPool.MaxConnector;
            }
            set
            {
                this.mqttConnectPool.MaxConnector = value;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MqttSyncClientPool.<>c <>9 = new MqttSyncClientPool.<>c();
            public static Func<MqttSyncClient, OperateResult<MqttRpcApiInfo[]>> <>9__11_0;
            public static Func<MqttSyncClient, OperateResult<string[]>> <>9__12_0;

            internal OperateResult<string[]> <ReadRetainTopics>b__12_0(MqttSyncClient m)
            {
                return m.ReadRetainTopics();
            }

            internal OperateResult<MqttRpcApiInfo[]> <ReadRpcApis>b__11_0(MqttSyncClient m)
            {
                return m.ReadRpcApis();
            }
        }
    }
}

