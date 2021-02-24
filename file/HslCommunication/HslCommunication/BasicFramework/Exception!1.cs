namespace HslCommunication.BasicFramework
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public sealed class Exception<TExceptionArgs> : Exception, ISerializable where TExceptionArgs: ExceptionArgs
    {
        private const string c_args = "Args";
        private readonly TExceptionArgs m_args;

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.SerializationFormatter)]
        private Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.m_args = (TExceptionArgs) info.GetValue("Args", typeof(TExceptionArgs));
        }

        public Exception([Optional, DefaultParameterValue(null)] string message, [Optional, DefaultParameterValue(null)] Exception innerException) : this(default(TExceptionArgs), message, innerException)
        {
        }

        public Exception(TExceptionArgs args, [Optional, DefaultParameterValue(null)] string message, [Optional, DefaultParameterValue(null)] Exception innerException) : base(message, innerException)
        {
            this.m_args = args;
        }

        public override bool Equals(object obj)
        {
            Exception<TExceptionArgs> exception = obj as Exception<TExceptionArgs>;
            if (exception == null)
            {
                return false;
            }
            return (object.Equals(this.m_args, exception.m_args) && base.Equals(obj));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Args", this.m_args);
            base.GetObjectData(info, context);
        }

        public TExceptionArgs Args
        {
            get
            {
                return this.m_args;
            }
        }

        public override string Message
        {
            get
            {
                string message = base.Message;
                return ((this.m_args == null) ? message : (message + " (" + this.m_args.Message + ")"));
            }
        }
    }
}

