namespace HslCommunication.Profinet.Keyence
{
    using HslCommunication;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;

    public class KeyenceSR2000SeriesTcp : NetworkDoubleBase, IKeyenceSR2000Series
    {
        public KeyenceSR2000SeriesTcp()
        {
            base.receiveTimeOut = 0x2710;
            base.SleepTime = 20;
        }

        public KeyenceSR2000SeriesTcp(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            base.receiveTimeOut = 0x2710;
            base.SleepTime = 20;
        }

        [HslMqttApi]
        public OperateResult<bool> CheckInput(int number)
        {
            return KeyenceSR2000Helper.CheckInput(number, new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        [HslMqttApi]
        public OperateResult CloseIndicator()
        {
            return KeyenceSR2000Helper.CloseIndicator(new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        [HslMqttApi]
        public OperateResult Lock()
        {
            return KeyenceSR2000Helper.Lock(new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        [HslMqttApi]
        public OperateResult OpenIndicator()
        {
            return KeyenceSR2000Helper.OpenIndicator(new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        [HslMqttApi]
        public OperateResult<string> ReadBarcode()
        {
            return KeyenceSR2000Helper.ReadBarcode(new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        [HslMqttApi]
        public OperateResult<string> ReadCommandState()
        {
            return KeyenceSR2000Helper.ReadCommandState(new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        [HslMqttApi]
        public OperateResult<string> ReadCustomer(string command)
        {
            return KeyenceSR2000Helper.ReadCustomer(command, new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        [HslMqttApi]
        public OperateResult<string> ReadErrorState()
        {
            return KeyenceSR2000Helper.ReadErrorState(new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        [HslMqttApi]
        public OperateResult<int[]> ReadRecord()
        {
            return KeyenceSR2000Helper.ReadRecord(new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        [HslMqttApi]
        public OperateResult<string> ReadVersion()
        {
            return KeyenceSR2000Helper.ReadVersion(new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        [HslMqttApi]
        public OperateResult Reset()
        {
            return KeyenceSR2000Helper.Reset(new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        [HslMqttApi]
        public OperateResult SetOutput(int number, bool value)
        {
            return KeyenceSR2000Helper.SetOutput(number, value, new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        public override string ToString()
        {
            return string.Format("KeyenceSR2000SeriesTcp[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi]
        public OperateResult UnLock()
        {
            return KeyenceSR2000Helper.UnLock(new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }
    }
}

