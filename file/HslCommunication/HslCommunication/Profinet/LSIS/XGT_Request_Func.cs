namespace HslCommunication.Profinet.LSIS
{
    using System;

    public enum XGT_Request_Func
    {
        Read = 0x54,
        ReadResponse = 0x55,
        Write = 0x58,
        WriteResponse = 0x59
    }
}

