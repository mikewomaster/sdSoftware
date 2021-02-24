namespace HslCommunication.BasicFramework
{
    using System;

    public interface ISoftFileSaveBase
    {
        void LoadByFile();
        void LoadByString(string content);
        void SaveToFile();
        string ToSaveString();

        string FileSavePath { get; set; }
    }
}

