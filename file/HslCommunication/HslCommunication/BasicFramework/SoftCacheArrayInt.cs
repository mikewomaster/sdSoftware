namespace HslCommunication.BasicFramework
{
    using System;

    public sealed class SoftCacheArrayInt : SoftCacheArrayBase
    {
        private int[] DataArray = null;

        public SoftCacheArrayInt(int capacity, int defaultValue)
        {
            if (capacity < 10)
            {
                capacity = 10;
            }
            base.ArrayLength = capacity;
            this.DataArray = new int[capacity];
            base.DataBytes = new byte[capacity * 4];
            if (defaultValue > 0)
            {
                for (int i = 0; i < capacity; i++)
                {
                    this.DataArray[i] = defaultValue;
                }
            }
        }

        public void AddValue(int value)
        {
            base.HybirdLock.Enter();
            for (int i = 0; i < (base.ArrayLength - 1); i++)
            {
                this.DataArray[i] = this.DataArray[i + 1];
            }
            this.DataArray[base.ArrayLength - 1] = value;
            for (int j = 0; j < base.ArrayLength; j++)
            {
                BitConverter.GetBytes(this.DataArray[j]).CopyTo(base.DataBytes, (int) (4 * j));
            }
            base.HybirdLock.Leave();
        }

        public int[] GetIntArray()
        {
            int[] numArray = null;
            base.HybirdLock.Enter();
            numArray = new int[base.ArrayLength];
            for (int i = 0; i < base.ArrayLength; i++)
            {
                numArray[i] = this.DataArray[i];
            }
            base.HybirdLock.Leave();
            return numArray;
        }

        public override void LoadFromBytes(byte[] dataSave)
        {
            int num = dataSave.Length / 4;
            base.ArrayLength = num;
            this.DataArray = new int[num];
            base.DataBytes = new byte[num * 4];
            for (int i = 0; i < num; i++)
            {
                this.DataArray[i] = BitConverter.ToInt32(dataSave, i * 4);
            }
        }
    }
}

