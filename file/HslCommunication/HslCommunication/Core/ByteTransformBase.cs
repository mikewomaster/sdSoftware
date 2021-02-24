namespace HslCommunication.Core
{
    using HslCommunication.BasicFramework;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ByteTransformBase : IByteTransform
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private HslCommunication.Core.DataFormat <DataFormat>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsStringReverseByteWord>k__BackingField;

        public ByteTransformBase()
        {
            this.DataFormat = HslCommunication.Core.DataFormat.DCBA;
        }

        public ByteTransformBase(HslCommunication.Core.DataFormat dataFormat)
        {
            this.DataFormat = dataFormat;
        }

        protected byte[] ByteTransDataFormat4(byte[] value, [Optional, DefaultParameterValue(0)] int index)
        {
            byte[] buffer = new byte[4];
            switch (this.DataFormat)
            {
                case HslCommunication.Core.DataFormat.ABCD:
                    buffer[0] = value[index + 3];
                    buffer[1] = value[index + 2];
                    buffer[2] = value[index + 1];
                    buffer[3] = value[index];
                    return buffer;

                case HslCommunication.Core.DataFormat.BADC:
                    buffer[0] = value[index + 2];
                    buffer[1] = value[index + 3];
                    buffer[2] = value[index];
                    buffer[3] = value[index + 1];
                    return buffer;

                case HslCommunication.Core.DataFormat.CDAB:
                    buffer[0] = value[index + 1];
                    buffer[1] = value[index];
                    buffer[2] = value[index + 3];
                    buffer[3] = value[index + 2];
                    return buffer;

                case HslCommunication.Core.DataFormat.DCBA:
                    buffer[0] = value[index];
                    buffer[1] = value[index + 1];
                    buffer[2] = value[index + 2];
                    buffer[3] = value[index + 3];
                    return buffer;
            }
            return buffer;
        }

        protected byte[] ByteTransDataFormat8(byte[] value, [Optional, DefaultParameterValue(0)] int index)
        {
            byte[] buffer = new byte[8];
            switch (this.DataFormat)
            {
                case HslCommunication.Core.DataFormat.ABCD:
                    buffer[0] = value[index + 7];
                    buffer[1] = value[index + 6];
                    buffer[2] = value[index + 5];
                    buffer[3] = value[index + 4];
                    buffer[4] = value[index + 3];
                    buffer[5] = value[index + 2];
                    buffer[6] = value[index + 1];
                    buffer[7] = value[index];
                    return buffer;

                case HslCommunication.Core.DataFormat.BADC:
                    buffer[0] = value[index + 6];
                    buffer[1] = value[index + 7];
                    buffer[2] = value[index + 4];
                    buffer[3] = value[index + 5];
                    buffer[4] = value[index + 2];
                    buffer[5] = value[index + 3];
                    buffer[6] = value[index];
                    buffer[7] = value[index + 1];
                    return buffer;

                case HslCommunication.Core.DataFormat.CDAB:
                    buffer[0] = value[index + 1];
                    buffer[1] = value[index];
                    buffer[2] = value[index + 3];
                    buffer[3] = value[index + 2];
                    buffer[4] = value[index + 5];
                    buffer[5] = value[index + 4];
                    buffer[6] = value[index + 7];
                    buffer[7] = value[index + 6];
                    return buffer;

                case HslCommunication.Core.DataFormat.DCBA:
                    buffer[0] = value[index];
                    buffer[1] = value[index + 1];
                    buffer[2] = value[index + 2];
                    buffer[3] = value[index + 3];
                    buffer[4] = value[index + 4];
                    buffer[5] = value[index + 5];
                    buffer[6] = value[index + 6];
                    buffer[7] = value[index + 7];
                    return buffer;
            }
            return buffer;
        }

        public virtual IByteTransform CreateByDateFormat(HslCommunication.Core.DataFormat dataFormat)
        {
            return this;
        }

        public override string ToString()
        {
            return string.Format("ByteTransformBase[{0}]", this.DataFormat);
        }

        public virtual bool TransBool(byte[] buffer, int index)
        {
            return ((buffer[index] & 1) == 1);
        }

        public bool[] TransBool(byte[] buffer, int index, int length)
        {
            byte[] destinationArray = new byte[length];
            Array.Copy(buffer, index, destinationArray, 0, length);
            return SoftBasic.ByteToBoolArray(destinationArray, length * 8);
        }

        public virtual byte[] TransByte(bool value)
        {
            bool[] values = new bool[] { value };
            return this.TransByte(values);
        }

        public virtual byte[] TransByte(bool[] values)
        {
            return ((values == null) ? null : SoftBasic.BoolArrayToByte(values));
        }

        public virtual byte[] TransByte(byte value)
        {
            return new byte[] { value };
        }

        public virtual byte[] TransByte(double value)
        {
            double[] values = new double[] { value };
            return this.TransByte(values);
        }

        public virtual byte[] TransByte(short value)
        {
            short[] values = new short[] { value };
            return this.TransByte(values);
        }

        public virtual byte[] TransByte(ushort value)
        {
            ushort[] values = new ushort[] { value };
            return this.TransByte(values);
        }

        public virtual byte[] TransByte(double[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 8];
            for (int i = 0; i < values.Length; i++)
            {
                this.ByteTransDataFormat8(BitConverter.GetBytes(values[i]), 0).CopyTo(array, (int) (8 * i));
            }
            return array;
        }

        public virtual byte[] TransByte(short[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 2];
            for (int i = 0; i < values.Length; i++)
            {
                BitConverter.GetBytes(values[i]).CopyTo(array, (int) (2 * i));
            }
            return array;
        }

        public virtual byte[] TransByte(int[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 4];
            for (int i = 0; i < values.Length; i++)
            {
                this.ByteTransDataFormat4(BitConverter.GetBytes(values[i]), 0).CopyTo(array, (int) (4 * i));
            }
            return array;
        }

        public virtual byte[] TransByte(long[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 8];
            for (int i = 0; i < values.Length; i++)
            {
                this.ByteTransDataFormat8(BitConverter.GetBytes(values[i]), 0).CopyTo(array, (int) (8 * i));
            }
            return array;
        }

        public virtual byte[] TransByte(float[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 4];
            for (int i = 0; i < values.Length; i++)
            {
                this.ByteTransDataFormat4(BitConverter.GetBytes(values[i]), 0).CopyTo(array, (int) (4 * i));
            }
            return array;
        }

        public virtual byte[] TransByte(ushort[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 2];
            for (int i = 0; i < values.Length; i++)
            {
                BitConverter.GetBytes(values[i]).CopyTo(array, (int) (2 * i));
            }
            return array;
        }

        public virtual byte[] TransByte(int value)
        {
            int[] values = new int[] { value };
            return this.TransByte(values);
        }

        public virtual byte[] TransByte(long value)
        {
            long[] values = new long[] { value };
            return this.TransByte(values);
        }

        public virtual byte[] TransByte(uint value)
        {
            uint[] values = new uint[] { value };
            return this.TransByte(values);
        }

        public virtual byte[] TransByte(uint[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 4];
            for (int i = 0; i < values.Length; i++)
            {
                this.ByteTransDataFormat4(BitConverter.GetBytes(values[i]), 0).CopyTo(array, (int) (4 * i));
            }
            return array;
        }

        public virtual byte[] TransByte(ulong value)
        {
            ulong[] values = new ulong[] { value };
            return this.TransByte(values);
        }

        public virtual byte[] TransByte(ulong[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 8];
            for (int i = 0; i < values.Length; i++)
            {
                this.ByteTransDataFormat8(BitConverter.GetBytes(values[i]), 0).CopyTo(array, (int) (8 * i));
            }
            return array;
        }

        public virtual byte[] TransByte(float value)
        {
            float[] values = new float[] { value };
            return this.TransByte(values);
        }

        public virtual byte TransByte(byte[] buffer, int index)
        {
            return buffer[index];
        }

        public virtual byte[] TransByte(string value, Encoding encoding)
        {
            if (value == null)
            {
                return null;
            }
            byte[] bytes = encoding.GetBytes(value);
            return (this.IsStringReverseByteWord ? SoftBasic.BytesReverseByWord(bytes) : bytes);
        }

        public virtual byte[] TransByte(byte[] buffer, int index, int length)
        {
            byte[] destinationArray = new byte[length];
            Array.Copy(buffer, index, destinationArray, 0, length);
            return destinationArray;
        }

        public virtual byte[] TransByte(string value, int length, Encoding encoding)
        {
            if (value == null)
            {
                return null;
            }
            byte[] bytes = encoding.GetBytes(value);
            return (this.IsStringReverseByteWord ? SoftBasic.ArrayExpandToLength<byte>(SoftBasic.BytesReverseByWord(bytes), length) : SoftBasic.ArrayExpandToLength<byte>(bytes, length));
        }

        public virtual double TransDouble(byte[] buffer, int index)
        {
            return BitConverter.ToDouble(this.ByteTransDataFormat8(buffer, index), 0);
        }

        public virtual double[] TransDouble(byte[] buffer, int index, int length)
        {
            double[] numArray = new double[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = this.TransDouble(buffer, index + (8 * i));
            }
            return numArray;
        }

        public virtual short TransInt16(byte[] buffer, int index)
        {
            return BitConverter.ToInt16(buffer, index);
        }

        public virtual short[] TransInt16(byte[] buffer, int index, int length)
        {
            short[] numArray = new short[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = this.TransInt16(buffer, index + (2 * i));
            }
            return numArray;
        }

        public virtual int TransInt32(byte[] buffer, int index)
        {
            return BitConverter.ToInt32(this.ByteTransDataFormat4(buffer, index), 0);
        }

        public virtual int[] TransInt32(byte[] buffer, int index, int length)
        {
            int[] numArray = new int[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = this.TransInt32(buffer, index + (4 * i));
            }
            return numArray;
        }

        public virtual long TransInt64(byte[] buffer, int index)
        {
            return BitConverter.ToInt64(this.ByteTransDataFormat8(buffer, index), 0);
        }

        public virtual long[] TransInt64(byte[] buffer, int index, int length)
        {
            long[] numArray = new long[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = this.TransInt64(buffer, index + (8 * i));
            }
            return numArray;
        }

        public virtual float TransSingle(byte[] buffer, int index)
        {
            return BitConverter.ToSingle(this.ByteTransDataFormat4(buffer, index), 0);
        }

        public virtual float[] TransSingle(byte[] buffer, int index, int length)
        {
            float[] numArray = new float[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = this.TransSingle(buffer, index + (4 * i));
            }
            return numArray;
        }

        public virtual string TransString(byte[] buffer, Encoding encoding)
        {
            return encoding.GetString(buffer);
        }

        public virtual string TransString(byte[] buffer, int index, int length, Encoding encoding)
        {
            byte[] inBytes = this.TransByte(buffer, index, length);
            if (this.IsStringReverseByteWord)
            {
                return encoding.GetString(SoftBasic.BytesReverseByWord(inBytes));
            }
            return encoding.GetString(inBytes);
        }

        public virtual ushort TransUInt16(byte[] buffer, int index)
        {
            return BitConverter.ToUInt16(buffer, index);
        }

        public virtual ushort[] TransUInt16(byte[] buffer, int index, int length)
        {
            ushort[] numArray = new ushort[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = this.TransUInt16(buffer, index + (2 * i));
            }
            return numArray;
        }

        public virtual uint TransUInt32(byte[] buffer, int index)
        {
            return BitConverter.ToUInt32(this.ByteTransDataFormat4(buffer, index), 0);
        }

        public virtual uint[] TransUInt32(byte[] buffer, int index, int length)
        {
            uint[] numArray = new uint[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = this.TransUInt32(buffer, index + (4 * i));
            }
            return numArray;
        }

        public virtual ulong TransUInt64(byte[] buffer, int index)
        {
            return BitConverter.ToUInt64(this.ByteTransDataFormat8(buffer, index), 0);
        }

        public virtual ulong[] TransUInt64(byte[] buffer, int index, int length)
        {
            ulong[] numArray = new ulong[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = this.TransUInt64(buffer, index + (8 * i));
            }
            return numArray;
        }

        public HslCommunication.Core.DataFormat DataFormat { get; set; }

        public bool IsStringReverseByteWord { get; set; }
    }
}

