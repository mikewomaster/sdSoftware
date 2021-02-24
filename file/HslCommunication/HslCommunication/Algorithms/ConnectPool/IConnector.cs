namespace HslCommunication.Algorithms.ConnectPool
{
    using System;

    public interface IConnector
    {
        void Close();
        void Open();

        string GuidToken { get; set; }

        bool IsConnectUsing { get; set; }

        DateTime LastUseTime { get; set; }
    }
}

