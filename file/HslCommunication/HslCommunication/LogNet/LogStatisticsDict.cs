namespace HslCommunication.LogNet
{
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class LogStatisticsDict
    {
        private int arrayLength = 30;
        private Dictionary<string, LogStatistics> dict;
        private object dictLock;
        private HslCommunication.LogNet.GenerateMode generateMode = HslCommunication.LogNet.GenerateMode.ByEveryDay;
        private LogStatistics logStat;

        public LogStatisticsDict(HslCommunication.LogNet.GenerateMode generateMode, int arrayLength)
        {
            this.generateMode = generateMode;
            this.arrayLength = arrayLength;
            this.dictLock = new object();
            this.dict = new Dictionary<string, LogStatistics>(0x80);
            this.logStat = new LogStatistics(generateMode, arrayLength);
        }

        public void AddLogStatistics(string key, LogStatistics logStatistics)
        {
            object dictLock = this.dictLock;
            lock (dictLock)
            {
                if (this.dict.ContainsKey(key))
                {
                    this.dict[key] = logStatistics;
                }
                else
                {
                    this.dict.Add(key, logStatistics);
                }
            }
        }

        [HslMqttApi(Description="Get data information of all keywords")]
        public string[] GetKeys()
        {
            object dictLock = this.dictLock;
            lock (dictLock)
            {
                return this.dict.Keys.ToArray<string>();
            }
        }

        public LogStatistics GetLogStatistics(string key)
        {
            object dictLock = this.dictLock;
            lock (dictLock)
            {
                if (this.dict.ContainsKey(key))
                {
                    return this.dict[key];
                }
                return null;
            }
        }

        [HslMqttApi(Description="Get a data snapshot of the current statistics")]
        public long[] GetStatisticsSnapshot(string key)
        {
            return ((this.GetLogStatistics(key) == null) ? null : this.GetLogStatistics(key).GetStatisticsSnapshot());
        }

        [HslMqttApi(Description="Get a snapshot of statistical data information according to the specified time range")]
        public long[] GetStatisticsSnapshotByTime(string key, DateTime start, DateTime finish)
        {
            return this.GetLogStatistics(key).GetStatisticsSnapshotByTime((this.GetLogStatistics(key) == null) ? ((DateTime) 0) : start, finish);
        }

        public void LoadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = HslHelper.ReadSpecifiedLengthFromStream(stream, 0x400);
                    this.generateMode = (HslCommunication.LogNet.GenerateMode) BitConverter.ToUInt16(buffer, 6);
                    int num = BitConverter.ToInt32(buffer, 8);
                    for (int i = 0; i < num; i++)
                    {
                        string key = HslHelper.ReadStringFromStream(stream);
                        byte[] buffer2 = HslHelper.ReadBinaryFromStream(stream);
                        LogStatistics logStatistics = new LogStatistics(this.generateMode, this.arrayLength);
                        logStatistics.LoadFromBinary(buffer2);
                        this.AddLogStatistics(key, logStatistics);
                    }
                }
            }
        }

        public bool RemoveLogStatistics(string key)
        {
            object dictLock = this.dictLock;
            lock (dictLock)
            {
                if (this.dict.ContainsKey(key))
                {
                    this.dict.Remove(key);
                    return true;
                }
                return false;
            }
        }

        public void SaveToFile(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] array = new byte[0x400];
                BitConverter.GetBytes(0x12345682).CopyTo(array, 0);
                BitConverter.GetBytes((ushort) array.Length).CopyTo(array, 4);
                BitConverter.GetBytes((ushort) this.GenerateMode).CopyTo(array, 6);
                string[] keys = this.GetKeys();
                BitConverter.GetBytes(keys.Length).CopyTo(array, 8);
                stream.Write(array, 0, array.Length);
                foreach (string str in keys)
                {
                    LogStatistics logStatistics = this.GetLogStatistics(str);
                    if (logStatistics > null)
                    {
                        HslHelper.WriteStringToStream(stream, str);
                        HslHelper.WriteBinaryToStream(stream, logStatistics.SaveToBinary());
                    }
                }
            }
        }

        [HslMqttApi(Description="Adding a new statistical information will determine the position to insert the data according to the current time")]
        public void StatisticsAdd(string key, [Optional, DefaultParameterValue(1L)] long frequency)
        {
            this.logStat.StatisticsAdd(frequency);
            LogStatistics logStatistics = this.GetLogStatistics(key);
            if (logStatistics == null)
            {
                object dictLock = this.dictLock;
                lock (dictLock)
                {
                    if (!this.dict.ContainsKey(key))
                    {
                        logStatistics = new LogStatistics(this.generateMode, this.arrayLength);
                        this.dict.Add(key, logStatistics);
                    }
                    else
                    {
                        logStatistics = this.dict[key];
                    }
                }
            }
            if (logStatistics != null)
            {
                logStatistics.StatisticsAdd(frequency);
            }
        }

        [HslMqttApi(Description="Adding a new statistical information will determine the position to insert the data according to the specified time")]
        public void StatisticsAddByTime(string key, long frequency, DateTime time)
        {
            this.logStat.StatisticsAddByTime(frequency, time);
            LogStatistics logStatistics = this.GetLogStatistics(key);
            if (logStatistics == null)
            {
                object dictLock = this.dictLock;
                lock (dictLock)
                {
                    if (!this.dict.ContainsKey(key))
                    {
                        logStatistics = new LogStatistics(this.generateMode, this.arrayLength);
                        this.dict.Add(key, logStatistics);
                    }
                    else
                    {
                        logStatistics = this.dict[key];
                    }
                }
            }
            if (logStatistics != null)
            {
                logStatistics.StatisticsAddByTime(frequency, time);
            }
        }

        [HslMqttApi(HttpMethod="GET", Description="Get the total amount of current statistical information")]
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

        [HslMqttApi(PropertyUnfold=true, Description="Get the log statistics object of the current dictionary class itself, and count the statistics of all elements")]
        public LogStatistics LogStat
        {
            get
            {
                return this.logStat;
            }
        }
    }
}

