namespace HslCommunication.Core.Net
{
    using HslCommunication;
    using HslCommunication.LogNet;
    using System;

    public interface IRobotNet
    {
        OperateResult<byte[]> Read(string address);
        OperateResult<string> ReadString(string address);
        OperateResult Write(string address, byte[] value);
        OperateResult Write(string address, string value);

        ILogNet LogNet { get; set; }
    }
}

