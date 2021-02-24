namespace HslCommunication.Core
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading;

    public sealed class HslReadWriteLock : IDisposable
    {
        private const int c_ls1ReaderReading = 8;
        private const int c_ls1ReaderWaiting = 0x1000;
        private const int c_ls1WriterWaiting = 0x200000;
        private const int c_lsAnyWaitingMask = 0x3ffff000;
        private const int c_lsReadersReadingMask = 0xff8;
        private const int c_lsReadersReadingStartBit = 3;
        private const int c_lsReadersWaitingMask = 0x1ff000;
        private const int c_lsReadersWaitingStartBit = 12;
        private const int c_lsStateMask = 7;
        private const int c_lsStateStartBit = 0;
        private const int c_lsWritersWaitingMask = 0x3fe00000;
        private const int c_lsWritersWaitingStartBit = 0x15;
        private bool disposedValue = false;
        private bool m_exclusive;
        private int m_LockState = 0;
        private Semaphore m_ReadersLock = new Semaphore(0, 0x7fffffff);
        private Semaphore m_WritersLock = new Semaphore(0, 0x7fffffff);

        private static void AddReadersReading(ref int ls, int amount)
        {
            ls += 8 * amount;
        }

        private static void AddReadersWaiting(ref int ls, int amount)
        {
            ls += 0x1000 * amount;
        }

        private static void AddWritersWaiting(ref int ls, int amount)
        {
            ls += 0x200000 * amount;
        }

        private static bool AnyWaiters(int ls)
        {
            return ((ls & 0x3ffff000) > 0);
        }

        private static string DebugState(int ls)
        {
            object[] args = new object[] { State(ls), NumReadersReading(ls), NumReadersWaiting(ls), NumWritersWaiting(ls) };
            return string.Format(CultureInfo.InvariantCulture, "State={0}, RR={1}, RW={2}, WW={3}", args);
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
                }
                this.m_WritersLock.Close();
                this.m_WritersLock = null;
                this.m_ReadersLock.Close();
                this.m_ReadersLock = null;
                this.disposedValue = true;
            }
        }

        private static int DoneReading(ref int target)
        {
            int num;
            int num3;
            int num2 = target;
            do
            {
                int ls = num = num2;
                AddReadersReading(ref ls, -1);
                if (NumReadersReading(ls) > 0)
                {
                    num3 = 0;
                }
                else if (!AnyWaiters(ls))
                {
                    SetState(ref ls, OneManyLockStates.Free);
                    num3 = 0;
                }
                else
                {
                    Debug.Assert(NumWritersWaiting(ls) > 0);
                    SetState(ref ls, OneManyLockStates.ReservedForWriter);
                    AddWritersWaiting(ref ls, -1);
                    num3 = -1;
                }
                num2 = Interlocked.CompareExchange(ref target, ls, num);
            }
            while (num != num2);
            return num3;
        }

        private static int DoneWriting(ref int target)
        {
            int num;
            int num2 = target;
            int num3 = 0;
            do
            {
                int ls = num = num2;
                if (!AnyWaiters(ls))
                {
                    SetState(ref ls, OneManyLockStates.Free);
                    num3 = 0;
                }
                else if (NumWritersWaiting(ls) > 0)
                {
                    SetState(ref ls, OneManyLockStates.ReservedForWriter);
                    AddWritersWaiting(ref ls, -1);
                    num3 = -1;
                }
                else
                {
                    num3 = NumReadersWaiting(ls);
                    Debug.Assert(num3 > 0);
                    SetState(ref ls, OneManyLockStates.OwnedByReaders);
                    AddReadersWaiting(ref ls, -num3);
                }
                num2 = Interlocked.CompareExchange(ref target, ls, num);
            }
            while (num != num2);
            return num3;
        }

        public void Enter(bool exclusive)
        {
            if (!exclusive)
            {
                while (WaitToRead(ref this.m_LockState))
                {
                    this.m_ReadersLock.WaitOne();
                }
            }
            else
            {
                while (WaitToWrite(ref this.m_LockState))
                {
                    this.m_WritersLock.WaitOne();
                }
            }
            this.m_exclusive = exclusive;
        }

        public void Leave()
        {
            int num;
            if (this.m_exclusive)
            {
                Debug.Assert((State(this.m_LockState) == OneManyLockStates.OwnedByWriter) && (NumReadersReading(this.m_LockState) == 0));
                num = DoneWriting(ref this.m_LockState);
            }
            else
            {
                OneManyLockStates states = State(this.m_LockState);
                Debug.Assert((State(this.m_LockState) == OneManyLockStates.OwnedByReaders) || (State(this.m_LockState) == OneManyLockStates.OwnedByReadersAndWriterPending));
                num = DoneReading(ref this.m_LockState);
            }
            if (num == -1)
            {
                this.m_WritersLock.Release();
            }
            else if (num > 0)
            {
                this.m_ReadersLock.Release(num);
            }
        }

        private static int NumReadersReading(int ls)
        {
            return ((ls & 0xff8) >> 3);
        }

        private static int NumReadersWaiting(int ls)
        {
            return ((ls & 0x1ff000) >> 12);
        }

        private static int NumWritersWaiting(int ls)
        {
            return ((ls & 0x3fe00000) >> 0x15);
        }

        private static void SetState(ref int ls, OneManyLockStates newState)
        {
            ls = (ls & -8) | newState;
        }

        private static OneManyLockStates State(int ls)
        {
            return (((OneManyLockStates) ls) & (OneManyLockStates.ReservedForWriter | OneManyLockStates.OwnedByReadersAndWriterPending));
        }

        public override string ToString()
        {
            return DebugState(this.m_LockState);
        }

        private static bool WaitToRead(ref int target)
        {
            int num;
            bool flag;
            int num2 = target;
            do
            {
                int ls = num = num2;
                flag = false;
                switch (State(ls))
                {
                    case OneManyLockStates.Free:
                        SetState(ref ls, OneManyLockStates.OwnedByReaders);
                        AddReadersReading(ref ls, 1);
                        break;

                    case OneManyLockStates.OwnedByWriter:
                    case OneManyLockStates.OwnedByReadersAndWriterPending:
                    case OneManyLockStates.ReservedForWriter:
                        AddReadersWaiting(ref ls, 1);
                        flag = true;
                        break;

                    case OneManyLockStates.OwnedByReaders:
                        AddReadersReading(ref ls, 1);
                        break;

                    default:
                        Debug.Assert(false, "Invalid Lock state");
                        break;
                }
                num2 = Interlocked.CompareExchange(ref target, ls, num);
            }
            while (num != num2);
            return flag;
        }

        private static bool WaitToWrite(ref int target)
        {
            int num;
            bool flag;
            int num2 = target;
            do
            {
                num = num2;
                int ls = num;
                flag = false;
                switch (State(ls))
                {
                    case OneManyLockStates.Free:
                    case OneManyLockStates.ReservedForWriter:
                        SetState(ref ls, OneManyLockStates.OwnedByWriter);
                        break;

                    case OneManyLockStates.OwnedByWriter:
                        AddWritersWaiting(ref ls, 1);
                        flag = true;
                        break;

                    case OneManyLockStates.OwnedByReaders:
                    case OneManyLockStates.OwnedByReadersAndWriterPending:
                        SetState(ref ls, OneManyLockStates.OwnedByReadersAndWriterPending);
                        AddWritersWaiting(ref ls, 1);
                        flag = true;
                        break;

                    default:
                        Debug.Assert(false, "Invalid Lock state");
                        break;
                }
                num2 = Interlocked.CompareExchange(ref target, ls, num);
            }
            while (num != num2);
            return flag;
        }

        private enum OneManyLockStates
        {
            Free,
            OwnedByWriter,
            OwnedByReaders,
            OwnedByReadersAndWriterPending,
            ReservedForWriter
        }
    }
}

