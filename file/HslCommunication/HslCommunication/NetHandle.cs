namespace HslCommunication
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct NetHandle
    {
        [FieldOffset(0)]
        private ushort m_CodeIdentifier;
        [FieldOffset(3)]
        private byte m_CodeMajor;
        [FieldOffset(2)]
        private byte m_CodeMinor;
        [FieldOffset(0)]
        private int m_CodeValue;

        public NetHandle(int value)
        {
            this.m_CodeMajor = 0;
            this.m_CodeMinor = 0;
            this.m_CodeIdentifier = 0;
            this.m_CodeValue = value;
        }

        public NetHandle(byte major, byte minor, ushort identifier)
        {
            this.m_CodeValue = 0;
            this.m_CodeMajor = major;
            this.m_CodeMinor = minor;
            this.m_CodeIdentifier = identifier;
        }

        public override bool Equals(object obj)
        {
            NetHandle handle;
            return (((!(obj is NetHandle) ? 0 : 1) != 0) && this.CodeValue.Equals(handle.CodeValue));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static NetHandle operator +(NetHandle netHandle1, NetHandle netHandle2)
        {
            return new NetHandle(netHandle1.CodeValue + netHandle2.CodeValue);
        }

        public static bool operator ==(NetHandle netHandle1, NetHandle netHandle2)
        {
            return (netHandle1.CodeValue == netHandle2.CodeValue);
        }

        public static bool operator >(NetHandle netHandle1, NetHandle netHandle2)
        {
            return (netHandle1.CodeValue > netHandle2.CodeValue);
        }

        public static implicit operator int(NetHandle netHandle)
        {
            return netHandle.m_CodeValue;
        }

        public static implicit operator NetHandle(int value)
        {
            return new NetHandle(value);
        }

        public static bool operator !=(NetHandle netHandle1, NetHandle netHandle2)
        {
            return (netHandle1.CodeValue != netHandle2.CodeValue);
        }

        public static bool operator <(NetHandle netHandle1, NetHandle netHandle2)
        {
            return (netHandle1.CodeValue < netHandle2.CodeValue);
        }

        public static NetHandle operator -(NetHandle netHandle1, NetHandle netHandle2)
        {
            return new NetHandle(netHandle1.CodeValue - netHandle2.CodeValue);
        }

        public override string ToString()
        {
            return this.m_CodeValue.ToString();
        }

        public ushort CodeIdentifier
        {
            get
            {
                return this.m_CodeIdentifier;
            }
            private set
            {
                this.m_CodeIdentifier = value;
            }
        }

        public byte CodeMajor
        {
            get
            {
                return this.m_CodeMajor;
            }
            private set
            {
                this.m_CodeMajor = value;
            }
        }

        public byte CodeMinor
        {
            get
            {
                return this.m_CodeMinor;
            }
            private set
            {
                this.m_CodeMinor = value;
            }
        }

        public int CodeValue
        {
            get
            {
                return this.m_CodeValue;
            }
            set
            {
                this.m_CodeValue = value;
            }
        }
    }
}

