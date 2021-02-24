namespace HslCommunication.Algorithms.ConnectPool
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class ConnectPool<TConnector> where TConnector: IConnector
    {
        private bool canGetConnector;
        private List<TConnector> connectors;
        private Func<TConnector> CreateConnector;
        private int expireTime;
        private object listLock;
        private int maxConnector;
        private Timer timerCheck;
        private int usedConnector;
        private int usedConnectorMax;

        public ConnectPool(Func<TConnector> createConnector)
        {
            this.CreateConnector = null;
            this.maxConnector = 10;
            this.usedConnector = 0;
            this.usedConnectorMax = 0;
            this.expireTime = 30;
            this.canGetConnector = true;
            this.timerCheck = null;
            this.connectors = null;
            this.CreateConnector = createConnector;
            this.listLock = new object();
            this.connectors = new List<TConnector>();
            this.timerCheck = new Timer(new TimerCallback(this.TimerCheckBackground), null, 0x2710, 0x7530);
        }

        public TConnector GetAvailableConnector()
        {
            while (!this.canGetConnector)
            {
                Thread.Sleep(20);
            }
            TConnector item = default(TConnector);
            object listLock = this.listLock;
            lock (listLock)
            {
                for (int i = 0; i < this.connectors.Count; i++)
                {
                    TConnector local2 = this.connectors[i];
                    if (!local2.IsConnectUsing)
                    {
                        local2 = this.connectors[i];
                        local2.IsConnectUsing = true;
                        item = this.connectors[i];
                        break;
                    }
                }
                if (item == null)
                {
                    item = this.CreateConnector();
                    item.IsConnectUsing = true;
                    item.LastUseTime = DateTime.Now;
                    item.Open();
                    this.connectors.Add(item);
                    this.usedConnector = this.connectors.Count;
                    if (this.usedConnector > this.usedConnectorMax)
                    {
                        this.usedConnectorMax = this.usedConnector;
                    }
                    if (this.usedConnector == this.maxConnector)
                    {
                        this.canGetConnector = false;
                    }
                }
                item.LastUseTime = DateTime.Now;
            }
            return item;
        }

        public void ResetAllConnector()
        {
            object listLock = this.listLock;
            lock (listLock)
            {
                for (int i = this.connectors.Count - 1; i >= 0; i--)
                {
                    this.connectors[i].Close();
                    this.connectors.RemoveAt(i);
                }
            }
        }

        public void ReturnConnector(TConnector connector)
        {
            object listLock = this.listLock;
            lock (listLock)
            {
                int index = this.connectors.IndexOf(connector);
                if (index != -1)
                {
                    TConnector local = this.connectors[index];
                    local.IsConnectUsing = false;
                }
            }
        }

        private void TimerCheckBackground(object obj)
        {
            object listLock = this.listLock;
            lock (listLock)
            {
                for (int i = this.connectors.Count - 1; i >= 0; i--)
                {
                    TConnector local = this.connectors[i];
                    TimeSpan span = (TimeSpan) (DateTime.Now - local.LastUseTime);
                    if ((span.TotalSeconds > this.expireTime) && !(local = this.connectors[i]).IsConnectUsing)
                    {
                        this.connectors[i].Close();
                        this.connectors.RemoveAt(i);
                    }
                }
                this.usedConnector = this.connectors.Count;
                if (this.usedConnector < this.MaxConnector)
                {
                    this.canGetConnector = true;
                }
            }
        }

        public int ConectionExpireTime
        {
            get
            {
                return this.expireTime;
            }
            set
            {
                this.expireTime = value;
            }
        }

        public int MaxConnector
        {
            get
            {
                return this.maxConnector;
            }
            set
            {
                this.maxConnector = value;
            }
        }

        public int UseConnectorMax
        {
            get
            {
                return this.usedConnectorMax;
            }
        }

        public int UsedConnector
        {
            get
            {
                return this.usedConnector;
            }
        }
    }
}

