namespace HslCommunication.Profinet.Omron
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class OmronFinsDataType
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <BitCode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <WordCode>k__BackingField;
        public static readonly OmronFinsDataType AR = new OmronFinsDataType(0x33, 0xb3);
        public static readonly OmronFinsDataType CIO = new OmronFinsDataType(0x30, 0xb0);
        public static readonly OmronFinsDataType DM = new OmronFinsDataType(2, 130);
        public static readonly OmronFinsDataType HR = new OmronFinsDataType(50, 0xb2);
        public static readonly OmronFinsDataType TIM = new OmronFinsDataType(9, 0x89);
        public static readonly OmronFinsDataType WR = new OmronFinsDataType(0x31, 0xb1);

        public OmronFinsDataType(byte bitCode, byte wordCode)
        {
            this.BitCode = bitCode;
            this.WordCode = wordCode;
        }

        public byte BitCode { get; private set; }

        public byte WordCode { get; private set; }
    }
}

