namespace HslCommunication.Robot.FANUC
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using System;

    public class FanucHelper
    {
        public const byte SELECTOR_AI = 10;
        public const byte SELECTOR_AQ = 12;
        public const byte SELECTOR_D = 8;
        public const byte SELECTOR_G = 0x38;
        public const byte SELECTOR_I = 70;
        public const byte SELECTOR_M = 0x4c;
        public const byte SELECTOR_Q = 0x48;

        public static OperateResult<byte, ushort> AnalysisFanucAddress(string address)
        {
            try
            {
                if (address.StartsWith("aq") || address.StartsWith("AQ"))
                {
                    return OperateResult.CreateSuccessResult<byte, ushort>(12, ushort.Parse(address.Substring(2)));
                }
                if (address.StartsWith("ai") || address.StartsWith("AI"))
                {
                    return OperateResult.CreateSuccessResult<byte, ushort>(10, ushort.Parse(address.Substring(2)));
                }
                if (address.StartsWith("i") || address.StartsWith("I"))
                {
                    return OperateResult.CreateSuccessResult<byte, ushort>(70, ushort.Parse(address.Substring(1)));
                }
                if (address.StartsWith("q") || address.StartsWith("Q"))
                {
                    return OperateResult.CreateSuccessResult<byte, ushort>(0x48, ushort.Parse(address.Substring(1)));
                }
                if (address.StartsWith("m") || address.StartsWith("M"))
                {
                    return OperateResult.CreateSuccessResult<byte, ushort>(0x4c, ushort.Parse(address.Substring(1)));
                }
                if (address.StartsWith("d") || address.StartsWith("D"))
                {
                    return OperateResult.CreateSuccessResult<byte, ushort>(8, ushort.Parse(address.Substring(1)));
                }
                return new OperateResult<byte, ushort>(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte, ushort>(exception.Message);
            }
        }

        public static byte[] BuildReadResponseData(byte[] data)
        {
            byte[] buffer = SoftBasic.HexStringToBytes("\r\n03 00 06 00 e4 2f 00 00 00 01 00 00 00 00 00 00\r\n00 01 00 00 00 00 00 00 00 00 00 00 00 00 06 94\r\n10 0e 00 00 30 3a 00 00 01 01 00 00 00 00 00 00\r\n01 01 ff 04 00 00 7c 21");
            if (data.Length > 6)
            {
                buffer = SoftBasic.SpliceTwoByteArray(buffer, data);
                buffer[4] = BitConverter.GetBytes(data.Length)[0];
                buffer[5] = BitConverter.GetBytes(data.Length)[1];
                return buffer;
            }
            buffer[4] = 0;
            buffer[5] = 0;
            buffer[0x1f] = 0xd4;
            data.CopyTo(buffer, 0x2c);
            return buffer;
        }

        public static byte[] BuildWriteData(byte sel, ushort address, byte[] value, int length)
        {
            if (value == null)
            {
                value = new byte[0];
            }
            if (value.Length > 6)
            {
                byte[] buffer = new byte[0x38 + value.Length];
                new byte[] { 
                    2, 0, 9, 0, 50, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0,
                    0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0x80,
                    0, 0, 0, 0, 0x10, 14, 0, 0, 1, 1, 50, 0, 0, 0, 0, 0,
                    1, 1, 7, 8, 0x31, 0, 0x19, 0
                }.CopyTo(buffer, 0);
                value.CopyTo(buffer, 0x38);
                buffer[4] = BitConverter.GetBytes(value.Length)[0];
                buffer[5] = BitConverter.GetBytes(value.Length)[1];
                buffer[0x33] = sel;
                buffer[0x34] = BitConverter.GetBytes((int) (address - 1))[0];
                buffer[0x35] = BitConverter.GetBytes((int) (address - 1))[1];
                buffer[0x36] = BitConverter.GetBytes(length)[0];
                buffer[0x37] = BitConverter.GetBytes(length)[1];
                return buffer;
            }
            byte[] array = new byte[] { 
                2, 0, 8, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0xc0,
                0, 0, 0, 0, 0x10, 14, 0, 0, 1, 1, 7, 8, 9, 0, 4, 0,
                1, 0, 2, 0, 3, 0, 4, 0
            };
            array[0x2b] = sel;
            array[0x2c] = BitConverter.GetBytes((int) (address - 1))[0];
            array[0x2d] = BitConverter.GetBytes((int) (address - 1))[1];
            array[0x2e] = BitConverter.GetBytes(length)[0];
            array[0x2f] = BitConverter.GetBytes(length)[1];
            value.CopyTo(array, 0x30);
            return array;
        }

        public static byte[] BulidReadData(byte sel, ushort address, ushort length)
        {
            byte[] buffer = new byte[] { 
                2, 0, 6, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0xc0,
                0, 0, 0, 0, 0x10, 14, 0, 0, 1, 1, 4, 8, 0, 0, 2, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            };
            buffer[0x2b] = sel;
            buffer[0x2c] = BitConverter.GetBytes((int) (address - 1))[0];
            buffer[0x2d] = BitConverter.GetBytes((int) (address - 1))[1];
            buffer[0x2e] = BitConverter.GetBytes(length)[0];
            buffer[0x2f] = BitConverter.GetBytes(length)[1];
            return buffer;
        }

        public static string[] GetFanucCmds()
        {
            return new string[] { 
                "CLRASG", "SETASG 1 500 ALM[E1] 1", "SETASG 501 100 ALM[1] 1", "SETASG 601 100 ALM[P1] 1", "SETASG 701 50 POS[15] 0.0", "SETASG 751 50 POS[15] 0.0", "SETASG 801 50 POS[G2: 15] 0.0", "SETASG 851 50 POS[G3: 0] 0.0", "SETASG 901 50 POS[G4:0] 0.0", "SETASG 951 50 POS[G5:0] 0.0", "SETASG 1001 18 PRG[1] 1", "SETASG 1019 18 PRG[M1] 1", "SETASG 1037 18 PRG[K1] 1", "SETASG 1055 18 PRG[MK1] 1", "SETASG 1073 500 PR[1] 0.0", "SETASG 1573 200 PR[G2:1] 0.0",
                "SETASG 1773 500 PR[G3:1] 0.0", "SETASG 2273 500 PR[G4: 1] 0.0", "SETASG 2773 500 PR[G5: 1] 0.0", "SETASG 3273 2 $FAST_CLOCK 1", "SETASG 3275 2 $TIMER[10].$TIMER_VAL 1", "SETASG 3277 2 $MOR_GRP[1].$CURRENT_ANG[1] 0", "SETASG 3279 2 $DUTY_TEMP 0", "SETASG 3281 40 $TIMER[10].$COMMENT 1", "SETASG 3321 40 $TIMER[2].$COMMENT 1", "SETASG 3361 50 $MNUTOOL[1,1] 0.0", "SETASG 3411 40 $[HTTPKCL]CMDS[1] 1", "SETASG 3451 10 R[1] 1.0", "SETASG 3461 10 R[6] 0", "SETASG 3471 250 PR[1]@1.25 0.0", "SETASG 3721 250 PR[1]@1.25 0.0", "SETASG 3971 120 PR[G2:1]@27.12 0.0",
                "SETASG 4091 120 DI[C1] 1", "SETASG 4211 120 DO[C1] 1", "SETASG 4331 120 RI[C1] 1", "SETASG 4451 120 RO[C1] 1", "SETASG 4571 120 UI[C1] 1", "SETASG 4691 120 UO[C1] 1", "SETASG 4811 120 SI[C1] 1", "SETASG 4931 120 SO[C1] 1", "SETASG 5051 120 WI[C1] 1", "SETASG 5171 120 WO[C1] 1", "SETASG 5291 120 WSI[C1] 1", "SETASG 5411 120 AI[C1] 1", "SETASG 5531 120 AO[C1] 1", "SETASG 5651 120 GI[C1] 1", "SETASG 5771 120 GO[C1] 1", "SETASG 5891 120 SR[1] 1",
                "SETASG 6011 120 SR[C1] 1", "SETASG 6131 10 R[1] 1.0", "SETASG 6141 2 $TIMER[1].$TIMER_VAL 1", "SETASG 6143 2 $TIMER[2].$TIMER_VAL 1", "SETASG 6145 2 $TIMER[3].$TIMER_VAL 1", "SETASG 6147 2 $TIMER[4].$TIMER_VAL 1", "SETASG 6149 2 $TIMER[5].$TIMER_VAL 1", "SETASG 6151 2 $TIMER[6].$TIMER_VAL 1", "SETASG 6153 2 $TIMER[7].$TIMER_VAL 1", "SETASG 6155 2 $TIMER[8].$TIMER_VAL 1", "SETASG 6157 2 $TIMER[9].$TIMER_VAL 1", "SETASG 6159 2 $TIMER[10].$TIMER_VAL 1"
            };
        }
    }
}

