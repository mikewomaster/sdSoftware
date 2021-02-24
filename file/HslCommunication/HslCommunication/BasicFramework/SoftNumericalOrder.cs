namespace HslCommunication.BasicFramework
{
    using HslCommunication.Core;
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;

    public sealed class SoftNumericalOrder : SoftFileSaveBase
    {
        private HslAsyncCoordinator AsyncCoordinator = null;
        private long CurrentIndex = 0L;
        private int NumberLength = 5;
        private string TextHead = string.Empty;
        private string TimeFormate = "yyyyMMdd";

        public SoftNumericalOrder(string textHead, string timeFormate, int numberLength, string fileSavePath)
        {
            base.LogHeaderText = "SoftNumericalOrder";
            this.TextHead = textHead;
            this.TimeFormate = timeFormate;
            this.NumberLength = numberLength;
            base.FileSavePath = fileSavePath;
            this.LoadByFile();
            this.AsyncCoordinator = new HslAsyncCoordinator(delegate {
                if (!string.IsNullOrEmpty(base.FileSavePath))
                {
                    using (StreamWriter writer = new StreamWriter(base.FileSavePath, false, Encoding.Default))
                    {
                        writer.Write(this.CurrentIndex);
                    }
                }
            });
        }

        public void ClearNumericalOrder()
        {
            Interlocked.Exchange(ref this.CurrentIndex, 0L);
            this.AsyncCoordinator.StartOperaterInfomation();
        }

        public long GetLongOrder()
        {
            long num = Interlocked.Increment(ref this.CurrentIndex);
            this.AsyncCoordinator.StartOperaterInfomation();
            return num;
        }

        public string GetNumericalOrder()
        {
            long num = Interlocked.Increment(ref this.CurrentIndex);
            this.AsyncCoordinator.StartOperaterInfomation();
            if (string.IsNullOrEmpty(this.TimeFormate))
            {
                return (this.TextHead + num.ToString().PadLeft(this.NumberLength, '0'));
            }
            return (this.TextHead + DateTime.Now.ToString(this.TimeFormate) + num.ToString().PadLeft(this.NumberLength, '0'));
        }

        public string GetNumericalOrder(string textHead)
        {
            long num = Interlocked.Increment(ref this.CurrentIndex);
            this.AsyncCoordinator.StartOperaterInfomation();
            if (string.IsNullOrEmpty(this.TimeFormate))
            {
                return (textHead + num.ToString().PadLeft(this.NumberLength, '0'));
            }
            return (textHead + DateTime.Now.ToString(this.TimeFormate) + num.ToString().PadLeft(this.NumberLength, '0'));
        }

        public override void LoadByString(string content)
        {
            this.CurrentIndex = Convert.ToInt64(content);
        }

        public override string ToSaveString()
        {
            return this.CurrentIndex.ToString();
        }
    }
}

