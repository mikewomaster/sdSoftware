namespace HslCommunication.Profinet.Keyence
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class KeyenceDataType
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <AsciiCode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <DataCode>k__BackingField = 0;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <DataType>k__BackingField = 0;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <FromBase>k__BackingField;
        public static readonly KeyenceDataType B = new KeyenceDataType(160, 1, "B*", 0x10);
        public static readonly KeyenceDataType CN = new KeyenceDataType(0xc5, 0, "CN", 10);
        public static readonly KeyenceDataType CS = new KeyenceDataType(0xc4, 1, "CS", 10);
        public static readonly KeyenceDataType D = new KeyenceDataType(0xa8, 0, "D*", 10);
        public static readonly KeyenceDataType L = new KeyenceDataType(0x92, 1, "L*", 10);
        public static readonly KeyenceDataType M = new KeyenceDataType(0x90, 1, "M*", 10);
        public static readonly KeyenceDataType R = new KeyenceDataType(0xaf, 0, "R*", 10);
        public static readonly KeyenceDataType SD = new KeyenceDataType(0xa9, 0, "SD", 10);
        public static readonly KeyenceDataType SM = new KeyenceDataType(0x91, 1, "SM", 10);
        public static readonly KeyenceDataType TN = new KeyenceDataType(0xc2, 0, "TN", 10);
        public static readonly KeyenceDataType TS = new KeyenceDataType(0xc1, 1, "TS", 10);
        public static readonly KeyenceDataType W = new KeyenceDataType(180, 0, "W*", 0x10);
        public static readonly KeyenceDataType X = new KeyenceDataType(0x9c, 1, "X*", 0x10);
        public static readonly KeyenceDataType Y = new KeyenceDataType(0x9d, 1, "Y*", 0x10);
        public static readonly KeyenceDataType ZR = new KeyenceDataType(0xb0, 0, "ZR", 0x10);

        public KeyenceDataType(byte code, byte type, string asciiCode, int fromBase)
        {
            this.DataCode = code;
            this.AsciiCode = asciiCode;
            this.FromBase = fromBase;
            if (type < 2)
            {
                this.DataType = type;
            }
        }

        public string AsciiCode { get; private set; }

        public byte DataCode { get; private set; }

        public byte DataType { get; private set; }

        public int FromBase { get; private set; }
    }
}

