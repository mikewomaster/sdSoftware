namespace HslCommunication.LogNet
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class LogPathBase : LogNetBase
    {
        protected int controlFileQuantity = -1;
        protected string fileName = string.Empty;
        protected string filePath = string.Empty;

        protected LogPathBase()
        {
        }

        public string[] GetExistLogFileNames()
        {
            if (!string.IsNullOrEmpty(this.filePath))
            {
                return Directory.GetFiles(this.filePath, "Logs_*.txt");
            }
            return new string[0];
        }

        protected override void OnWriteCompleted()
        {
            if (this.controlFileQuantity > 1)
            {
                try
                {
                    string[] existLogFileNames = this.GetExistLogFileNames();
                    if (existLogFileNames.Length > this.controlFileQuantity)
                    {
                        List<FileInfo> list = new List<FileInfo>();
                        for (int i = 0; i < existLogFileNames.Length; i++)
                        {
                            list.Add(new FileInfo(existLogFileNames[i]));
                        }
                        list.Sort((Comparison<FileInfo>) ((m, n) => m.CreationTime.CompareTo(n.CreationTime)));
                        for (int j = 0; j < (list.Count - this.controlFileQuantity); j++)
                        {
                            File.Delete(list[j].FullName);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        public override string ToString()
        {
            return "LogPathBase";
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LogPathBase.<>c <>9 = new LogPathBase.<>c();
            public static Comparison<FileInfo> <>9__0_0;

            internal int <OnWriteCompleted>b__0_0(FileInfo m, FileInfo n)
            {
                return m.CreationTime.CompareTo(n.CreationTime);
            }
        }
    }
}

