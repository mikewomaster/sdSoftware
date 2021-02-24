namespace HslCommunication.Profinet.Keyence
{
    using HslCommunication;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;

    public class KeyenceSR2000Serial : SerialBase, IKeyenceSR2000Series
    {
        public KeyenceSR2000Serial()
        {
            base.ReceiveTimeout = 0x2710;
            base.SleepTime = 20;
        }

        [HslMqttApi]
        public OperateResult<bool> CheckInput(int number)
        {
            return KeyenceSR2000Helper.CheckInput(number, new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        [HslMqttApi]
        public OperateResult CloseIndicator()
        {
            return KeyenceSR2000Helper.CloseIndicator(new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        [HslMqttApi]
        public OperateResult Lock()
        {
            return KeyenceSR2000Helper.Lock(new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        [HslMqttApi]
        public OperateResult OpenIndicator()
        {
            return KeyenceSR2000Helper.OpenIndicator(new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        [HslMqttApi]
        public OperateResult<string> ReadBarcode()
        {
            return KeyenceSR2000Helper.ReadBarcode(new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        [HslMqttApi]
        public OperateResult<string> ReadCommandState()
        {
            return KeyenceSR2000Helper.ReadCommandState(new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        [HslMqttApi]
        public OperateResult<string> ReadCustomer(string command)
        {
            return KeyenceSR2000Helper.ReadCustomer(command, new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        [HslMqttApi]
        public OperateResult<string> ReadErrorState()
        {
            return KeyenceSR2000Helper.ReadErrorState(new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        [HslMqttApi]
        public OperateResult<int[]> ReadRecord()
        {
            return KeyenceSR2000Helper.ReadRecord(new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        [HslMqttApi]
        public OperateResult<string> ReadVersion()
        {
            return KeyenceSR2000Helper.ReadVersion(new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        [HslMqttApi]
        public OperateResult Reset()
        {
            return KeyenceSR2000Helper.Reset(new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        [HslMqttApi]
        public OperateResult SetOutput(int number, bool value)
        {
            return KeyenceSR2000Helper.SetOutput(number, value, new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }

        public override string ToString()
        {
            return string.Format("KeyenceSR2000Serial[{0}:{1}]", base.PortName, base.BaudRate);
        }

        [HslMqttApi]
        public OperateResult UnLock()
        {
            return KeyenceSR2000Helper.UnLock(new Func<byte[], OperateResult<byte[]>>(this.ReadBase));
        }
    }
}

