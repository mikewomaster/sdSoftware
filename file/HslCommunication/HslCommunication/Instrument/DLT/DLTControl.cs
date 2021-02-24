namespace HslCommunication.Instrument.DLT
{
    using System;

    public class DLTControl
    {
        public const byte Broadcast = 8;
        public const byte ChangeBaudRate = 0x17;
        public const byte ChangePassword = 0x18;
        public const byte ClearMaxQuantityDemanded = 0x19;
        public const byte ClosingAlarmPowerpProtection = 0x1c;
        public const byte ElectricityReset = 0x1a;
        public const byte EventReset = 0x1b;
        public const byte FreezeCommand = 0x16;
        public const byte MultiFunctionTerminalOutputControlCommand = 0x1d;
        public const byte ReadAddress = 0x13;
        public const byte ReadData = 0x11;
        public const byte ReadFollowData = 0x12;
        public const byte Retain = 0;
        public const byte SecurityAuthenticationCommand = 3;
        public const byte WriteAddress = 0x15;
        public const byte WriteData = 20;
    }
}

