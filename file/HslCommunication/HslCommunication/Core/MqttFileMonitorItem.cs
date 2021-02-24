namespace HslCommunication.Core
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class MqttFileMonitorItem
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ClientId>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IPEndPoint <EndPoint>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <FileName>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Groups>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long <LastUpdateProgress>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <LastUpdateTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Operate>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long <SpeedSecond>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <StartTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long <TotalSize>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long <UniqueId>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <UserName>k__BackingField;
        private static long uniqueIdCreate = 0L;

        public MqttFileMonitorItem()
        {
            this.StartTime = DateTime.Now;
            this.LastUpdateTime = DateTime.Now;
            this.UniqueId = Interlocked.Increment(ref uniqueIdCreate);
        }

        public void UpdateProgress(long progress, long total)
        {
            this.TotalSize = total;
            TimeSpan span = (TimeSpan) (DateTime.Now - this.LastUpdateTime);
            if (span.TotalSeconds >= 0.2)
            {
                long num = progress - this.LastUpdateProgress;
                if (num <= 0L)
                {
                    this.SpeedSecond = 0L;
                }
                else
                {
                    this.SpeedSecond = (long) (((double) num) / span.TotalSeconds);
                    this.LastUpdateTime = DateTime.Now;
                    this.LastUpdateProgress = progress;
                }
            }
        }

        public string ClientId { get; set; }

        public IPEndPoint EndPoint { get; set; }

        public string FileName { get; set; }

        public string Groups { get; set; }

        public long LastUpdateProgress { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public string Operate { get; set; }

        public long SpeedSecond { get; set; }

        public DateTime StartTime { get; set; }

        public long TotalSize { get; set; }

        public long UniqueId { get; private set; }

        public string UserName { get; set; }
    }
}

