namespace HslCommunication.LogNet
{
    using HslCommunication;
    using HslCommunication.Reflection;
    using System;

    public class LogStatisticsBase<T>
    {
        private int arrayLength;
        protected HslCommunication.LogNet.GenerateMode generateMode;
        private long lastDataMark;
        private object lockStatistics;
        private T[] statistics;

        public LogStatisticsBase(HslCommunication.LogNet.GenerateMode generateMode, int arrayLength)
        {
            this.statistics = null;
            this.generateMode = HslCommunication.LogNet.GenerateMode.ByEveryDay;
            this.arrayLength = 30;
            this.lastDataMark = -1L;
            this.generateMode = generateMode;
            this.arrayLength = arrayLength;
            this.statistics = new T[arrayLength];
            this.lastDataMark = this.GetDataMarkFromDateTime(DateTime.Now);
            this.lockStatistics = new object();
        }

        [HslMqttApi(Description="According to the specified time, get the data mark information specified at that time")]
        public long GetDataMarkFromDateTime(DateTime dateTime)
        {
            switch (this.generateMode)
            {
                case HslCommunication.LogNet.GenerateMode.ByEveryMinute:
                    return this.GetMinuteFromTime(dateTime);

                case HslCommunication.LogNet.GenerateMode.ByEveryHour:
                    return this.GetHourFromTime(dateTime);

                case HslCommunication.LogNet.GenerateMode.ByEveryDay:
                    return this.GetDayFromTime(dateTime);

                case HslCommunication.LogNet.GenerateMode.ByEveryWeek:
                    return this.GetWeekFromTime(dateTime);

                case HslCommunication.LogNet.GenerateMode.ByEveryMonth:
                    return this.GetMonthFromTime(dateTime);

                case HslCommunication.LogNet.GenerateMode.ByEverySeason:
                    return this.GetSeasonFromTime(dateTime);

                case HslCommunication.LogNet.GenerateMode.ByEveryYear:
                    return this.GetYearFromTime(dateTime);
            }
            return this.GetDayFromTime(dateTime);
        }

        [HslMqttApi(Description="Obtain the latest data mark information according to the time mode of current data statistics")]
        public long GetDataMarkFromTimeNow()
        {
            return this.GetDataMarkFromDateTime(DateTime.Now);
        }

        private long GetDayFromTime(DateTime dateTime)
        {
            TimeSpan span = (TimeSpan) (dateTime.Date - new DateTime(0x7b2, 1, 1));
            return (long) span.Days;
        }

        private long GetHourFromTime(DateTime dateTime)
        {
            TimeSpan span = (TimeSpan) (dateTime.Date - new DateTime(0x7b2, 1, 1));
            return ((span.Days * 0x18L) + dateTime.Hour);
        }

        private T[] GetLeftMoveTimes(int times)
        {
            if (times >= this.statistics.Length)
            {
                return new T[this.arrayLength];
            }
            T[] destinationArray = new T[this.arrayLength];
            Array.Copy(this.statistics, times, destinationArray, 0, this.statistics.Length - times);
            return destinationArray;
        }

        private long GetMinuteFromTime(DateTime dateTime)
        {
            TimeSpan span = (TimeSpan) (dateTime.Date - new DateTime(0x7b2, 1, 1));
            return ((((span.Days * 0x18L) * 60L) + (dateTime.Hour * 60)) + dateTime.Minute);
        }

        private long GetMonthFromTime(DateTime dateTime)
        {
            return (((dateTime.Year - 0x7b2) * 12L) + (dateTime.Month - 1));
        }

        private long GetSeasonFromTime(DateTime dateTime)
        {
            return (((dateTime.Year - 0x7b2) * 4L) + ((dateTime.Month - 1) / 3));
        }

        public OperateResult<long, T[]> GetStatisticsSnapAndDataMark()
        {
            object lockStatistics = this.lockStatistics;
            lock (lockStatistics)
            {
                long dataMarkFromDateTime = this.GetDataMarkFromDateTime(DateTime.Now);
                if (this.lastDataMark != dataMarkFromDateTime)
                {
                    int times = (int) (dataMarkFromDateTime - this.lastDataMark);
                    this.statistics = this.GetLeftMoveTimes(times);
                    this.lastDataMark = dataMarkFromDateTime;
                }
                return OperateResult.CreateSuccessResult<long, T[]>(dataMarkFromDateTime, this.statistics.CopyArray<T>());
            }
        }

        [HslMqttApi(Description="Get a data snapshot of the current statistics")]
        public T[] GetStatisticsSnapshot()
        {
            return this.GetStatisticsSnapAndDataMark().Content2;
        }

        [HslMqttApi(Description="Get a snapshot of statistical data information according to the specified time range")]
        public T[] GetStatisticsSnapshotByTime(DateTime start, DateTime finish)
        {
            if (finish <= start)
            {
                return new T[0];
            }
            object lockStatistics = this.lockStatistics;
            lock (lockStatistics)
            {
                long dataMarkFromDateTime = this.GetDataMarkFromDateTime(DateTime.Now);
                if (this.lastDataMark != dataMarkFromDateTime)
                {
                    int times = (int) (dataMarkFromDateTime - this.lastDataMark);
                    this.statistics = this.GetLeftMoveTimes(times);
                    this.lastDataMark = dataMarkFromDateTime;
                }
                long num2 = (dataMarkFromDateTime - this.statistics.Length) + 1L;
                long num3 = this.GetDataMarkFromDateTime(start);
                long num4 = this.GetDataMarkFromDateTime(finish);
                if (num3 < num2)
                {
                    num3 = num2;
                }
                if (num4 > dataMarkFromDateTime)
                {
                    num4 = dataMarkFromDateTime;
                }
                int index = (int) (num3 - num2);
                int num6 = (int) ((num4 - num3) + 1L);
                if (num3 == num4)
                {
                    return new T[] { this.statistics[index] };
                }
                T[] localArray2 = new T[num6];
                for (int i = 0; i < num6; i++)
                {
                    localArray2[i] = this.statistics[index + i];
                }
                return localArray2;
            }
        }

        private long GetWeekFromTime(DateTime dateTime)
        {
            TimeSpan span = (TimeSpan) (dateTime.Date - new DateTime(0x7b2, 1, 1));
            return ((span.Days + 3L) / 7L);
        }

        private long GetYearFromTime(DateTime dateTime)
        {
            return (long) (dateTime.Year - 0x7b2);
        }

        public void Reset(T[] statistics, long lastDataMark)
        {
            if (statistics.Length > this.arrayLength)
            {
                Array.Copy(statistics, statistics.Length - this.arrayLength, this.statistics, 0, this.arrayLength);
            }
            else if (statistics.Length < this.arrayLength)
            {
                Array.Copy(statistics, 0, this.statistics, this.arrayLength - statistics.Length, statistics.Length);
            }
            else
            {
                this.statistics = statistics;
            }
            this.arrayLength = statistics.Length;
            this.lastDataMark = lastDataMark;
        }

        protected void StatisticsCustomAction(Func<T, T> newValue)
        {
            object lockStatistics = this.lockStatistics;
            lock (lockStatistics)
            {
                long dataMarkFromDateTime = this.GetDataMarkFromDateTime(DateTime.Now);
                if (this.lastDataMark != dataMarkFromDateTime)
                {
                    int times = (int) (dataMarkFromDateTime - this.lastDataMark);
                    this.statistics = this.GetLeftMoveTimes(times);
                    this.lastDataMark = dataMarkFromDateTime;
                }
                this.statistics[this.statistics.Length - 1] = newValue(this.statistics[this.statistics.Length - 1]);
            }
        }

        protected void StatisticsCustomAction(Func<T, T> newValue, DateTime time)
        {
            object lockStatistics = this.lockStatistics;
            lock (lockStatistics)
            {
                long dataMarkFromDateTime = this.GetDataMarkFromDateTime(DateTime.Now);
                if (this.lastDataMark != dataMarkFromDateTime)
                {
                    int times = (int) (dataMarkFromDateTime - this.lastDataMark);
                    this.statistics = this.GetLeftMoveTimes(times);
                    this.lastDataMark = dataMarkFromDateTime;
                }
                long num2 = this.GetDataMarkFromDateTime(time);
                if (num2 <= dataMarkFromDateTime)
                {
                    int index = (int) (num2 - ((dataMarkFromDateTime - this.statistics.Length) + 1L));
                    if ((index >= 0) && (index < this.statistics.Length))
                    {
                        this.statistics[index] = newValue(this.statistics[index]);
                    }
                }
            }
        }

        public int ArrayLength
        {
            get
            {
                return this.arrayLength;
            }
        }

        public HslCommunication.LogNet.GenerateMode GenerateMode
        {
            get
            {
                return this.generateMode;
            }
        }
    }
}

