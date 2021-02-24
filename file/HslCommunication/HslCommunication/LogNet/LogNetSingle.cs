namespace HslCommunication.LogNet
{
    using System;
    using System.IO;
    using System.Text;

    public class LogNetSingle : LogNetBase, ILogNet, IDisposable
    {
        private readonly string fileName = string.Empty;

        public LogNetSingle(string filePath)
        {
            this.fileName = filePath;
            base.LogSaveMode = LogSaveMode.SingleFile;
            if (!string.IsNullOrEmpty(this.fileName))
            {
                FileInfo info = new FileInfo(filePath);
                if (!Directory.Exists(info.DirectoryName))
                {
                    Directory.CreateDirectory(info.DirectoryName);
                }
            }
        }

        public void ClearLog()
        {
            base.m_fileSaveLock.Enter();
            try
            {
                if (!string.IsNullOrEmpty(this.fileName))
                {
                    File.Create(this.fileName).Dispose();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                base.m_fileSaveLock.Leave();
            }
        }

        public string GetAllSavedLog()
        {
            string str = string.Empty;
            base.m_fileSaveLock.Enter();
            try
            {
                if (!string.IsNullOrEmpty(this.fileName) && File.Exists(this.fileName))
                {
                    StreamReader reader = new StreamReader(this.fileName, Encoding.UTF8);
                    str = reader.ReadToEnd();
                    reader.Dispose();
                }
                return str;
            }
            catch
            {
                throw;
            }
            finally
            {
                base.m_fileSaveLock.Leave();
            }
            return str;
        }

        public string[] GetExistLogFileNames()
        {
            return new string[] { this.fileName };
        }

        protected override string GetFileSaveName()
        {
            return this.fileName;
        }

        public override string ToString()
        {
            return ("LogNetSingle[" + this.fileName + "]");
        }
    }
}

