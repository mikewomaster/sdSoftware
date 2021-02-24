namespace HslCommunication.Profinet.Knx
{
    using HslCommunication.LogNet;
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class KnxUdp
    {
        private IPEndPoint _localEndpoint;
        private IPEndPoint _rouEndpoint;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <Channel>k__BackingField;
        private HslCommunication.Profinet.Knx.KnxCode KNX_CODE = new HslCommunication.Profinet.Knx.KnxCode();
        private ILogNet logNet;
        private const int stateRequestTimerInterval = 0xea60;
        private UdpClient udpClient;

        public void ConnectKnx()
        {
            if (this.udpClient == null)
            {
                UdpClient client1 = new UdpClient(this.LocalEndpoint) {
                    Client = { 
                        DontFragment = true,
                        SendBufferSize = 0,
                        ReceiveTimeout = 0x1d4c0
                    }
                };
                this.udpClient = client1;
            }
            int num = this.udpClient.Send(this.KNX_CODE.Handshake(this.LocalEndpoint), 0x1a, this.RouEndpoint);
            this.udpClient.BeginReceive(new AsyncCallback(this.ReceiveCallback), null);
            Thread.Sleep(0x3e8);
            if (this.KNX_CODE.IsConnect)
            {
                this.KNX_CODE.Return_data_msg += new HslCommunication.Profinet.Knx.KnxCode.ReturnData(this.KNX_CODE_Return_data_msg);
                this.KNX_CODE.GetData_msg += new HslCommunication.Profinet.Knx.KnxCode.GetData(this.KNX_CODE_GetData_msg);
                this.KNX_CODE.Set_knx_data += new HslCommunication.Profinet.Knx.KnxCode.ReturnData(this.KNX_CODE_Set_knx_data);
                this.KNX_CODE.knx_server_is_real(this.LocalEndpoint);
            }
        }

        public void DisConnectKnx()
        {
            if (this.KNX_CODE.Channel > 0)
            {
                byte[] dgram = this.KNX_CODE.Disconnect_knx(this.KNX_CODE.Channel, this.LocalEndpoint);
                this.udpClient.Send(dgram, dgram.Length, this.RouEndpoint);
            }
        }

        public void KeepConnection()
        {
            this.KNX_CODE.knx_server_is_real(this.LocalEndpoint);
        }

        private void KNX_CODE_GetData_msg(short addr, byte len, byte[] data)
        {
            if (this.logNet != null)
            {
                this.logNet.WriteDebug("收到数据 地址：" + addr.ToString() + " 长度:" + len.ToString() + "数据：" + BitConverter.ToString(data));
            }
            else
            {
                ILogNet logNet = this.logNet;
            }
        }

        private void KNX_CODE_Return_data_msg(byte[] data)
        {
            this.udpClient.Send(data, data.Length, this.RouEndpoint);
        }

        private void KNX_CODE_Set_knx_data(byte[] data)
        {
            this.udpClient.Send(data, data.Length, this.RouEndpoint);
        }

        public void ReadKnxData(short addr)
        {
            this.KNX_CODE.knx_server_is_real(this.LocalEndpoint);
            this.KNX_CODE.Knx_Resd_step1(addr);
        }

        private void ReceiveCallback(IAsyncResult iar)
        {
            byte[] buffer = this.udpClient.EndReceive(iar, ref this._rouEndpoint);
            if (this.logNet != null)
            {
                this.logNet.WriteDebug("收到报文 {0}", BitConverter.ToString(buffer));
            }
            else
            {
                ILogNet logNet = this.logNet;
            }
            this.KNX_CODE.KNX_check(buffer);
            if (this.KNX_CODE.IsConnect)
            {
                this.udpClient.BeginReceive(new AsyncCallback(this.ReceiveCallback), null);
            }
        }

        public void SetKnxData(short addr, byte len, byte[] data)
        {
            this.KNX_CODE.Knx_Write(addr, len, data);
        }

        public byte Channel { get; set; }

        public bool IsConnect
        {
            get
            {
                return this.KNX_CODE.IsConnect;
            }
        }

        public HslCommunication.Profinet.Knx.KnxCode KnxCode
        {
            get
            {
                return this.KNX_CODE;
            }
        }

        public IPEndPoint LocalEndpoint
        {
            get
            {
                return this._localEndpoint;
            }
            set
            {
                this._localEndpoint = value;
            }
        }

        public ILogNet LogNet
        {
            get
            {
                return this.logNet;
            }
            set
            {
                this.logNet = value;
            }
        }

        public IPEndPoint RouEndpoint
        {
            get
            {
                return this._rouEndpoint;
            }
            set
            {
                this._rouEndpoint = value;
            }
        }
    }
}

