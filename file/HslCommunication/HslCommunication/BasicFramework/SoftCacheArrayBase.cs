namespace HslCommunication.BasicFramework
{
    using HslCommunication.Core;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public abstract class SoftCacheArrayBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <ArrayLength>k__BackingField;
        protected byte[] DataBytes = null;
        protected SimpleHybirdLock HybirdLock = new SimpleHybirdLock();

        protected SoftCacheArrayBase()
        {
        }

        public byte[] GetAllData()
        {
            byte[] array = new byte[this.DataBytes.Length];
            this.DataBytes.CopyTo(array, 0);
            return array;
        }

        public virtual void LoadFromBytes(byte[] dataSave)
        {
        }

        public int ArrayLength { get; protected set; }
    }
}

