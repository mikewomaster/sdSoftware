namespace HslCommunication.BasicFramework
{
    using HslCommunication.Core;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class SharpList<T>
    {
        private T[] array;
        private int capacity;
        private int count;
        private SimpleHybirdLock hybirdLock;
        private int lastIndex;

        public SharpList(int count, [Optional, DefaultParameterValue(false)] bool appendLast)
        {
            this.capacity = 0x800;
            this.count = 0;
            this.lastIndex = 0;
            if (count > 0x2000)
            {
                this.capacity = 0x1000;
            }
            this.array = new T[this.capacity + count];
            this.hybirdLock = new SimpleHybirdLock();
            this.count = count;
            if (appendLast)
            {
                this.lastIndex = count;
            }
        }

        public void Add(T value)
        {
            this.hybirdLock.Enter();
            if (this.lastIndex < (this.capacity + this.count))
            {
                int lastIndex = this.lastIndex;
                this.lastIndex = lastIndex + 1;
                this.array[lastIndex] = value;
            }
            else
            {
                T[] destinationArray = new T[this.capacity + this.count];
                Array.Copy(this.array, this.capacity, destinationArray, 0, this.count);
                this.array = destinationArray;
                this.lastIndex = this.count;
            }
            this.hybirdLock.Leave();
        }

        public void Add(IEnumerable<T> values)
        {
            foreach (T local in values)
            {
                this.Add(local);
            }
        }

        public T[] ToArray()
        {
            T[] destinationArray = null;
            this.hybirdLock.Enter();
            if (this.lastIndex < this.count)
            {
                destinationArray = new T[this.lastIndex];
                Array.Copy(this.array, 0, destinationArray, 0, this.lastIndex);
            }
            else
            {
                destinationArray = new T[this.count];
                Array.Copy(this.array, this.lastIndex - this.count, destinationArray, 0, this.count);
            }
            this.hybirdLock.Leave();
            return destinationArray;
        }

        public override string ToString()
        {
            return string.Format("SharpList<{0}>[{1}]", typeof(T), this.capacity);
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0)
                {
                    throw new IndexOutOfRangeException("Index must larger than zero");
                }
                if (index >= this.count)
                {
                    throw new IndexOutOfRangeException("Index must smaller than array length");
                }
                T local = default(T);
                this.hybirdLock.Enter();
                if (this.lastIndex < this.count)
                {
                    local = this.array[index];
                }
                else
                {
                    local = this.array[(index + this.lastIndex) - this.count];
                }
                this.hybirdLock.Leave();
                return local;
            }
            set
            {
                if (index < 0)
                {
                    throw new IndexOutOfRangeException("Index must larger than zero");
                }
                if (index >= this.count)
                {
                    throw new IndexOutOfRangeException("Index must smaller than array length");
                }
                this.hybirdLock.Enter();
                if (this.lastIndex < this.count)
                {
                    this.array[index] = value;
                }
                else
                {
                    this.array[(index + this.lastIndex) - this.count] = value;
                }
                this.hybirdLock.Leave();
            }
        }
    }
}

