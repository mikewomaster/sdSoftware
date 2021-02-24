namespace HslCommunication.Core
{
    using System;
    using System.Threading;

    public sealed class HslAsyncCoordinator
    {
        private Action action = null;
        private int OperaterStatus = 0;
        private long Target = 0L;

        public HslAsyncCoordinator(Action operater)
        {
            this.action = operater;
        }

        public void StartOperaterInfomation()
        {
            Interlocked.Increment(ref this.Target);
            if (Interlocked.CompareExchange(ref this.OperaterStatus, 1, 0) == 0)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.ThreadPoolOperater), null);
            }
        }

        private void ThreadPoolOperater(object obj)
        {
            long num2;
            long target = this.Target;
            long num3 = 0L;
            do
            {
                num2 = target;
                if (this.action != null)
                {
                    Action action = this.action;
                    action();
                }
                else
                {
                    Action expressionStack_17_0 = this.action;
                }
                target = Interlocked.CompareExchange(ref this.Target, num3, num2);
            }
            while (num2 != target);
            Interlocked.Exchange(ref this.OperaterStatus, 0);
            if (this.Target != num3)
            {
                this.StartOperaterInfomation();
            }
        }

        public override string ToString()
        {
            return "HslAsyncCoordinator";
        }
    }
}

