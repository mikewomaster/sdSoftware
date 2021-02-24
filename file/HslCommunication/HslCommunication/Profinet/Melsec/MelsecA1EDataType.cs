namespace HslCommunication.Profinet.Melsec
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class MelsecA1EDataType
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <AsciiCode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <DataCode>k__BackingField = new byte[2];
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <DataType>k__BackingField = 0;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <FromBase>k__BackingField;
        public static readonly MelsecA1EDataType B;
        public static readonly MelsecA1EDataType CC;
        public static readonly MelsecA1EDataType CN;
        public static readonly MelsecA1EDataType CS;
        public static readonly MelsecA1EDataType D;
        public static readonly MelsecA1EDataType F;
        public static readonly MelsecA1EDataType M;
        public static readonly MelsecA1EDataType R;
        public static readonly MelsecA1EDataType S;
        public static readonly MelsecA1EDataType TC;
        public static readonly MelsecA1EDataType TN;
        public static readonly MelsecA1EDataType TS;
        public static readonly MelsecA1EDataType W;
        public static readonly MelsecA1EDataType X;
        public static readonly MelsecA1EDataType Y;

        static MelsecA1EDataType()
        {
            byte[] code = new byte[] { 0x58, 0x20 };
            X = new MelsecA1EDataType(code, 1, "X*", 0x10);
            byte[] buffer2 = new byte[] { 0x59, 0x20 };
            Y = new MelsecA1EDataType(buffer2, 1, "Y*", 0x10);
            byte[] buffer3 = new byte[] { 0x4d, 0x20 };
            M = new MelsecA1EDataType(buffer3, 1, "M*", 10);
            byte[] buffer4 = new byte[] { 0x53, 0x20 };
            S = new MelsecA1EDataType(buffer4, 1, "S*", 10);
            byte[] buffer5 = new byte[] { 70, 0x20 };
            F = new MelsecA1EDataType(buffer5, 1, "F*", 10);
            byte[] buffer6 = new byte[] { 0x42, 0x20 };
            B = new MelsecA1EDataType(buffer6, 1, "B*", 0x10);
            byte[] buffer7 = new byte[] { 0x54, 0x53 };
            TS = new MelsecA1EDataType(buffer7, 1, "TS", 10);
            byte[] buffer8 = new byte[] { 0x54, 0x43 };
            TC = new MelsecA1EDataType(buffer8, 1, "TC", 10);
            byte[] buffer9 = new byte[] { 0x54, 0x4e };
            TN = new MelsecA1EDataType(buffer9, 0, "TN", 10);
            byte[] buffer10 = new byte[] { 0x43, 0x53 };
            CS = new MelsecA1EDataType(buffer10, 1, "CS", 10);
            byte[] buffer11 = new byte[] { 0x43, 0x43 };
            CC = new MelsecA1EDataType(buffer11, 1, "CC", 10);
            byte[] buffer12 = new byte[] { 0x43, 0x4e };
            CN = new MelsecA1EDataType(buffer12, 0, "CN", 10);
            byte[] buffer13 = new byte[] { 0x44, 0x20 };
            D = new MelsecA1EDataType(buffer13, 0, "D*", 10);
            byte[] buffer14 = new byte[] { 0x57, 0x20 };
            W = new MelsecA1EDataType(buffer14, 0, "W*", 0x10);
            byte[] buffer15 = new byte[] { 0x52, 0x20 };
            R = new MelsecA1EDataType(buffer15, 0, "R*", 10);
        }

        public MelsecA1EDataType(byte[] code, byte type, string asciiCode, int fromBase)
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

        public byte[] DataCode { get; private set; }

        public byte DataType { get; private set; }

        public int FromBase { get; private set; }
    }
}

