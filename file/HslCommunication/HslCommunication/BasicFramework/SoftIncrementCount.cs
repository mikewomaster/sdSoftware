namespace HslCommunication.BasicFramework
{
    using HslCommunication.Core;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public sealed class SoftIncrementCount : IDisposable
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <IncreaseTick>k__BackingField = 1;
        private long current = 0L;
        private bool disposedValue = false;
        private SimpleHybirdLock hybirdLock;
        private long max = 0x7fffffffffffffffL;
        private long start = 0L;

        public SoftIncrementCount(long max, [Optional, DefaultParameterValue(0L)] long start, [Optional, DefaultParameterValue(1)] int tick)
        {
            this.start = start;
            this.max = max;
            this.current = start;
            this.IncreaseTick = tick;
            this.hybirdLock = new SimpleHybirdLock();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.hybirdLock.Dispose();
                }
                this.disposedValue = true;
            }
        }

        public long GetCurrentValue()
        {
            long current = 0L;
            this.hybirdLock.Enter();
            current = this.current;
            this.current += this.IncreaseTick;
            if (this.current > this.max)
            {
                this.current = this.start;
            }
            else if (this.current < this.start)
            {
                this.current = this.max;
            }
            this.hybirdLock.Leave();
            return current;
        }

        public void ResetCurrentValue()
        {
            this.hybirdLock.Enter();
            this.current = this.start;
            this.hybirdLock.Leave();
        }

        public void ResetCurrentValue(long value)
        {
            this.hybirdLock.Enter();
            if (value > this.max)
            {
                this.current = this.max;
            }
            else if (value < this.start)
            {
                this.current = this.start;
            }
            else
            {
                this.current = value;
            }
            this.hybirdLock.Leave();
        }

        public void ResetMaxValue(long max)
        {
            this.hybirdLock.Enter();
            if (max > this.start)
            {
                if (max < this.current)
                {
                    this.current = this.start;
                }
                this.max = max;
            }
            this.hybirdLock.Leave();
        }

        public void ResetStartValue(long start)
        {
            this.hybirdLock.Enter();
            if (start < this.max)
            {
                if (this.current < start)
                {
                    this.current = start;
                }
                this.start = start;
            }
            this.hybirdLock.Leave();
        }

        public override string ToString()
        {
            return string.Format("SoftIncrementCount[{0}]", this.current);
        }

        public int IncreaseTick { get; set; }

        public long MaxValue
        {
            get
            {
                return this.max;
            }
        }
    }
}

