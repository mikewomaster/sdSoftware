namespace HslCommunication.LogNet
{
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class LogValueLimitDict
    {
        private int arrayLength = 30;
        private Dictionary<string, LogValueLimit> dict;
        private object dictLock;
        private HslCommunication.LogNet.GenerateMode generateMode = HslCommunication.LogNet.GenerateMode.ByEveryDay;
        private LogStatistics logStat;

        public LogValueLimitDict(HslCommunication.LogNet.GenerateMode generateMode, int arrayLength)
        {
            this.generateMode = generateMode;
            this.arrayLength = arrayLength;
            this.dictLock = new object();
            this.dict = new Dictionary<string, LogValueLimit>(0x80);
            this.logStat = new LogStatistics(generateMode, arrayLength);
        }

        public void AddLogValueLimit(string key, LogValueLimit logValueLimit)
        {
            object dictLock = this.dictLock;
            lock (dictLock)
            {
                if (this.dict.ContainsKey(key))
                {
                    this.dict[key] = logValueLimit;
                }
                else
                {
                    this.dict.Add(key, logValueLimit);
                }
            }
        }

        [HslMqttApi(Description="Add a new data for analysis")]
        public void AnalysisNewValue(string key, double value)
        {
            this.logStat.StatisticsAdd(1L);
            LogValueLimit logValueLimit = this.GetLogValueLimit(key);
            if (logValueLimit == null)
            {
                object dictLock = this.dictLock;
                lock (dictLock)
                {
                    if (!this.dict.ContainsKey(key))
                    {
                        logValueLimit = new LogValueLimit(this.generateMode, this.arrayLength);
                        this.dict.Add(key, logValueLimit);
                    }
                    else
                    {
                        logValueLimit = this.dict[key];
                    }
                }
            }
            if (logValueLimit != null)
            {
                logValueLimit.AnalysisNewValue(value);
            }
        }

        [HslMqttApi(Description="Add a new data for analysis")]
        public void AnalysisNewValueByTime(string key, double value, DateTime time)
        {
            this.logStat.StatisticsAddByTime(1L, time);
            LogValueLimit logValueLimit = this.GetLogValueLimit(key);
            if (logValueLimit == null)
            {
                object dictLock = this.dictLock;
                lock (dictLock)
                {
                    if (!this.dict.ContainsKey(key))
                    {
                        logValueLimit = new LogValueLimit(this.generateMode, this.arrayLength);
                        this.dict.Add(key, logValueLimit);
                    }
                    else
                    {
                        logValueLimit = this.dict[key];
                    }
                }
            }
            if (logValueLimit != null)
            {
                logValueLimit.AnalysisNewValueByTime(value, time);
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

        public LogValueLimit GetLogValueLimit(string key)
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
        public ValueLimit[] GetStatisticsSnapshot(string key)
        {
            return ((this.GetLogValueLimit(key) == null) ? null : this.GetLogValueLimit(key).GetStatisticsSnapshot());
        }

        [HslMqttApi(Description="Get a snapshot of statistical data information according to the specified time range")]
        public ValueLimit[] GetStatisticsSnapshotByTime(string key, DateTime start, DateTime finish)
        {
            return this.GetLogValueLimit(key).GetStatisticsSnapshotByTime((this.GetLogValueLimit(key) == null) ? ((DateTime) 0) : start, finish);
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
                        LogValueLimit logValueLimit = new LogValueLimit(this.generateMode, this.arrayLength);
                        logValueLimit.LoadFromBinary(buffer2);
                        this.AddLogValueLimit(key, logValueLimit);
                    }
                }
            }
        }

        public bool RemoveLogValueLimit(string key)
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
                    LogValueLimit logValueLimit = this.GetLogValueLimit(str);
                    if (logValueLimit > null)
                    {
                        HslHelper.WriteStringToStream(stream, str);
                        HslHelper.WriteBinaryToStream(stream, logValueLimit.SaveToBinary());
                    }
                }
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

