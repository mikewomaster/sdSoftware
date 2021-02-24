namespace HslCommunication.LogNet
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class LogStatistics : LogStatisticsBase<long>
    {
        private RegularByteTransform byteTransform;
        private long totalSum;

        public LogStatistics(GenerateMode generateMode, int dataCount) : base(generateMode, dataCount)
        {
            this.totalSum = 0L;
            this.byteTransform = new RegularByteTransform();
        }

        public void LoadFromBinary(byte[] buffer)
        {
            if (BitConverter.ToInt32(buffer, 0) != 0x12345678)
            {
                throw new Exception("File is not LogStatistics file, can't load data.");
            }
            int index = BitConverter.ToUInt16(buffer, 4);
            GenerateMode mode = (GenerateMode) BitConverter.ToUInt16(buffer, 6);
            int length = BitConverter.ToInt32(buffer, 8);
            long lastDataMark = BitConverter.ToInt64(buffer, 12);
            long num5 = BitConverter.ToInt64(buffer, 20);
            base.generateMode = mode;
            this.totalSum = num5;
            long[] statistics = this.byteTransform.TransInt64(buffer, index, length);
            base.Reset(statistics, lastDataMark);
        }

        public void LoadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                this.LoadFromBinary(File.ReadAllBytes(fileName));
            }
        }

        public byte[] SaveToBinary()
        {
            OperateResult<long, long[]> statisticsSnapAndDataMark = base.GetStatisticsSnapAndDataMark();
            int num = 0x400;
            byte[] array = new byte[(statisticsSnapAndDataMark.Content2.Length * 8) + num];
            BitConverter.GetBytes(0x12345678).CopyTo(array, 0);
            BitConverter.GetBytes((ushort) num).CopyTo(array, 4);
            BitConverter.GetBytes((ushort) base.GenerateMode).CopyTo(array, 6);
            BitConverter.GetBytes(statisticsSnapAndDataMark.Content2.Length).CopyTo(array, 8);
            BitConverter.GetBytes(statisticsSnapAndDataMark.Content1).CopyTo(array, 12);
            BitConverter.GetBytes(this.TotalSum).CopyTo(array, 20);
            for (int i = 0; i < statisticsSnapAndDataMark.Content2.Length; i++)
            {
                BitConverter.GetBytes(statisticsSnapAndDataMark.Content2[i]).CopyTo(array, (int) (num + (i * 8)));
            }
            return array;
        }

        public void SaveToFile(string fileName)
        {
            File.WriteAllBytes(fileName, this.SaveToBinary());
        }

        [HslMqttApi(Description="Adding a new statistical information will determine the position to insert the data according to the current time.")]
        public void StatisticsAdd([Optional, DefaultParameterValue(1L)] long frequency)
        {
            Interlocked.Add(ref this.totalSum, frequency);
            base.StatisticsCustomAction(m => m + frequency);
        }

        [HslMqttApi(Description="Adding a new statistical information will determine the position to insert the data according to the specified time.")]
        public void StatisticsAddByTime(long frequency, DateTime time)
        {
            Interlocked.Add(ref this.totalSum, frequency);
            base.StatisticsCustomAction(m => m + frequency, time);
        }

        public override string ToString()
        {
            return string.Format("LogStatistics[{0}:{1}]", base.GenerateMode, base.ArrayLength);
        }

        [HslMqttApi(HttpMethod="GET", Description="Get the sum of all current values")]
        public long TotalSum
        {
            get
            {
                return this.totalSum;
            }
        }
    }
}

