namespace HslCommunication.Profinet.Melsec
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class MelsecMcRDataType
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <AsciiCode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <DataCode>k__BackingField = new byte[2];
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <DataType>k__BackingField = 0;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <FromBase>k__BackingField;
        public static readonly MelsecMcRDataType B;
        public static readonly MelsecMcRDataType CC;
        public static readonly MelsecMcRDataType CN;
        public static readonly MelsecMcRDataType CS;
        public static readonly MelsecMcRDataType D;
        public static readonly MelsecMcRDataType DX;
        public static readonly MelsecMcRDataType DY;
        public static readonly MelsecMcRDataType F;
        public static readonly MelsecMcRDataType L;
        public static readonly MelsecMcRDataType LCC;
        public static readonly MelsecMcRDataType LCN;
        public static readonly MelsecMcRDataType LCS;
        public static readonly MelsecMcRDataType LSTC;
        public static readonly MelsecMcRDataType LSTN;
        public static readonly MelsecMcRDataType LSTS;
        public static readonly MelsecMcRDataType LTC;
        public static readonly MelsecMcRDataType LTN;
        public static readonly MelsecMcRDataType LTS;
        public static readonly MelsecMcRDataType M;
        public static readonly MelsecMcRDataType R;
        public static readonly MelsecMcRDataType S;
        public static readonly MelsecMcRDataType SB;
        public static readonly MelsecMcRDataType SD;
        public static readonly MelsecMcRDataType SM;
        public static readonly MelsecMcRDataType STC;
        public static readonly MelsecMcRDataType STN;
        public static readonly MelsecMcRDataType STS;
        public static readonly MelsecMcRDataType SW;
        public static readonly MelsecMcRDataType TC;
        public static readonly MelsecMcRDataType TN;
        public static readonly MelsecMcRDataType TS;
        public static readonly MelsecMcRDataType V;
        public static readonly MelsecMcRDataType W;
        public static readonly MelsecMcRDataType X;
        public static readonly MelsecMcRDataType Y;
        public static readonly MelsecMcRDataType Z;

        static MelsecMcRDataType()
        {
            byte[] code = new byte[2];
            code[0] = 0x9c;
            X = new MelsecMcRDataType(code, 1, "X***", 0x10);
            byte[] buffer2 = new byte[2];
            buffer2[0] = 0x9d;
            Y = new MelsecMcRDataType(buffer2, 1, "Y***", 0x10);
            byte[] buffer3 = new byte[2];
            buffer3[0] = 0x90;
            M = new MelsecMcRDataType(buffer3, 1, "M***", 10);
            byte[] buffer4 = new byte[2];
            buffer4[0] = 0x91;
            SM = new MelsecMcRDataType(buffer4, 1, "SM**", 10);
            byte[] buffer5 = new byte[2];
            buffer5[0] = 0x92;
            L = new MelsecMcRDataType(buffer5, 1, "L***", 10);
            byte[] buffer6 = new byte[2];
            buffer6[0] = 0x93;
            F = new MelsecMcRDataType(buffer6, 1, "F***", 10);
            byte[] buffer7 = new byte[2];
            buffer7[0] = 0x94;
            V = new MelsecMcRDataType(buffer7, 1, "V***", 10);
            byte[] buffer8 = new byte[2];
            buffer8[0] = 0x98;
            S = new MelsecMcRDataType(buffer8, 1, "S***", 10);
            byte[] buffer9 = new byte[2];
            buffer9[0] = 160;
            B = new MelsecMcRDataType(buffer9, 1, "B***", 0x10);
            byte[] buffer10 = new byte[2];
            buffer10[0] = 0xa1;
            SB = new MelsecMcRDataType(buffer10, 1, "SB**", 0x10);
            byte[] buffer11 = new byte[2];
            buffer11[0] = 0xa2;
            DX = new MelsecMcRDataType(buffer11, 1, "DX**", 0x10);
            byte[] buffer12 = new byte[2];
            buffer12[0] = 0xa3;
            DY = new MelsecMcRDataType(buffer12, 1, "DY**", 0x10);
            byte[] buffer13 = new byte[2];
            buffer13[0] = 0xa8;
            D = new MelsecMcRDataType(buffer13, 0, "D***", 10);
            byte[] buffer14 = new byte[2];
            buffer14[0] = 0xa9;
            SD = new MelsecMcRDataType(buffer14, 0, "SD**", 10);
            byte[] buffer15 = new byte[2];
            buffer15[0] = 180;
            W = new MelsecMcRDataType(buffer15, 0, "W***", 0x10);
            byte[] buffer16 = new byte[2];
            buffer16[0] = 0xb5;
            SW = new MelsecMcRDataType(buffer16, 0, "SW**", 0x10);
            byte[] buffer17 = new byte[2];
            buffer17[0] = 0xaf;
            R = new MelsecMcRDataType(buffer17, 0, "R***", 10);
            byte[] buffer18 = new byte[2];
            buffer18[0] = 0xcc;
            Z = new MelsecMcRDataType(buffer18, 0, "Z***", 10);
            byte[] buffer19 = new byte[2];
            buffer19[0] = 0x59;
            LSTS = new MelsecMcRDataType(buffer19, 1, "LSTS", 10);
            byte[] buffer20 = new byte[2];
            buffer20[0] = 0x58;
            LSTC = new MelsecMcRDataType(buffer20, 1, "LSTC", 10);
            byte[] buffer21 = new byte[2];
            buffer21[0] = 90;
            LSTN = new MelsecMcRDataType(buffer21, 0, "LSTN", 10);
            byte[] buffer22 = new byte[2];
            buffer22[0] = 0xc7;
            STS = new MelsecMcRDataType(buffer22, 1, "STS*", 10);
            byte[] buffer23 = new byte[2];
            buffer23[0] = 0xc6;
            STC = new MelsecMcRDataType(buffer23, 1, "STC*", 10);
            byte[] buffer24 = new byte[2];
            buffer24[0] = 200;
            STN = new MelsecMcRDataType(buffer24, 0, "STN*", 10);
            byte[] buffer25 = new byte[2];
            buffer25[0] = 0x51;
            LTS = new MelsecMcRDataType(buffer25, 1, "LTS*", 10);
            byte[] buffer26 = new byte[2];
            buffer26[0] = 80;
            LTC = new MelsecMcRDataType(buffer26, 1, "LTC*", 10);
            byte[] buffer27 = new byte[2];
            buffer27[0] = 0x52;
            LTN = new MelsecMcRDataType(buffer27, 0, "LTN*", 10);
            byte[] buffer28 = new byte[2];
            buffer28[0] = 0xc1;
            TS = new MelsecMcRDataType(buffer28, 1, "TS**", 10);
            byte[] buffer29 = new byte[2];
            buffer29[0] = 0xc0;
            TC = new MelsecMcRDataType(buffer29, 1, "TC**", 10);
            byte[] buffer30 = new byte[2];
            buffer30[0] = 0xc2;
            TN = new MelsecMcRDataType(buffer30, 0, "TN**", 10);
            byte[] buffer31 = new byte[2];
            buffer31[0] = 0x55;
            LCS = new MelsecMcRDataType(buffer31, 1, "LCS*", 10);
            byte[] buffer32 = new byte[2];
            buffer32[0] = 0x54;
            LCC = new MelsecMcRDataType(buffer32, 1, "LCC*", 10);
            byte[] buffer33 = new byte[2];
            buffer33[0] = 0x56;
            LCN = new MelsecMcRDataType(buffer33, 0, "LCN*", 10);
            byte[] buffer34 = new byte[2];
            buffer34[0] = 0xc4;
            CS = new MelsecMcRDataType(buffer34, 1, "CS**", 10);
            byte[] buffer35 = new byte[2];
            buffer35[0] = 0xc3;
            CC = new MelsecMcRDataType(buffer35, 1, "CC**", 10);
            byte[] buffer36 = new byte[2];
            buffer36[0] = 0xc5;
            CN = new MelsecMcRDataType(buffer36, 0, "CN**", 10);
        }

        public MelsecMcRDataType(byte[] code, byte type, string asciiCode, int fromBase)
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

