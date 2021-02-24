namespace HslCommunication.Core.IMessage
{
    using System;

    public interface INetMessage
    {
        bool CheckHeadBytesLegal(byte[] token);
        int GetContentLengthByHeadBytes();
        int GetHeadBytesIdentity();

        byte[] ContentBytes { get; set; }

        byte[] HeadBytes { get; set; }

        int ProtocolHeadBytesLength { get; }

        byte[] SendBytes { get; set; }
    }
}

