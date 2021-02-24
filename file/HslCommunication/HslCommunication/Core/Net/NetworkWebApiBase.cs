namespace HslCommunication.Core.Net
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.LogNet;
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class NetworkWebApiBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ILogNet <LogNet>k__BackingField;
        private string ipAddress;
        private string name;
        private string password;
        private int port;

        public NetworkWebApiBase(string ipAddress)
        {
            this.ipAddress = "127.0.0.1";
            this.port = 80;
            this.name = string.Empty;
            this.password = string.Empty;
            this.ipAddress = ipAddress;
        }

        public NetworkWebApiBase(string ipAddress, int port)
        {
            this.ipAddress = "127.0.0.1";
            this.port = 80;
            this.name = string.Empty;
            this.password = string.Empty;
            this.ipAddress = ipAddress;
            this.port = port;
        }

        public NetworkWebApiBase(string ipAddress, int port, string name, string password)
        {
            this.ipAddress = "127.0.0.1";
            this.port = 80;
            this.name = string.Empty;
            this.password = string.Empty;
            this.ipAddress = HslHelper.GetIpAddressFromInput(ipAddress);
            this.port = port;
            this.name = name;
            this.password = password;
        }

        public virtual OperateResult<byte[]> Read(string address)
        {
            OperateResult<string> result = this.ReadString(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.UTF8.GetBytes(result.Content));
        }

        protected virtual OperateResult<string> ReadByAddress(string address)
        {
            return new OperateResult<string>(StringResources.Language.NotSupportedFunction);
        }

        public virtual OperateResult<string> ReadString(string address)
        {
            if (!HslCommunication.Authorization.nzugaydgwadawdibbas())
            {
                return new OperateResult<string>(StringResources.Language.AuthorizationFailed);
            }
            if (address.StartsWith("url=") || address.StartsWith("URL="))
            {
                address = address.Substring(4);
                string str = string.Format("http://{0}:{1}/{2}", this.ipAddress, this.port, address.StartsWith("/") ? address.Substring(1) : address);
                try
                {
                    WebClient client = new WebClient();
                    if (!string.IsNullOrEmpty(this.name))
                    {
                        client.Credentials = new NetworkCredential(this.name, this.password);
                    }
                    byte[] bytes = client.DownloadData(str);
                    client.Dispose();
                    return OperateResult.CreateSuccessResult<string>(Encoding.UTF8.GetString(bytes));
                }
                catch (Exception exception)
                {
                    return new OperateResult<string>(exception.Message);
                }
            }
            return this.ReadByAddress(address);
        }

        public override string ToString()
        {
            return string.Format("NetworkWebApiBase[{0}:{1}]", this.ipAddress, this.port);
        }

        public virtual OperateResult Write(string address, byte[] value)
        {
            return this.Write(address, Encoding.Default.GetString(value));
        }

        public virtual OperateResult Write(string address, string value)
        {
            if (address.StartsWith("url=") || address.StartsWith("URL="))
            {
                address = address.Substring(4);
                string str = string.Format("http://{0}:{1}/{2}", this.ipAddress, this.port, address.StartsWith("/") ? address.Substring(1) : address);
                try
                {
                    WebClient client = new WebClient {
                        Proxy = null
                    };
                    if (!string.IsNullOrEmpty(this.name))
                    {
                        client.Credentials = new NetworkCredential(this.name, this.password);
                    }
                    byte[] bytes = client.UploadData(str, Encoding.UTF8.GetBytes(value));
                    client.Dispose();
                    return OperateResult.CreateSuccessResult<string>(Encoding.UTF8.GetString(bytes));
                }
                catch (Exception exception)
                {
                    return new OperateResult<string>(exception.Message);
                }
            }
            return new OperateResult<string>(StringResources.Language.NotSupportedFunction);
        }

        public string IpAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                this.ipAddress = value;
            }
        }

        public ILogNet LogNet { get; set; }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public int Port
        {
            get
            {
                return this.port;
            }
            set
            {
                this.port = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
    }
}

