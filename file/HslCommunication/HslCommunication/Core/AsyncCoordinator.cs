namespace HslCommunication.Core
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;

    internal sealed class AsyncCoordinator
    {
        private Action<CoordinationStatus> m_callback;
        private int m_opCount = 1;
        private int m_statusReported = 0;
        private Timer m_timer;

        public void AboutToBegin([Optional, DefaultParameterValue(1)] int opsToAdd)
        {
            Interlocked.Add(ref this.m_opCount, opsToAdd);
        }

        public void AllBegun(Action<CoordinationStatus> callback, [Optional, DefaultParameterValue(-1)] int timeout)
        {
            this.m_callback = callback;
            if (timeout != -1)
            {
                this.m_timer = new Timer(new TimerCallback(this.TimeExpired), null, timeout, -1);
            }
            this.JustEnded();
        }

        public void Cancel()
        {
            this.ReportStatus(CoordinationStatus.Cancel);
        }

        public void JustEnded()
        {
            if (Interlocked.Decrement(ref this.m_opCount) == 0)
            {
                this.ReportStatus(CoordinationStatus.AllDone);
            }
        }

        public static int Maxinum(ref int target, Func<int, int> change)
        {
            int num2;
            int num3;
            int num = target;
            do
            {
                num2 = num;
                num3 = change(num2);
                num = Interlocked.CompareExchange(ref target, num3, num2);
            }
            while (num2 != num);
            return num3;
        }

        private void ReportStatus(CoordinationStatus status)
        {
            if (Interlocked.Exchange(ref this.m_statusReported, 1) == 0)
            {
                this.m_callback(status);
            }
        }

        private void TimeExpired(object o)
        {
            this.ReportStatus(CoordinationStatus.Timeout);
        }
    }
}

