namespace HslCommunication.Core
{
    using System;

    public class ReverseBytesTransform : ByteTransformBase
    {
        public ReverseBytesTransform()
        {
        }

        public ReverseBytesTransform(DataFormat dataFormat) : base(dataFormat)
        {
        }

        public override IByteTransform CreateByDateFormat(DataFormat dataFormat)
        {
            return new ReverseBytesTransform(dataFormat) { IsStringReverseByteWord = base.IsStringReverseByteWord };
        }

        public override string ToString()
        {
            return string.Format("ReverseBytesTransform[{0}]", base.DataFormat);
        }

        public override byte[] TransByte(double[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 8];
            for (int i = 0; i < values.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(values[i]);
                Array.Reverse(bytes);
                base.ByteTransDataFormat8(bytes, 0).CopyTo(array, (int) (8 * i));
            }
            return array;
        }

        public override byte[] TransByte(short[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 2];
            for (int i = 0; i < values.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(values[i]);
                Array.Reverse(bytes);
                bytes.CopyTo(array, (int) (2 * i));
            }
            return array;
        }

        public override byte[] TransByte(int[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 4];
            for (int i = 0; i < values.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(values[i]);
                Array.Reverse(bytes);
                base.ByteTransDataFormat4(bytes, 0).CopyTo(array, (int) (4 * i));
            }
            return array;
        }

        public override byte[] TransByte(long[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 8];
            for (int i = 0; i < values.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(values[i]);
                Array.Reverse(bytes);
                base.ByteTransDataFormat8(bytes, 0).CopyTo(array, (int) (8 * i));
            }
            return array;
        }

        public override byte[] TransByte(float[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 4];
            for (int i = 0; i < values.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(values[i]);
                Array.Reverse(bytes);
                base.ByteTransDataFormat4(bytes, 0).CopyTo(array, (int) (4 * i));
            }
            return array;
        }

        public override byte[] TransByte(ushort[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 2];
            for (int i = 0; i < values.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(values[i]);
                Array.Reverse(bytes);
                bytes.CopyTo(array, (int) (2 * i));
            }
            return array;
        }

        public override byte[] TransByte(uint[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 4];
            for (int i = 0; i < values.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(values[i]);
                Array.Reverse(bytes);
                base.ByteTransDataFormat4(bytes, 0).CopyTo(array, (int) (4 * i));
            }
            return array;
        }

        public override byte[] TransByte(ulong[] values)
        {
            if (values == null)
            {
                return null;
            }
            byte[] array = new byte[values.Length * 8];
            for (int i = 0; i < values.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(values[i]);
                Array.Reverse(bytes);
                base.ByteTransDataFormat8(bytes, 0).CopyTo(array, (int) (8 * i));
            }
            return array;
        }

        public override double TransDouble(byte[] buffer, int index)
        {
            byte[] buffer2 = new byte[] { buffer[7 + index], buffer[6 + index], buffer[5 + index], buffer[4 + index], buffer[3 + index], buffer[2 + index], buffer[1 + index], buffer[index] };
            return BitConverter.ToDouble(base.ByteTransDataFormat8(buffer2, 0), 0);
        }

        public override short TransInt16(byte[] buffer, int index)
        {
            return BitConverter.ToInt16(new byte[] { buffer[1 + index], buffer[index] }, 0);
        }

        public override int TransInt32(byte[] buffer, int index)
        {
            byte[] buffer2 = new byte[] { buffer[3 + index], buffer[2 + index], buffer[1 + index], buffer[index] };
            return BitConverter.ToInt32(base.ByteTransDataFormat4(buffer2, 0), 0);
        }

        public override long TransInt64(byte[] buffer, int index)
        {
            byte[] buffer2 = new byte[] { buffer[7 + index], buffer[6 + index], buffer[5 + index], buffer[4 + index], buffer[3 + index], buffer[2 + index], buffer[1 + index], buffer[index] };
            return BitConverter.ToInt64(base.ByteTransDataFormat8(buffer2, 0), 0);
        }

        public override float TransSingle(byte[] buffer, int index)
        {
            byte[] buffer2 = new byte[] { buffer[3 + index], buffer[2 + index], buffer[1 + index], buffer[index] };
            return BitConverter.ToSingle(base.ByteTransDataFormat4(buffer2, 0), 0);
        }

        public override ushort TransUInt16(byte[] buffer, int index)
        {
            return BitConverter.ToUInt16(new byte[] { buffer[1 + index], buffer[index] }, 0);
        }

        public override uint TransUInt32(byte[] buffer, int index)
        {
            byte[] buffer2 = new byte[] { buffer[3 + index], buffer[2 + index], buffer[1 + index], buffer[index] };
            return BitConverter.ToUInt32(base.ByteTransDataFormat4(buffer2, 0), 0);
        }

        public override ulong TransUInt64(byte[] buffer, int index)
        {
            byte[] buffer2 = new byte[] { buffer[7 + index], buffer[6 + index], buffer[5 + index], buffer[4 + index], buffer[3 + index], buffer[2 + index], buffer[1 + index], buffer[index] };
            return BitConverter.ToUInt64(base.ByteTransDataFormat8(buffer2, 0), 0);
        }
    }
}

