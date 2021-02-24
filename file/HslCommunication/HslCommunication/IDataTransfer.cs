namespace HslCommunication
{
    using System;

    public interface IDataTransfer
    {
        void ParseSource(byte[] Content);
        byte[] ToSource();

        ushort ReadCount { get; }
    }
}

