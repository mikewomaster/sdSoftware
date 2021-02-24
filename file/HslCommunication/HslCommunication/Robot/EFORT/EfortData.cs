namespace HslCommunication.Robot.EFORT
{
    using HslCommunication;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class EfortData
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <AuthorityStatus>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <AxisMoveStatus>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float[] <DbAxisAcc>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float[] <DbAxisAccAcc>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int[] <DbAxisDirCnt>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float[] <DbAxisPos>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float[] <DbAxisSpeed>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int[] <DbAxisTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float[] <DbAxisTorque>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float[] <DbCartPos>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <DbDeviceTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <ErrorStatus>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ErrorText>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <HstopStatus>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <IoDIn>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <IoDOut>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int[] <IoIIn>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int[] <IoIOut>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <ModeStatus>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <PacketEnd>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <PacketHeartbeat>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <PacketOrders>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <PacketStart>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <ProgHoldStatus>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <ProgLoadStatus>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <ProgMoveStatus>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ProgramName>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ProjectName>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <ServoStatus>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <SpeedStatus>k__BackingField;

        public EfortData()
        {
            this.IoDOut = new byte[0x20];
            this.IoDIn = new byte[0x20];
            this.IoIOut = new int[0x20];
            this.IoIIn = new int[0x20];
            this.DbAxisPos = new float[7];
            this.DbCartPos = new float[6];
            this.DbAxisSpeed = new float[7];
            this.DbAxisAcc = new float[7];
            this.DbAxisAccAcc = new float[7];
            this.DbAxisTorque = new float[7];
            this.DbAxisDirCnt = new int[7];
            this.DbAxisTime = new int[7];
        }

        public static OperateResult<EfortData> PraseFrom(byte[] data)
        {
            if (data.Length < 0x314)
            {
                return new OperateResult<EfortData>(string.Format(StringResources.Language.DataLengthIsNotEnough, 0x314, data.Length));
            }
            EfortData data2 = new EfortData {
                PacketStart = Encoding.ASCII.GetString(data, 0, 0x10).Trim(),
                PacketOrders = BitConverter.ToUInt16(data, 0x12),
                PacketHeartbeat = BitConverter.ToUInt16(data, 20),
                ErrorStatus = data[0x16],
                HstopStatus = data[0x17],
                AuthorityStatus = data[0x18],
                ServoStatus = data[0x19],
                AxisMoveStatus = data[0x1a],
                ProgMoveStatus = data[0x1b],
                ProgLoadStatus = data[0x1c],
                ProgHoldStatus = data[0x1d],
                ModeStatus = BitConverter.ToUInt16(data, 30),
                SpeedStatus = BitConverter.ToUInt16(data, 0x20)
            };
            for (int i = 0; i < 0x20; i++)
            {
                data2.IoDOut[i] = data[0x22 + i];
            }
            for (int j = 0; j < 0x20; j++)
            {
                data2.IoDIn[j] = data[0x42 + j];
            }
            for (int k = 0; k < 0x20; k++)
            {
                data2.IoIOut[k] = BitConverter.ToInt32(data, 100 + (4 * k));
            }
            for (int m = 0; m < 0x20; m++)
            {
                data2.IoIIn[m] = BitConverter.ToInt32(data, 0xe4 + (4 * m));
            }
            data2.ProjectName = Encoding.ASCII.GetString(data, 0x164, 0x20).Trim(new char[1]);
            data2.ProgramName = Encoding.ASCII.GetString(data, 0x184, 0x20).Trim(new char[1]);
            data2.ErrorText = Encoding.ASCII.GetString(data, 420, 0x80).Trim(new char[1]);
            for (int n = 0; n < 7; n++)
            {
                data2.DbAxisPos[n] = BitConverter.ToSingle(data, 0x224 + (4 * n));
            }
            for (int num6 = 0; num6 < 6; num6++)
            {
                data2.DbCartPos[num6] = BitConverter.ToSingle(data, 0x240 + (4 * num6));
            }
            for (int num7 = 0; num7 < 7; num7++)
            {
                data2.DbAxisSpeed[num7] = BitConverter.ToSingle(data, 600 + (4 * num7));
            }
            for (int num8 = 0; num8 < 7; num8++)
            {
                data2.DbAxisAcc[num8] = BitConverter.ToSingle(data, 0x274 + (4 * num8));
            }
            for (int num9 = 0; num9 < 7; num9++)
            {
                data2.DbAxisAccAcc[num9] = BitConverter.ToSingle(data, 0x290 + (4 * num9));
            }
            for (int num10 = 0; num10 < 7; num10++)
            {
                data2.DbAxisTorque[num10] = BitConverter.ToSingle(data, 0x2ac + (4 * num10));
            }
            for (int num11 = 0; num11 < 7; num11++)
            {
                data2.DbAxisDirCnt[num11] = BitConverter.ToInt32(data, 0x2c8 + (4 * num11));
            }
            for (int num12 = 0; num12 < 7; num12++)
            {
                data2.DbAxisTime[num12] = BitConverter.ToInt32(data, 740 + (4 * num12));
            }
            data2.DbDeviceTime = BitConverter.ToInt32(data, 0x300);
            data2.PacketEnd = Encoding.ASCII.GetString(data, 0x304, 0x10).Trim();
            return OperateResult.CreateSuccessResult<EfortData>(data2);
        }

        public static OperateResult<EfortData> PraseFromPrevious(byte[] data)
        {
            if (data.Length < 0x310)
            {
                return new OperateResult<EfortData>(string.Format(StringResources.Language.DataLengthIsNotEnough, 0x310, data.Length));
            }
            EfortData data2 = new EfortData {
                PacketStart = Encoding.ASCII.GetString(data, 0, 15).Trim(),
                PacketOrders = BitConverter.ToUInt16(data, 0x11),
                PacketHeartbeat = BitConverter.ToUInt16(data, 0x13),
                ErrorStatus = data[0x15],
                HstopStatus = data[0x16],
                AuthorityStatus = data[0x17],
                ServoStatus = data[0x18],
                AxisMoveStatus = data[0x19],
                ProgMoveStatus = data[0x1a],
                ProgLoadStatus = data[0x1b],
                ProgHoldStatus = data[0x1c],
                ModeStatus = BitConverter.ToUInt16(data, 0x1d),
                SpeedStatus = BitConverter.ToUInt16(data, 0x1f)
            };
            for (int i = 0; i < 0x20; i++)
            {
                data2.IoDOut[i] = data[0x21 + i];
            }
            for (int j = 0; j < 0x20; j++)
            {
                data2.IoDIn[j] = data[0x41 + j];
            }
            for (int k = 0; k < 0x20; k++)
            {
                data2.IoIOut[k] = BitConverter.ToInt32(data, 0x61 + (4 * k));
            }
            for (int m = 0; m < 0x20; m++)
            {
                data2.IoIIn[m] = BitConverter.ToInt32(data, 0xe1 + (4 * m));
            }
            data2.ProjectName = Encoding.ASCII.GetString(data, 0x161, 0x20).Trim(new char[1]);
            data2.ProgramName = Encoding.ASCII.GetString(data, 0x181, 0x20).Trim(new char[1]);
            data2.ErrorText = Encoding.ASCII.GetString(data, 0x1a1, 0x80).Trim(new char[1]);
            for (int n = 0; n < 7; n++)
            {
                data2.DbAxisPos[n] = BitConverter.ToSingle(data, 0x221 + (4 * n));
            }
            for (int num6 = 0; num6 < 6; num6++)
            {
                data2.DbCartPos[num6] = BitConverter.ToSingle(data, 0x23d + (4 * num6));
            }
            for (int num7 = 0; num7 < 7; num7++)
            {
                data2.DbAxisSpeed[num7] = BitConverter.ToSingle(data, 0x255 + (4 * num7));
            }
            for (int num8 = 0; num8 < 7; num8++)
            {
                data2.DbAxisAcc[num8] = BitConverter.ToSingle(data, 0x271 + (4 * num8));
            }
            for (int num9 = 0; num9 < 7; num9++)
            {
                data2.DbAxisAccAcc[num9] = BitConverter.ToSingle(data, 0x28d + (4 * num9));
            }
            for (int num10 = 0; num10 < 7; num10++)
            {
                data2.DbAxisTorque[num10] = BitConverter.ToSingle(data, 0x2a9 + (4 * num10));
            }
            for (int num11 = 0; num11 < 7; num11++)
            {
                data2.DbAxisDirCnt[num11] = BitConverter.ToInt32(data, 0x2c5 + (4 * num11));
            }
            for (int num12 = 0; num12 < 7; num12++)
            {
                data2.DbAxisTime[num12] = BitConverter.ToInt32(data, 0x2e1 + (4 * num12));
            }
            data2.DbDeviceTime = BitConverter.ToInt32(data, 0x2fd);
            data2.PacketEnd = Encoding.ASCII.GetString(data, 0x301, 15).Trim();
            return OperateResult.CreateSuccessResult<EfortData>(data2);
        }

        public byte AuthorityStatus { get; set; }

        public byte AxisMoveStatus { get; set; }

        public float[] DbAxisAcc { get; set; }

        public float[] DbAxisAccAcc { get; set; }

        public int[] DbAxisDirCnt { get; set; }

        public float[] DbAxisPos { get; set; }

        public float[] DbAxisSpeed { get; set; }

        public int[] DbAxisTime { get; set; }

        public float[] DbAxisTorque { get; set; }

        public float[] DbCartPos { get; set; }

        public int DbDeviceTime { get; set; }

        public byte ErrorStatus { get; set; }

        public string ErrorText { get; set; }

        public byte HstopStatus { get; set; }

        public byte[] IoDIn { get; set; }

        public byte[] IoDOut { get; set; }

        public int[] IoIIn { get; set; }

        public int[] IoIOut { get; set; }

        public ushort ModeStatus { get; set; }

        public string PacketEnd { get; set; }

        public ushort PacketHeartbeat { get; set; }

        public ushort PacketOrders { get; set; }

        public string PacketStart { get; set; }

        public byte ProgHoldStatus { get; set; }

        public byte ProgLoadStatus { get; set; }

        public byte ProgMoveStatus { get; set; }

        public string ProgramName { get; set; }

        public string ProjectName { get; set; }

        public byte ServoStatus { get; set; }

        public ushort SpeedStatus { get; set; }
    }
}

