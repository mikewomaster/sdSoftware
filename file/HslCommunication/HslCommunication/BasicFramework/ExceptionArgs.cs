namespace HslCommunication.BasicFramework
{
    using System;

    [Serializable]
    public abstract class ExceptionArgs
    {
        protected ExceptionArgs()
        {
        }

        public virtual string Message
        {
            get
            {
                return string.Empty;
            }
        }
    }
}

