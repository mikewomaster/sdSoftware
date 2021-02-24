namespace HslCommunication.Core
{
    using HslCommunication;
    using HslCommunication.LogNet;
    using System;
    using System.Text;

    public interface IReadWriteNet
    {
        OperateResult<T> Read<T>() where T: class, new();
        OperateResult<byte[]> Read(string address, ushort length);
        OperateResult<bool> ReadBool(string address);
        OperateResult<bool[]> ReadBool(string address, ushort length);
        OperateResult<T> ReadCustomer<T>(string address) where T: IDataTransfer, new();
        OperateResult<double> ReadDouble(string address);
        OperateResult<double[]> ReadDouble(string address, ushort length);
        OperateResult<float> ReadFloat(string address);
        OperateResult<float[]> ReadFloat(string address, ushort length);
        OperateResult<short> ReadInt16(string address);
        OperateResult<short[]> ReadInt16(string address, ushort length);
        OperateResult<int> ReadInt32(string address);
        OperateResult<int[]> ReadInt32(string address, ushort length);
        OperateResult<long> ReadInt64(string address);
        OperateResult<long[]> ReadInt64(string address, ushort length);
        OperateResult<string> ReadString(string address, ushort length);
        OperateResult<string> ReadString(string address, ushort length, Encoding encoding);
        OperateResult<ushort> ReadUInt16(string address);
        OperateResult<ushort[]> ReadUInt16(string address, ushort length);
        OperateResult<uint> ReadUInt32(string address);
        OperateResult<uint[]> ReadUInt32(string address, ushort length);
        OperateResult<ulong> ReadUInt64(string address);
        OperateResult<ulong[]> ReadUInt64(string address, ushort length);
        OperateResult<TimeSpan> Wait(string address, bool waitValue, int readInterval, int waitTimeout);
        OperateResult<TimeSpan> Wait(string address, short waitValue, int readInterval, int waitTimeout);
        OperateResult<TimeSpan> Wait(string address, int waitValue, int readInterval, int waitTimeout);
        OperateResult<TimeSpan> Wait(string address, long waitValue, int readInterval, int waitTimeout);
        OperateResult<TimeSpan> Wait(string address, ushort waitValue, int readInterval, int waitTimeout);
        OperateResult<TimeSpan> Wait(string address, uint waitValue, int readInterval, int waitTimeout);
        OperateResult<TimeSpan> Wait(string address, ulong waitValue, int readInterval, int waitTimeout);
        OperateResult Write<T>(T data) where T: class, new();
        OperateResult Write(string address, bool[] value);
        OperateResult Write(string address, byte[] value);
        OperateResult Write(string address, bool value);
        OperateResult Write(string address, short value);
        OperateResult Write(string address, int value);
        OperateResult Write(string address, long value);
        OperateResult Write(string address, ushort value);
        OperateResult Write(string address, uint value);
        OperateResult Write(string address, double[] values);
        OperateResult Write(string address, short[] values);
        OperateResult Write(string address, int[] values);
        OperateResult Write(string address, long[] values);
        OperateResult Write(string address, ushort[] values);
        OperateResult Write(string address, uint[] values);
        OperateResult Write(string address, ulong[] values);
        OperateResult Write(string address, double value);
        OperateResult Write(string address, float value);
        OperateResult Write(string address, string value);
        OperateResult Write(string address, ulong value);
        OperateResult Write(string address, float[] values);
        OperateResult Write(string address, string value, int length);
        OperateResult Write(string address, string value, Encoding encoding);
        OperateResult Write(string address, string value, int length, Encoding encoding);
        OperateResult WriteCustomer<T>(string address, T value) where T: IDataTransfer, new();

        string ConnectionId { get; set; }

        ILogNet LogNet { get; set; }
    }
}

