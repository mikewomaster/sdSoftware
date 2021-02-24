namespace HslCommunication.LogNet
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;

    public class LogNetDateTime : LogPathBase, ILogNet, IDisposable
    {
        private GenerateMode generateMode = GenerateMode.ByEveryYear;

        public LogNetDateTime(string filePath, [Optional, DefaultParameterValue(7)] GenerateMode generateMode, [Optional, DefaultParameterValue(-1)] int fileQuantity)
        {
            base.filePath = filePath;
            this.generateMode = generateMode;
            base.LogSaveMode = LogSaveMode.Time;
            base.controlFileQuantity = fileQuantity;
            if (!string.IsNullOrEmpty(filePath) && !Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }

        protected override string GetFileSaveName()
        {
            if (!string.IsNullOrEmpty(base.filePath))
            {
                switch (this.generateMode)
                {
                    case GenerateMode.ByEveryMinute:
                        return Path.Combine(base.filePath, "Logs_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".txt");

                    case GenerateMode.ByEveryHour:
                        return Path.Combine(base.filePath, "Logs_" + DateTime.Now.ToString("yyyyMMdd_HH") + ".txt");

                    case GenerateMode.ByEveryDay:
                        return Path.Combine(base.filePath, "Logs_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");

                    case GenerateMode.ByEveryWeek:
                    {
                        int num = new GregorianCalendar().GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                        string[] textArray1 = new string[] { "Logs_", DateTime.Now.Year.ToString(), "_W", num.ToString(), ".txt" };
                        return Path.Combine(base.filePath, string.Concat(textArray1));
                    }
                    case GenerateMode.ByEveryMonth:
                        return Path.Combine(base.filePath, "Logs_" + DateTime.Now.ToString("yyyy_MM") + ".txt");

                    case GenerateMode.ByEverySeason:
                    {
                        string[] textArray2 = new string[] { "Logs_", DateTime.Now.Year.ToString(), "_Q", ((DateTime.Now.Month / 3) + 1).ToString(), ".txt" };
                        return Path.Combine(base.filePath, string.Concat(textArray2));
                    }
                    case GenerateMode.ByEveryYear:
                        return Path.Combine(base.filePath, "Logs_" + DateTime.Now.Year.ToString() + ".txt");
                }
            }
            return string.Empty;
        }

        public override string ToString()
        {
            return string.Format("LogNetDateTime[{0}]", this.generateMode);
        }
    }
}

