namespace HslCommunication.LogNet
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public class LogNetFileSize : LogPathBase, ILogNet, IDisposable
    {
        private int currentFileSize = 0;
        private int fileMaxSize = 0x200000;

        public LogNetFileSize(string filePath, [Optional, DefaultParameterValue(0x200000)] int fileMaxSize, [Optional, DefaultParameterValue(-1)] int fileQuantity)
        {
            base.filePath = filePath;
            this.fileMaxSize = fileMaxSize;
            base.controlFileQuantity = fileQuantity;
            base.LogSaveMode = LogSaveMode.FileFixedSize;
            if (!string.IsNullOrEmpty(filePath) && !Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }

        private string GetDefaultFileName()
        {
            return Path.Combine(base.filePath, "Logs_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
        }

        protected override string GetFileSaveName()
        {
            if (string.IsNullOrEmpty(base.filePath))
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(base.fileName))
            {
                base.fileName = this.GetLastAccessFileName();
            }
            if (File.Exists(base.fileName))
            {
                FileInfo info = new FileInfo(base.fileName);
                if (info.Length > this.fileMaxSize)
                {
                    base.fileName = this.GetDefaultFileName();
                }
                else
                {
                    this.currentFileSize = (int) info.Length;
                }
            }
            return base.fileName;
        }

        private string GetLastAccessFileName()
        {
            foreach (string str in base.GetExistLogFileNames())
            {
                FileInfo info = new FileInfo(str);
                if (info.Length < this.fileMaxSize)
                {
                    this.currentFileSize = (int) info.Length;
                    return str;
                }
            }
            return this.GetDefaultFileName();
        }

        public override string ToString()
        {
            return string.Format("LogNetFileSize[{0}]", this.fileMaxSize);
        }
    }
}

