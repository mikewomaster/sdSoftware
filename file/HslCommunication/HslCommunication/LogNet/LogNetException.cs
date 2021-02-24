namespace HslCommunication.LogNet
{
    using System;

    public class LogNetException : Exception
    {
        public LogNetException(Exception innerException) : base(innerException.Message, innerException)
        {
        }
    }
}

