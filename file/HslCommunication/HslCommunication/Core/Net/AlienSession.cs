namespace HslCommunication.Core.Net
{
    using HslCommunication.BasicFramework;
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class AlienSession
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <DTU>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsStatusOk>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <OfflineTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <OnlineTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Pwd>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private System.Net.Sockets.Socket <Socket>k__BackingField;

        public AlienSession()
        {
            this.IsStatusOk = true;
            this.OnlineTime = DateTime.Now;
            this.OfflineTime = DateTime.MinValue;
        }

        public void Offline()
        {
            if (this.IsStatusOk)
            {
                this.IsStatusOk = false;
                this.OfflineTime = DateTime.Now;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DtuSession[" + this.DTU + "] [" + (this.IsStatusOk ? "Online" : "Offline") + "]");
            if (this.IsStatusOk)
            {
                builder.Append(" [" + SoftBasic.GetTimeSpanDescription((TimeSpan) (DateTime.Now - this.OnlineTime)) + "]");
            }
            else if (this.OfflineTime == DateTime.MinValue)
            {
                builder.Append(" [----]");
            }
            else
            {
                builder.Append(" [" + SoftBasic.GetTimeSpanDescription((TimeSpan) (DateTime.Now - this.OfflineTime)) + "]");
            }
            return builder.ToString();
        }

        public string DTU { get; set; }

        public bool IsStatusOk { get; set; }

        public DateTime OfflineTime { get; set; }

        public DateTime OnlineTime { get; set; }

        public string Pwd { get; set; }

        public System.Net.Sockets.Socket Socket { get; set; }
    }
}

