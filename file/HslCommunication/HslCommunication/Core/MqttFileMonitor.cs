namespace HslCommunication.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MqttFileMonitor
    {
        private object dicLock = new object();
        private Dictionary<long, MqttFileMonitorItem> fileMonitors = new Dictionary<long, MqttFileMonitorItem>();

        public void Add(MqttFileMonitorItem monitorItem)
        {
            object dicLock = this.dicLock;
            lock (dicLock)
            {
                if (this.fileMonitors.ContainsKey(monitorItem.UniqueId))
                {
                    this.fileMonitors[monitorItem.UniqueId] = monitorItem;
                }
                else
                {
                    this.fileMonitors.Add(monitorItem.UniqueId, monitorItem);
                }
            }
        }

        public MqttFileMonitorItem[] GetMonitorItemsSnapShoot()
        {
            object dicLock = this.dicLock;
            lock (dicLock)
            {
                return this.fileMonitors.Values.ToArray<MqttFileMonitorItem>();
            }
        }

        public void Remove(long uniqueId)
        {
            object dicLock = this.dicLock;
            lock (dicLock)
            {
                if (this.fileMonitors.ContainsKey(uniqueId))
                {
                    this.fileMonitors.Remove(uniqueId);
                }
            }
        }
    }
}

