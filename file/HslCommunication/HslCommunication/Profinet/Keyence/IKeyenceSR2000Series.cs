namespace HslCommunication.Profinet.Keyence
{
    using HslCommunication;
    using HslCommunication.Reflection;
    using System;

    internal interface IKeyenceSR2000Series
    {
        [HslMqttApi]
        OperateResult<bool> CheckInput(int number);
        [HslMqttApi]
        OperateResult CloseIndicator();
        [HslMqttApi]
        OperateResult Lock();
        [HslMqttApi]
        OperateResult OpenIndicator();
        [HslMqttApi]
        OperateResult<string> ReadBarcode();
        [HslMqttApi]
        OperateResult<string> ReadCommandState();
        [HslMqttApi]
        OperateResult<string> ReadCustomer(string command);
        [HslMqttApi]
        OperateResult<string> ReadErrorState();
        [HslMqttApi]
        OperateResult<int[]> ReadRecord();
        [HslMqttApi]
        OperateResult<string> ReadVersion();
        [HslMqttApi]
        OperateResult Reset();
        [HslMqttApi]
        OperateResult SetOutput(int number, bool value);
        [HslMqttApi]
        OperateResult UnLock();
    }
}

