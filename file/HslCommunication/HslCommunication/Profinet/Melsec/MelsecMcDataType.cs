namespace HslCommunication.Profinet.Melsec
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class MelsecMcDataType
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <AsciiCode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <DataCode>k__BackingField = 0;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <DataType>k__BackingField = 0;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <FromBase>k__BackingField;
        public static readonly MelsecMcDataType B = new MelsecMcDataType(160, 1, "B*", 0x10);
        public static readonly MelsecMcDataType CC = new MelsecMcDataType(0xc3, 1, "CC", 10);
        public static readonly MelsecMcDataType CN = new MelsecMcDataType(0xc5, 0, "CN", 10);
        public static readonly MelsecMcDataType CS = new MelsecMcDataType(0xc4, 1, "CS", 10);
        public static readonly MelsecMcDataType D = new MelsecMcDataType(0xa8, 0, "D*", 10);
        public static readonly MelsecMcDataType DX = new MelsecMcDataType(0xa2, 1, "DX", 0x10);
        public static readonly MelsecMcDataType DY = new MelsecMcDataType(0xa3, 1, "DY", 0x10);
        public static readonly MelsecMcDataType F = new MelsecMcDataType(0x93, 1, "F*", 10);
        public static readonly MelsecMcDataType Keyence_B = new MelsecMcDataType(160, 1, "B*", 0x10);
        public static readonly MelsecMcDataType Keyence_CC = new MelsecMcDataType(0xc3, 1, "CC", 10);
        public static readonly MelsecMcDataType Keyence_CN = new MelsecMcDataType(0xc5, 0, "CN", 10);
        public static readonly MelsecMcDataType Keyence_CS = new MelsecMcDataType(0xc4, 1, "CS", 10);
        public static readonly MelsecMcDataType Keyence_D = new MelsecMcDataType(0xa8, 0, "D*", 10);
        public static readonly MelsecMcDataType Keyence_L = new MelsecMcDataType(0x92, 1, "L*", 10);
        public static readonly MelsecMcDataType Keyence_M = new MelsecMcDataType(0x90, 1, "M*", 10);
        public static readonly MelsecMcDataType Keyence_R = new MelsecMcDataType(0xaf, 0, "R*", 10);
        public static readonly MelsecMcDataType Keyence_SD = new MelsecMcDataType(0xa9, 0, "SD", 10);
        public static readonly MelsecMcDataType Keyence_SM = new MelsecMcDataType(0x91, 1, "SM", 10);
        public static readonly MelsecMcDataType Keyence_TC = new MelsecMcDataType(0xc0, 1, "TC", 10);
        public static readonly MelsecMcDataType Keyence_TN = new MelsecMcDataType(0xc2, 0, "TN", 10);
        public static readonly MelsecMcDataType Keyence_TS = new MelsecMcDataType(0xc1, 1, "TS", 10);
        public static readonly MelsecMcDataType Keyence_W = new MelsecMcDataType(180, 0, "W*", 0x10);
        public static readonly MelsecMcDataType Keyence_X = new MelsecMcDataType(0x9c, 1, "X*", 0x10);
        public static readonly MelsecMcDataType Keyence_Y = new MelsecMcDataType(0x9d, 1, "Y*", 0x10);
        public static readonly MelsecMcDataType Keyence_ZR = new MelsecMcDataType(0xb0, 0, "ZR", 10);
        public static readonly MelsecMcDataType L = new MelsecMcDataType(0x92, 1, "L*", 10);
        public static readonly MelsecMcDataType M = new MelsecMcDataType(0x90, 1, "M*", 10);
        public static readonly MelsecMcDataType Panasonic_CN = new MelsecMcDataType(0xc5, 0, "CN", 10);
        public static readonly MelsecMcDataType Panasonic_CS = new MelsecMcDataType(0xc4, 1, "CS", 10);
        public static readonly MelsecMcDataType Panasonic_DT = new MelsecMcDataType(0xa8, 0, "D*", 10);
        public static readonly MelsecMcDataType Panasonic_L = new MelsecMcDataType(160, 1, "L*", 10);
        public static readonly MelsecMcDataType Panasonic_LD = new MelsecMcDataType(180, 0, "W*", 10);
        public static readonly MelsecMcDataType Panasonic_R = new MelsecMcDataType(0x90, 1, "R*", 10);
        public static readonly MelsecMcDataType Panasonic_SD = new MelsecMcDataType(0xa9, 0, "SD", 10);
        public static readonly MelsecMcDataType Panasonic_SM = new MelsecMcDataType(0x91, 1, "SM", 10);
        public static readonly MelsecMcDataType Panasonic_TN = new MelsecMcDataType(0xc2, 0, "TN", 10);
        public static readonly MelsecMcDataType Panasonic_TS = new MelsecMcDataType(0xc1, 1, "TS", 10);
        public static readonly MelsecMcDataType Panasonic_X = new MelsecMcDataType(0x9c, 1, "X*", 10);
        public static readonly MelsecMcDataType Panasonic_Y = new MelsecMcDataType(0x9d, 1, "Y*", 10);
        public static readonly MelsecMcDataType R = new MelsecMcDataType(0xaf, 0, "R*", 10);
        public static readonly MelsecMcDataType S = new MelsecMcDataType(0x98, 1, "S*", 10);
        public static readonly MelsecMcDataType SB = new MelsecMcDataType(0xa1, 1, "SB", 0x10);
        public static readonly MelsecMcDataType SC = new MelsecMcDataType(0xc6, 1, "SC", 10);
        public static readonly MelsecMcDataType SD = new MelsecMcDataType(0xa9, 0, "SD", 10);
        public static readonly MelsecMcDataType SM = new MelsecMcDataType(0x91, 1, "SM", 10);
        public static readonly MelsecMcDataType SN = new MelsecMcDataType(200, 0, "SN", 100);
        public static readonly MelsecMcDataType SS = new MelsecMcDataType(0xc7, 1, "SS", 10);
        public static readonly MelsecMcDataType SW = new MelsecMcDataType(0xb5, 0, "SW", 0x10);
        public static readonly MelsecMcDataType TC = new MelsecMcDataType(0xc0, 1, "TC", 10);
        public static readonly MelsecMcDataType TN = new MelsecMcDataType(0xc2, 0, "TN", 10);
        public static readonly MelsecMcDataType TS = new MelsecMcDataType(0xc1, 1, "TS", 10);
        public static readonly MelsecMcDataType V = new MelsecMcDataType(0x94, 1, "V*", 10);
        public static readonly MelsecMcDataType W = new MelsecMcDataType(180, 0, "W*", 0x10);
        public static readonly MelsecMcDataType X = new MelsecMcDataType(0x9c, 1, "X*", 0x10);
        public static readonly MelsecMcDataType Y = new MelsecMcDataType(0x9d, 1, "Y*", 0x10);
        public static readonly MelsecMcDataType Z = new MelsecMcDataType(0xcc, 0, "Z*", 10);
        public static readonly MelsecMcDataType ZR = new MelsecMcDataType(0xb0, 0, "ZR", 10);

        public MelsecMcDataType(byte code, byte type, string asciiCode, int fromBase)
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

