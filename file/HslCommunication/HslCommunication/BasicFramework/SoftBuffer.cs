namespace HslCommunication.BasicFramework
{
    using HslCommunication.Core;
    using System;

    public class SoftBuffer : IDisposable
    {
        private byte[] buffer;
        private IByteTransform byteTransform;
        private int capacity;
        private bool disposedValue;
        private SimpleHybirdLock hybirdLock;
        private bool isBoolReverseByWord;

        public SoftBuffer()
        {
            this.capacity = 10;
            this.isBoolReverseByWord = false;
            this.disposedValue = false;
            this.buffer = new byte[this.capacity];
            this.hybirdLock = new SimpleHybirdLock();
            this.byteTransform = new RegularByteTransform();
        }

        public SoftBuffer(int capacity)
        {
            this.capacity = 10;
            this.isBoolReverseByWord = false;
            this.disposedValue = false;
            this.buffer = new byte[capacity];
            this.capacity = capacity;
            this.hybirdLock = new SimpleHybirdLock();
            this.byteTransform = new RegularByteTransform();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if (this.hybirdLock != null)
                    {
                        this.hybirdLock.Dispose();
                    }
                    else
                    {
                        SimpleHybirdLock hybirdLock = this.hybirdLock;
                    }
                    this.buffer = null;
                }
                this.disposedValue = true;
            }
        }

        private byte getAndByte(int offset)
        {
            switch (offset)
            {
                case 0:
                    return 0xfe;

                case 1:
                    return 0xfd;

                case 2:
                    return 0xfb;

                case 3:
                    return 0xf7;

                case 4:
                    return 0xef;

                case 5:
                    return 0xdf;

                case 6:
                    return 0xbf;

                case 7:
                    return 0x7f;
            }
            return 0xff;
        }

        public bool GetBool(int destIndex)
        {
            return this.GetBool(destIndex, 1)[0];
        }

        public bool[] GetBool(int destIndex, int length)
        {
            bool[] flagArray = new bool[length];
            try
            {
                this.hybirdLock.Enter();
                for (int i = 0; i < length; i++)
                {
                    int index = (destIndex + i) / 8;
                    int offset = (destIndex + i) % 8;
                    if (this.isBoolReverseByWord)
                    {
                        if ((index % 2) == 0)
                        {
                            index++;
                        }
                        else
                        {
                            index--;
                        }
                    }
                    flagArray[i] = (this.buffer[index] & this.getOrByte(offset)) == this.getOrByte(offset);
                }
                this.hybirdLock.Leave();
            }
            catch
            {
                this.hybirdLock.Leave();
                throw;
            }
            return flagArray;
        }

        public byte GetByte(int index)
        {
            return this.GetBytes(index, 1)[0];
        }

        public byte[] GetBytes()
        {
            return this.GetBytes(0, this.capacity);
        }

        public byte[] GetBytes(int index, int length)
        {
            byte[] destinationArray = new byte[length];
            if (length > 0)
            {
                this.hybirdLock.Enter();
                if ((index >= 0) && ((index + length) <= this.buffer.Length))
                {
                    Array.Copy(this.buffer, index, destinationArray, 0, length);
                }
                this.hybirdLock.Leave();
            }
            return destinationArray;
        }

        public T GetCustomer<T>(int index) where T: IDataTransfer, new()
        {
            T local = Activator.CreateInstance<T>();
            byte[] bytes = this.GetBytes(index, local.ReadCount);
            local.ParseSource(bytes);
            return local;
        }

        public double GetDouble(int index)
        {
            return this.GetDouble(index, 1)[0];
        }

        public double[] GetDouble(int index, int length)
        {
            return this.byteTransform.TransDouble(this.GetBytes(index, length * 8), 0, length);
        }

        public short GetInt16(int index)
        {
            return this.GetInt16(index, 1)[0];
        }

        public short[] GetInt16(int index, int length)
        {
            return this.byteTransform.TransInt16(this.GetBytes(index, length * 2), 0, length);
        }

        public int GetInt32(int index)
        {
            return this.GetInt32(index, 1)[0];
        }

        public int[] GetInt32(int index, int length)
        {
            return this.byteTransform.TransInt32(this.GetBytes(index, length * 4), 0, length);
        }

        public long GetInt64(int index)
        {
            return this.GetInt64(index, 1)[0];
        }

        public long[] GetInt64(int index, int length)
        {
            return this.byteTransform.TransInt64(this.GetBytes(index, length * 8), 0, length);
        }

        private byte getOrByte(int offset)
        {
            switch (offset)
            {
                case 0:
                    return 1;

                case 1:
                    return 2;

                case 2:
                    return 4;

                case 3:
                    return 8;

                case 4:
                    return 0x10;

                case 5:
                    return 0x20;

                case 6:
                    return 0x40;

                case 7:
                    return 0x80;
            }
            return 0;
        }

        public float GetSingle(int index)
        {
            return this.GetSingle(index, 1)[0];
        }

        public float[] GetSingle(int index, int length)
        {
            return this.byteTransform.TransSingle(this.GetBytes(index, length * 4), 0, length);
        }

        public ushort GetUInt16(int index)
        {
            return this.GetUInt16(index, 1)[0];
        }

        public ushort[] GetUInt16(int index, int length)
        {
            return this.byteTransform.TransUInt16(this.GetBytes(index, length * 2), 0, length);
        }

        public uint GetUInt32(int index)
        {
            return this.GetUInt32(index, 1)[0];
        }

        public uint[] GetUInt32(int index, int length)
        {
            return this.byteTransform.TransUInt32(this.GetBytes(index, length * 4), 0, length);
        }

        public ulong GetUInt64(int index)
        {
            return this.GetUInt64(index, 1)[0];
        }

        public ulong[] GetUInt64(int index, int length)
        {
            return this.byteTransform.TransUInt64(this.GetBytes(index, length * 8), 0, length);
        }

        public void SetBool(bool value, int destIndex)
        {
            bool[] flagArray1 = new bool[] { value };
            this.SetBool(flagArray1, destIndex);
        }

        public void SetBool(bool[] value, int destIndex)
        {
            if (value > null)
            {
                try
                {
                    this.hybirdLock.Enter();
                    for (int i = 0; i < value.Length; i++)
                    {
                        int index = (destIndex + i) / 8;
                        int offset = (destIndex + i) % 8;
                        if (this.isBoolReverseByWord)
                        {
                            if ((index % 2) == 0)
                            {
                                index++;
                            }
                            else
                            {
                                index--;
                            }
                        }
                        if (value[i])
                        {
                            this.buffer[index] = (byte) (this.buffer[index] | this.getOrByte(offset));
                        }
                        else
                        {
                            this.buffer[index] = (byte) (this.buffer[index] & this.getAndByte(offset));
                        }
                    }
                    this.hybirdLock.Leave();
                }
                catch
                {
                    this.hybirdLock.Leave();
                    throw;
                }
            }
        }

        public void SetBytes(byte[] data, int destIndex)
        {
            if (((destIndex < this.capacity) && (destIndex >= 0)) && (data > null))
            {
                this.hybirdLock.Enter();
                if ((data.Length + destIndex) > this.buffer.Length)
                {
                    Array.Copy(data, 0, this.buffer, destIndex, this.buffer.Length - destIndex);
                }
                else
                {
                    data.CopyTo(this.buffer, destIndex);
                }
                this.hybirdLock.Leave();
            }
        }

        public void SetBytes(byte[] data, int destIndex, int length)
        {
            if (((destIndex < this.capacity) && (destIndex >= 0)) && (data > null))
            {
                if (length > data.Length)
                {
                    length = data.Length;
                }
                this.hybirdLock.Enter();
                if ((length + destIndex) > this.buffer.Length)
                {
                    Array.Copy(data, 0, this.buffer, destIndex, this.buffer.Length - destIndex);
                }
                else
                {
                    Array.Copy(data, 0, this.buffer, destIndex, length);
                }
                this.hybirdLock.Leave();
            }
        }

        public void SetBytes(byte[] data, int sourceIndex, int destIndex, int length)
        {
            if (((destIndex < this.capacity) && (destIndex >= 0)) && (data > null))
            {
                if (length > data.Length)
                {
                    length = data.Length;
                }
                this.hybirdLock.Enter();
                Array.Copy(data, sourceIndex, this.buffer, destIndex, length);
                this.hybirdLock.Leave();
            }
        }

        public void SetCustomer<T>(T data, int index) where T: IDataTransfer, new()
        {
            this.SetBytes(data.ToSource(), index);
        }

        public void SetValue(byte value, int index)
        {
            byte[] data = new byte[] { value };
            this.SetBytes(data, index);
        }

        public void SetValue(short[] values, int index)
        {
            this.SetBytes(this.byteTransform.TransByte(values), index);
        }

        public void SetValue(short value, int index)
        {
            short[] values = new short[] { value };
            this.SetValue(values, index);
        }

        public void SetValue(int[] values, int index)
        {
            this.SetBytes(this.byteTransform.TransByte(values), index);
        }

        public void SetValue(long[] values, int index)
        {
            this.SetBytes(this.byteTransform.TransByte(values), index);
        }

        public void SetValue(float[] values, int index)
        {
            this.SetBytes(this.byteTransform.TransByte(values), index);
        }

        public void SetValue(ushort[] values, int index)
        {
            this.SetBytes(this.byteTransform.TransByte(values), index);
        }

        public void SetValue(uint[] values, int index)
        {
            this.SetBytes(this.byteTransform.TransByte(values), index);
        }

        public void SetValue(double value, int index)
        {
            double[] values = new double[] { value };
            this.SetValue(values, index);
        }

        public void SetValue(int value, int index)
        {
            int[] values = new int[] { value };
            this.SetValue(values, index);
        }

        public void SetValue(long value, int index)
        {
            long[] values = new long[] { value };
            this.SetValue(values, index);
        }

        public void SetValue(float value, int index)
        {
            float[] values = new float[] { value };
            this.SetValue(values, index);
        }

        public void SetValue(ushort value, int index)
        {
            ushort[] values = new ushort[] { value };
            this.SetValue(values, index);
        }

        public void SetValue(uint value, int index)
        {
            uint[] values = new uint[] { value };
            this.SetValue(values, index);
        }

        public void SetValue(ulong value, int index)
        {
            ulong[] values = new ulong[] { value };
            this.SetValue(values, index);
        }

        public void SetValue(double[] values, int index)
        {
            this.SetBytes(this.byteTransform.TransByte(values), index);
        }

        public void SetValue(ulong[] values, int index)
        {
            this.SetBytes(this.byteTransform.TransByte(values), index);
        }

        public override string ToString()
        {
            return string.Format("SoftBuffer[{0}][{1}]", this.capacity, this.ByteTransform);
        }

        public IByteTransform ByteTransform
        {
            get
            {
                return this.byteTransform;
            }
            set
            {
                this.byteTransform = value;
            }
        }

        public bool IsBoolReverseByWord
        {
            get
            {
                return this.isBoolReverseByWord;
            }
            set
            {
                this.isBoolReverseByWord = value;
            }
        }
    }
}

