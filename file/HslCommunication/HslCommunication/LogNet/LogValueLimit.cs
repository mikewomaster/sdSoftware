namespace HslCommunication.LogNet
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using System;
    using System.IO;
    using System.Threading;

    public class LogValueLimit : LogStatisticsBase<ValueLimit>
    {
        private RegularByteTransform byteTransform;
        private long valueCount;

        public LogValueLimit(GenerateMode generateMode, int dataCount) : base(generateMode, dataCount)
        {
            this.valueCount = 0L;
            this.byteTransform = new RegularByteTransform();
        }

        [HslMqttApi(Description="Add a new data for analysis")]
        public void AnalysisNewValue(double value)
        {
            Interlocked.Increment(ref this.valueCount);
            base.StatisticsCustomAction(m => m.SetNewValue(value));
        }

        [HslMqttApi(Description="Add a new data for analysis")]
        public void AnalysisNewValueByTime(double value, DateTime time)
        {
            Interlocked.Increment(ref this.valueCount);
            base.StatisticsCustomAction(m => m.SetNewValue(value), time);
        }

        public void LoadFromBinary(byte[] buffer)
        {
            if (BitConverter.ToInt32(buffer, 0) != 0x12345679)
            {
                throw new Exception("File is not LogValueLimit file, can't load data.");
            }
            int num2 = BitConverter.ToUInt16(buffer, 4);
            GenerateMode mode = (GenerateMode) BitConverter.ToUInt16(buffer, 6);
            int num3 = BitConverter.ToInt32(buffer, 8);
            long lastDataMark = BitConverter.ToInt64(buffer, 12);
            long num5 = BitConverter.ToInt64(buffer, 20);
            int num6 = BitConverter.ToInt32(buffer, 0x1c);
            base.generateMode = mode;
            this.valueCount = num5;
            ValueLimit[] statistics = new ValueLimit[num3];
            for (int i = 0; i < statistics.Length; i++)
            {
                statistics[i].StartValue = this.byteTransform.TransDouble(buffer, (i * num6) + num2);
                statistics[i].Current = this.byteTransform.TransDouble(buffer, ((i * num6) + num2) + 8);
                statistics[i].MaxValue = this.byteTransform.TransDouble(buffer, ((i * num6) + num2) + 0x10);
                statistics[i].MinValue = this.byteTransform.TransDouble(buffer, ((i * num6) + num2) + 0x18);
                statistics[i].Average = this.byteTransform.TransDouble(buffer, ((i * num6) + num2) + 0x20);
                statistics[i].Count = this.byteTransform.TransInt32(buffer, ((i * num6) + num2) + 40);
            }
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
            OperateResult<long, ValueLimit[]> statisticsSnapAndDataMark = base.GetStatisticsSnapAndDataMark();
            int num = 0x400;
            int num2 = 0x40;
            byte[] array = new byte[(statisticsSnapAndDataMark.Content2.Length * num2) + num];
            BitConverter.GetBytes(0x12345679).CopyTo(array, 0);
            BitConverter.GetBytes((ushort) num).CopyTo(array, 4);
            BitConverter.GetBytes((ushort) base.GenerateMode).CopyTo(array, 6);
            BitConverter.GetBytes(statisticsSnapAndDataMark.Content2.Length).CopyTo(array, 8);
            BitConverter.GetBytes(statisticsSnapAndDataMark.Content1).CopyTo(array, 12);
            BitConverter.GetBytes(this.valueCount).CopyTo(array, 20);
            BitConverter.GetBytes(num2).CopyTo(array, 0x1c);
            for (int i = 0; i < statisticsSnapAndDataMark.Content2.Length; i++)
            {
                this.byteTransform.TransByte(statisticsSnapAndDataMark.Content2[i].StartValue).CopyTo(array, (int) ((i * num2) + num));
                this.byteTransform.TransByte(statisticsSnapAndDataMark.Content2[i].Current).CopyTo(array, (int) (((i * num2) + num) + 8));
                this.byteTransform.TransByte(statisticsSnapAndDataMark.Content2[i].MaxValue).CopyTo(array, (int) (((i * num2) + num) + 0x10));
                this.byteTransform.TransByte(statisticsSnapAndDataMark.Content2[i].MinValue).CopyTo(array, (int) (((i * num2) + num) + 0x18));
                this.byteTransform.TransByte(statisticsSnapAndDataMark.Content2[i].Average).CopyTo(array, (int) (((i * num2) + num) + 0x20));
                this.byteTransform.TransByte(statisticsSnapAndDataMark.Content2[i].Count).CopyTo(array, (int) (((i * num2) + num) + 40));
            }
            return array;
        }

        public void SaveToFile(string fileName)
        {
            File.WriteAllBytes(fileName, this.SaveToBinary());
        }

        public override string ToString()
        {
            return string.Format("LogValueLimit[{0}:{1}]", base.GenerateMode, base.ArrayLength);
        }

        [HslMqttApi(HttpMethod="GET", Description="The total amount of data currently set")]
        public long ValueCount
        {
            get
            {
                return this.valueCount;
            }
        }
    }
}

