namespace HslCommunication.LogNet
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class HslMessageItem
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <Cancel>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private HslMessageDegree <Degree>k__BackingField = HslMessageDegree.DEBUG;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long <Id>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <KeyWord>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Text>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <ThreadId>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <Time>k__BackingField;
        private static long IdNumber = 0L;

        public HslMessageItem()
        {
            this.Id = Interlocked.Increment(ref IdNumber);
            this.Time = DateTime.Now;
        }

        public override string ToString()
        {
            if (this.Degree != HslMessageDegree.None)
            {
                if (string.IsNullOrEmpty(this.KeyWord))
                {
                    return string.Format("[{0}] {1:yyyy-MM-dd HH:mm:ss.fff} Thread [{2:D3}] {3}", new object[] { LogNetManagment.GetDegreeDescription(this.Degree), this.Time, this.ThreadId, this.Text });
                }
                return string.Format("[{0}] {1:yyyy-MM-dd HH:mm:ss.fff} Thread [{2:D3}] {3} : {4}", new object[] { LogNetManagment.GetDegreeDescription(this.Degree), this.Time, this.ThreadId, this.KeyWord, this.Text });
            }
            return this.Text;
        }

        public string ToStringWithoutKeyword()
        {
            if (this.Degree != HslMessageDegree.None)
            {
                return string.Format("[{0}] {1:yyyy-MM-dd HH:mm:ss.fff} Thread [{2:D3}] {3}", new object[] { LogNetManagment.GetDegreeDescription(this.Degree), this.Time, this.ThreadId, this.Text });
            }
            return this.Text;
        }

        public bool Cancel { get; set; }

        public HslMessageDegree Degree { get; set; }

        public long Id { get; private set; }

        public string KeyWord { get; set; }

        public string Text { get; set; }

        public int ThreadId { get; set; }

        public DateTime Time { get; set; }
    }
}

