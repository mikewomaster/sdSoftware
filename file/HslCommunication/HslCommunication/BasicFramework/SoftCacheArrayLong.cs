namespace HslCommunication.BasicFramework
{
    using System;

    public sealed class SoftCacheArrayLong : SoftCacheArrayBase
    {
        private long[] DataArray = null;

        public SoftCacheArrayLong(int capacity, int defaultValue)
        {
            if (capacity < 10)
            {
                capacity = 10;
            }
            base.ArrayLength = capacity;
            this.DataArray = new long[capacity];
            base.DataBytes = new byte[capacity * 8];
            if (defaultValue > 0)
            {
                for (int i = 0; i < capacity; i++)
                {
                    this.DataArray[i] = defaultValue;
                }
            }
        }

        public void AddValue(long value)
        {
            base.HybirdLock.Enter();
            for (int i = 0; i < (base.ArrayLength - 1); i++)
            {
                this.DataArray[i] = this.DataArray[i + 1];
            }
            this.DataArray[base.ArrayLength - 1] = value;
            for (int j = 0; j < base.ArrayLength; j++)
            {
                BitConverter.GetBytes(this.DataArray[j]).CopyTo(base.DataBytes, (int) (8 * j));
            }
            base.HybirdLock.Leave();
        }

        public override void LoadFromBytes(byte[] dataSave)
        {
            int num = dataSave.Length / 8;
            base.ArrayLength = num;
            this.DataArray = new long[num];
            base.DataBytes = new byte[num * 8];
            for (int i = 0; i < num; i++)
            {
                this.DataArray[i] = BitConverter.ToInt64(dataSave, i * 8);
            }
        }
    }
}

