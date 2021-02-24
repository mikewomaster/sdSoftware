namespace HslCommunication.Core
{
    using System;
    using System.Threading;

    public sealed class SimpleHybirdLock : IDisposable
    {
        private bool disposedValue = false;
        private AutoResetEvent m_waiterLock = new AutoResetEvent(false);
        private int m_waiters = 0;
        private static long simpleHybirdLockCount = 0L;
        private static long simpleHybirdLockWaitCount = 0L;

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
                }
                this.m_waiterLock.Close();
                this.disposedValue = true;
            }
        }

        public void Enter()
        {
            Interlocked.Increment(ref simpleHybirdLockCount);
            if (Interlocked.Increment(ref this.m_waiters) != 1)
            {
                Interlocked.Increment(ref simpleHybirdLockWaitCount);
                this.m_waiterLock.WaitOne();
            }
        }

        public void Leave()
        {
            Interlocked.Decrement(ref simpleHybirdLockCount);
            if (Interlocked.Decrement(ref this.m_waiters) != 0)
            {
                Interlocked.Decrement(ref simpleHybirdLockWaitCount);
                this.m_waiterLock.Set();
            }
        }

        public bool IsWaitting
        {
            get
            {
                return (this.m_waiters > 0);
            }
        }

        public static long SimpleHybirdLockCount
        {
            get
            {
                return simpleHybirdLockCount;
            }
        }

        public static long SimpleHybirdLockWaitCount
        {
            get
            {
                return simpleHybirdLockWaitCount;
            }
        }
    }
}

