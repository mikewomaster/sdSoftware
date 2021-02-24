namespace HslCommunication.Core
{
    using System;

    public class RemoteCloseException : Exception
    {
        public RemoteCloseException() : base("Remote Closed Exception")
        {
        }
    }
}

