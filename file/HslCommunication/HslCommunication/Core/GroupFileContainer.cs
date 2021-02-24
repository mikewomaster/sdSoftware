namespace HslCommunication.Core
{
    using HslCommunication.LogNet;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;

    public class GroupFileContainer
    {
        private HslAsyncCoordinator coordinatorCacheJsonArray;
        private string dirPath = string.Empty;
        private string fileFolderPath;
        private string fileFullPath;
        private const string FileListResources = "list.txt";
        private int filesCount = 0;
        private List<GroupFileItem> groupFileItems;
        private object hybirdLock = new object();
        private string jsonArrayContent = "[]";
        private ILogNet LogNet;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event FileCountChangedDelegate FileCountChanged;

        public GroupFileContainer(ILogNet logNet, string path)
        {
            this.LogNet = logNet;
            this.dirPath = path;
            if (!string.IsNullOrEmpty(path))
            {
                this.LoadByPath(path);
            }
        }

        private void CacheJsonArrayContent()
        {
            object hybirdLock = this.hybirdLock;
            lock (hybirdLock)
            {
                this.filesCount = this.groupFileItems.Count;
                try
                {
                    this.jsonArrayContent = JArray.FromObject(this.groupFileItems).ToString();
                    using (StreamWriter writer = new StreamWriter(this.fileFullPath, false, Encoding.UTF8))
                    {
                        writer.Write(this.jsonArrayContent);
                        writer.Flush();
                    }
                }
                catch (Exception exception)
                {
                    ILogNet logNet;
                    if (this.LogNet != null)
                    {
                        logNet = this.LogNet;
                        goto Label_007E;
                    }
                    else
                    {
                        ILogNet expressionStack_7B_0 = this.LogNet;
                    }
                    goto Label_0098;
                Label_007E:
                    logNet.WriteException("CacheJsonArrayContent", exception);
                }
            Label_0098:;
            }
            if (this.FileCountChanged != null)
            {
                FileCountChangedDelegate fileCountChanged = this.FileCountChanged;
                fileCountChanged(this, this.filesCount);
            }
            else
            {
                FileCountChangedDelegate expressionStack_A1_0 = this.FileCountChanged;
            }
        }

        public List<string> ClearAllFiles()
        {
            List<string> list = new List<string>();
            object hybirdLock = this.hybirdLock;
            lock (hybirdLock)
            {
                for (int i = 0; i < this.groupFileItems.Count; i++)
                {
                    list.Add(this.groupFileItems[i].MappingName);
                }
                this.groupFileItems.Clear();
            }
            this.coordinatorCacheJsonArray.StartOperaterInfomation();
            return list;
        }

        public string DeleteFile(string fileName)
        {
            string mappingName = string.Empty;
            object hybirdLock = this.hybirdLock;
            lock (hybirdLock)
            {
                for (int i = 0; i < this.groupFileItems.Count; i++)
                {
                    if (this.groupFileItems[i].FileName == fileName)
                    {
                        mappingName = this.groupFileItems[i].MappingName;
                        this.groupFileItems.RemoveAt(i);
                        goto Label_007C;
                    }
                }
            }
        Label_007C:
            this.coordinatorCacheJsonArray.StartOperaterInfomation();
            return mappingName;
        }

        public string DeleteFileByGuid(string guidName)
        {
            string mappingName = string.Empty;
            object hybirdLock = this.hybirdLock;
            lock (hybirdLock)
            {
                for (int i = 0; i < this.groupFileItems.Count; i++)
                {
                    if (this.groupFileItems[i].MappingName == guidName)
                    {
                        mappingName = this.groupFileItems[i].MappingName;
                        this.groupFileItems.RemoveAt(i);
                        goto Label_007C;
                    }
                }
            }
        Label_007C:
            this.coordinatorCacheJsonArray.StartOperaterInfomation();
            return mappingName;
        }

        public bool FileExists(string fileName)
        {
            bool flag = false;
            object hybirdLock = this.hybirdLock;
            lock (hybirdLock)
            {
                for (int i = 0; i < this.groupFileItems.Count; i++)
                {
                    ILogNet logNet;
                    if (this.groupFileItems[i].FileName != fileName)
                    {
                        continue;
                    }
                    flag = true;
                    if (!File.Exists(Path.Combine(this.dirPath, this.groupFileItems[i].MappingName)))
                    {
                        flag = false;
                        if (this.LogNet != null)
                        {
                            logNet = this.LogNet;
                            goto Label_006E;
                        }
                        else
                        {
                            ILogNet expressionStack_6B_0 = this.LogNet;
                        }
                    }
                    return flag;
                Label_006E:
                    logNet.WriteError("File Check exist failed, find file in list, but mapping file not found");
                    return flag;
                }
            }
            return flag;
        }

        public string GetCurrentFileMappingName(string fileName)
        {
            string mappingName = string.Empty;
            object hybirdLock = this.hybirdLock;
            lock (hybirdLock)
            {
                for (int i = 0; i < this.groupFileItems.Count; i++)
                {
                    if (this.groupFileItems[i].FileName == fileName)
                    {
                        mappingName = this.groupFileItems[i].MappingName;
                        GroupFileItem local1 = this.groupFileItems[i];
                        local1.DownloadTimes += 1L;
                    }
                }
            }
            this.coordinatorCacheJsonArray.StartOperaterInfomation();
            return mappingName;
        }

        private void LoadByPath(string path)
        {
            this.fileFolderPath = path;
            this.fileFullPath = Path.Combine(path, "list.txt");
            if (!Directory.Exists(this.fileFolderPath))
            {
                Directory.CreateDirectory(this.fileFolderPath);
            }
            if (File.Exists(this.fileFullPath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(this.fileFullPath, Encoding.UTF8))
                    {
                        this.groupFileItems = JArray.Parse(reader.ReadToEnd()).ToObject<List<GroupFileItem>>();
                    }
                }
                catch (Exception exception)
                {
                    ILogNet logNet;
                    if (this.LogNet != null)
                    {
                        logNet = this.LogNet;
                        goto Label_008F;
                    }
                    else
                    {
                        ILogNet expressionStack_8C_0 = this.LogNet;
                    }
                    goto Label_00A4;
                Label_008F:
                    logNet.WriteException("GroupFileContainer", "Load files txt failed,", exception);
                }
            }
        Label_00A4:
            if (this.groupFileItems == null)
            {
                this.groupFileItems = new List<GroupFileItem>();
            }
            this.coordinatorCacheJsonArray = new HslAsyncCoordinator(new Action(this.CacheJsonArrayContent));
            this.CacheJsonArrayContent();
        }

        public override string ToString()
        {
            return ("GroupFileContainer[" + this.dirPath + "]");
        }

        public string UpdateFileMappingName(string fileName, long fileSize, string mappingName, string owner, string description)
        {
            string str = string.Empty;
            object hybirdLock = this.hybirdLock;
            lock (hybirdLock)
            {
                for (int i = 0; i < this.groupFileItems.Count; i++)
                {
                    if (this.groupFileItems[i].FileName == fileName)
                    {
                        str = this.groupFileItems[i].MappingName;
                        this.groupFileItems[i].MappingName = mappingName;
                        this.groupFileItems[i].Description = description;
                        this.groupFileItems[i].FileSize = fileSize;
                        this.groupFileItems[i].Owner = owner;
                        this.groupFileItems[i].UploadTime = DateTime.Now;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(str))
                {
                    GroupFileItem item = new GroupFileItem {
                        FileName = fileName,
                        FileSize = fileSize,
                        DownloadTimes = 0L,
                        Description = description,
                        Owner = owner,
                        MappingName = mappingName,
                        UploadTime = DateTime.Now
                    };
                    this.groupFileItems.Add(item);
                }
            }
            this.coordinatorCacheJsonArray.StartOperaterInfomation();
            return str;
        }

        public string DirectoryPath
        {
            get
            {
                return this.dirPath;
            }
        }

        public int FileCount
        {
            get
            {
                return this.filesCount;
            }
        }

        public string JsonArrayContent
        {
            get
            {
                return this.jsonArrayContent;
            }
        }

        public delegate void FileCountChangedDelegate(GroupFileContainer container, int fileCount);
    }
}

