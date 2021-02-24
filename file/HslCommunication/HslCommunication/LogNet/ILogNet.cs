namespace HslCommunication.LogNet
{
    using System;
    using System.Runtime.CompilerServices;

    public interface ILogNet : IDisposable
    {
        event EventHandler<HslEventArgs> BeforeSaveToFile;

        void FiltrateKeyword(string keyword);
        string[] GetExistLogFileNames();
        void RecordMessage(HslMessageDegree degree, string keyWord, string text);
        void RemoveFiltrate(string keyword);
        void SetMessageDegree(HslMessageDegree degree);
        void WriteAnyString(string text);
        void WriteDebug(string text);
        void WriteDebug(string keyWord, string text);
        void WriteDescrition(string description);
        void WriteError(string text);
        void WriteError(string keyWord, string text);
        void WriteException(string keyWord, Exception ex);
        void WriteException(string keyWord, string text, Exception ex);
        void WriteFatal(string text);
        void WriteFatal(string keyWord, string text);
        void WriteInfo(string text);
        void WriteInfo(string keyWord, string text);
        void WriteNewLine();
        void WriteWarn(string text);
        void WriteWarn(string keyWord, string text);

        LogStatistics LogNetStatistics { get; set; }

        HslCommunication.LogNet.LogSaveMode LogSaveMode { get; }
    }
}

