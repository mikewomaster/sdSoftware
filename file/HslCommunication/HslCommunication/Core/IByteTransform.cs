namespace HslCommunication.Core
{
    using System;
    using System.Text;

    public interface IByteTransform
    {
        IByteTransform CreateByDateFormat(HslCommunication.Core.DataFormat dataFormat);
        bool TransBool(byte[] buffer, int index);
        bool[] TransBool(byte[] buffer, int index, int length);
        byte[] TransByte(bool value);
        byte[] TransByte(bool[] values);
        byte[] TransByte(byte value);
        byte[] TransByte(short[] values);
        byte[] TransByte(int[] values);
        byte[] TransByte(long[] values);
        byte[] TransByte(float[] values);
        byte[] TransByte(ushort[] values);
        byte[] TransByte(uint[] values);
        byte[] TransByte(ulong[] values);
        byte[] TransByte(double value);
        byte[] TransByte(short value);
        byte[] TransByte(int value);
        byte[] TransByte(long value);
        byte[] TransByte(float value);
        byte[] TransByte(ushort value);
        byte[] TransByte(uint value);
        byte[] TransByte(ulong value);
        byte[] TransByte(double[] values);
        byte TransByte(byte[] buffer, int index);
        byte[] TransByte(string value, Encoding encoding);
        byte[] TransByte(byte[] buffer, int index, int length);
        byte[] TransByte(string value, int length, Encoding encoding);
        double TransDouble(byte[] buffer, int index);
        double[] TransDouble(byte[] buffer, int index, int length);
        short TransInt16(byte[] buffer, int index);
        short[] TransInt16(byte[] buffer, int index, int length);
        int TransInt32(byte[] buffer, int index);
        int[] TransInt32(byte[] buffer, int index, int length);
        long TransInt64(byte[] buffer, int index);
        long[] TransInt64(byte[] buffer, int index, int length);
        float TransSingle(byte[] buffer, int index);
        float[] TransSingle(byte[] buffer, int index, int length);
        string TransString(byte[] buffer, Encoding encoding);
        string TransString(byte[] buffer, int index, int length, Encoding encoding);
        ushort TransUInt16(byte[] buffer, int index);
        ushort[] TransUInt16(byte[] buffer, int index, int length);
        uint TransUInt32(byte[] buffer, int index);
        uint[] TransUInt32(byte[] buffer, int index, int length);
        ulong TransUInt64(byte[] buffer, int index);
        ulong[] TransUInt64(byte[] buffer, int index, int length);

        HslCommunication.Core.DataFormat DataFormat { get; set; }

        bool IsStringReverseByteWord { get; set; }
    }
}

