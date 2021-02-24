namespace HslCommunication.Robot.FANUC
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class FanucData
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <AIComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucAlarm <AlarmCurrent>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucAlarm[] <AlarmList>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucAlarm <AlarmPassword>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <AOComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucPose <CurrentPose>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucPose <CurrentPose2>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucPose <CurrentPose3>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucPose <CurrentPose4>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucPose <CurrentPose5>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucPose <CurrentPoseUF>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucPose[] <DataPosRegMG>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <DIComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <DOComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <DUTY_TEMP>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <FAST_CLOCK>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <GIComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <GOComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <HTTPKCL_CMDS>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucPose <MNUTOOL1_1>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <MOR_GRP_CURRENT_ANG>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int[] <NumReg1>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float[] <NumReg2>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucPose[] <PosRegGP1>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucPose[] <PosRegGP2>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucPose[] <PosRegGP3>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucPose[] <PosRegGP4>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucPose[] <PosRegGP5>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <RIComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <ROComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <SIComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <SOComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <STRREG_COMMENT_Comment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <STRREGComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucTask <Task>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucTask <TaskIgnoreKarel>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucTask <TaskIgnoreMacro>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FanucTask <TaskIgnoreMacroKarel>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <TIMER10_COMMENT>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <Timer10_TIMER_VAL>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <TIMER2_COMMENT>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <UIComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <UOComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <WIComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <WOComment>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <WSIComment>k__BackingField;
        private bool isIni = false;

        private static void AppendStringBuilder(StringBuilder sb, string name, string value)
        {
            string[] values = new string[] { value };
            AppendStringBuilder(sb, name, values);
        }

        private static void AppendStringBuilder(StringBuilder sb, string name, string[] values)
        {
            sb.Append(name);
            sb.Append(":");
            if (values.Length > 1)
            {
                sb.Append(Environment.NewLine);
            }
            for (int i = 0; i < values.Length; i++)
            {
                sb.Append(values[i]);
                sb.Append(Environment.NewLine);
            }
            if (values.Length > 1)
            {
                sb.Append(Environment.NewLine);
            }
        }

        private static FanucAlarm[] GetFanucAlarmArray(IByteTransform byteTransform, byte[] content, int index, int arraySize, Encoding encoding)
        {
            FanucAlarm[] alarmArray = new FanucAlarm[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                alarmArray[i] = FanucAlarm.PraseFrom(byteTransform, content, index + (200 * i), encoding);
            }
            return alarmArray;
        }

        private static FanucPose[] GetFanucPoseArray(IByteTransform byteTransform, byte[] content, int index, int arraySize, Encoding encoding)
        {
            FanucPose[] poseArray = new FanucPose[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                poseArray[i] = FanucPose.PraseFrom(byteTransform, content, index + (i * 100));
            }
            return poseArray;
        }

        private static string[] GetStringArray(byte[] content, int index, int length, int arraySize, Encoding encoding)
        {
            string[] strArray = new string[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                strArray[i] = encoding.GetString(content, index + (length * i), length).TrimEnd(new char[1]);
            }
            return strArray;
        }

        public void LoadByContent(byte[] content)
        {
            Encoding encoding;
            IByteTransform byteTransform = new RegularByteTransform();
            try
            {
                encoding = Encoding.GetEncoding("shift_jis", EncoderFallback.ReplacementFallback, new DecoderReplacementFallback());
            }
            catch
            {
                encoding = Encoding.UTF8;
            }
            string[] fanucCmds = FanucHelper.GetFanucCmds();
            int[] numArray = new int[fanucCmds.Length - 1];
            int[] numArray2 = new int[fanucCmds.Length - 1];
            for (int i = 1; i < fanucCmds.Length; i++)
            {
                MatchCollection matchs = Regex.Matches(fanucCmds[i], "[0-9]+");
                numArray[i - 1] = (int.Parse(matchs[0].Value) - 1) * 2;
                numArray2[i - 1] = int.Parse(matchs[1].Value) * 2;
            }
            this.AlarmList = GetFanucAlarmArray(byteTransform, content, numArray[0], 5, encoding);
            this.AlarmCurrent = FanucAlarm.PraseFrom(byteTransform, content, numArray[1], encoding);
            this.AlarmPassword = FanucAlarm.PraseFrom(byteTransform, content, numArray[2], encoding);
            this.CurrentPose = FanucPose.PraseFrom(byteTransform, content, numArray[3]);
            this.CurrentPoseUF = FanucPose.PraseFrom(byteTransform, content, numArray[4]);
            this.CurrentPose2 = FanucPose.PraseFrom(byteTransform, content, numArray[5]);
            this.CurrentPose3 = FanucPose.PraseFrom(byteTransform, content, numArray[6]);
            this.CurrentPose4 = FanucPose.PraseFrom(byteTransform, content, numArray[7]);
            this.CurrentPose5 = FanucPose.PraseFrom(byteTransform, content, numArray[8]);
            this.Task = FanucTask.PraseFrom(byteTransform, content, numArray[9], encoding);
            this.TaskIgnoreMacro = FanucTask.PraseFrom(byteTransform, content, numArray[10], encoding);
            this.TaskIgnoreKarel = FanucTask.PraseFrom(byteTransform, content, numArray[11], encoding);
            this.TaskIgnoreMacroKarel = FanucTask.PraseFrom(byteTransform, content, numArray[12], encoding);
            this.PosRegGP1 = GetFanucPoseArray(byteTransform, content, numArray[13], 10, encoding);
            this.PosRegGP2 = GetFanucPoseArray(byteTransform, content, numArray[14], 4, encoding);
            this.PosRegGP3 = GetFanucPoseArray(byteTransform, content, numArray[15], 10, encoding);
            this.PosRegGP4 = GetFanucPoseArray(byteTransform, content, numArray[0x10], 10, encoding);
            this.PosRegGP5 = GetFanucPoseArray(byteTransform, content, numArray[0x11], 10, encoding);
            this.FAST_CLOCK = BitConverter.ToInt32(content, numArray[0x12]);
            this.Timer10_TIMER_VAL = BitConverter.ToInt32(content, numArray[0x13]);
            this.MOR_GRP_CURRENT_ANG = BitConverter.ToSingle(content, numArray[20]);
            this.DUTY_TEMP = BitConverter.ToSingle(content, numArray[0x15]);
            this.TIMER10_COMMENT = encoding.GetString(content, numArray[0x16], 80).Trim(new char[1]);
            this.TIMER2_COMMENT = encoding.GetString(content, numArray[0x17], 80).Trim(new char[1]);
            this.MNUTOOL1_1 = FanucPose.PraseFrom(byteTransform, content, numArray[0x18]);
            this.HTTPKCL_CMDS = encoding.GetString(content, numArray[0x19], 80).Trim(new char[1]);
            this.NumReg1 = byteTransform.TransInt32(content, numArray[0x1a], 5);
            this.NumReg2 = byteTransform.TransSingle(content, numArray[0x1b], 5);
            this.DataPosRegMG = new FanucPose[10];
            for (int j = 0; j < this.DataPosRegMG.Length; j++)
            {
                this.DataPosRegMG[j] = new FanucPose();
                this.DataPosRegMG[j].Xyzwpr = byteTransform.TransSingle(content, numArray[0x1d] + (j * 50), 9);
                this.DataPosRegMG[j].Config = FanucPose.TransConfigStringArray(byteTransform.TransInt16(content, (numArray[0x1d] + 0x24) + (j * 50), 7));
                this.DataPosRegMG[j].Joint = byteTransform.TransSingle(content, numArray[30] + (j * 0x24), 9);
            }
            this.DIComment = GetStringArray(content, numArray[0x1f], 80, 3, encoding);
            this.DOComment = GetStringArray(content, numArray[0x20], 80, 3, encoding);
            this.RIComment = GetStringArray(content, numArray[0x21], 80, 3, encoding);
            this.ROComment = GetStringArray(content, numArray[0x22], 80, 3, encoding);
            this.UIComment = GetStringArray(content, numArray[0x23], 80, 3, encoding);
            this.UOComment = GetStringArray(content, numArray[0x24], 80, 3, encoding);
            this.SIComment = GetStringArray(content, numArray[0x25], 80, 3, encoding);
            this.SOComment = GetStringArray(content, numArray[0x26], 80, 3, encoding);
            this.WIComment = GetStringArray(content, numArray[0x27], 80, 3, encoding);
            this.WOComment = GetStringArray(content, numArray[40], 80, 3, encoding);
            this.WSIComment = GetStringArray(content, numArray[0x29], 80, 3, encoding);
            this.AIComment = GetStringArray(content, numArray[0x2a], 80, 3, encoding);
            this.AOComment = GetStringArray(content, numArray[0x2b], 80, 3, encoding);
            this.GIComment = GetStringArray(content, numArray[0x2c], 80, 3, encoding);
            this.GOComment = GetStringArray(content, numArray[0x2d], 80, 3, encoding);
            this.STRREGComment = GetStringArray(content, numArray[0x2e], 80, 3, encoding);
            this.STRREG_COMMENT_Comment = GetStringArray(content, numArray[0x2f], 80, 3, encoding);
            this.isIni = true;
        }

        public static OperateResult<FanucData> PraseFrom(byte[] content)
        {
            FanucData data = new FanucData();
            data.LoadByContent(content);
            return OperateResult.CreateSuccessResult<FanucData>(data);
        }

        public override string ToString()
        {
            if (!this.isIni)
            {
                return "NULL";
            }
            StringBuilder sb = new StringBuilder();
            AppendStringBuilder(sb, "AlarmList", (from m in this.AlarmList select m.ToString()).ToArray<string>());
            AppendStringBuilder(sb, "AlarmCurrent", this.AlarmCurrent.ToString());
            AppendStringBuilder(sb, "AlarmPassword", this.AlarmPassword.ToString());
            AppendStringBuilder(sb, "CurrentPose", this.CurrentPose.ToString());
            AppendStringBuilder(sb, "CurrentPoseUF", this.CurrentPoseUF.ToString());
            AppendStringBuilder(sb, "CurrentPose2", this.CurrentPose2.ToString());
            AppendStringBuilder(sb, "CurrentPose3", this.CurrentPose3.ToString());
            AppendStringBuilder(sb, "CurrentPose4", this.CurrentPose4.ToString());
            AppendStringBuilder(sb, "CurrentPose5", this.CurrentPose5.ToString());
            AppendStringBuilder(sb, "Task", this.Task.ToString());
            AppendStringBuilder(sb, "TaskIgnoreMacro", this.TaskIgnoreMacro.ToString());
            AppendStringBuilder(sb, "TaskIgnoreKarel", this.TaskIgnoreKarel.ToString());
            AppendStringBuilder(sb, "TaskIgnoreMacroKarel", this.TaskIgnoreMacroKarel.ToString());
            AppendStringBuilder(sb, "PosRegGP1", (from m in this.PosRegGP1 select m.ToString()).ToArray<string>());
            AppendStringBuilder(sb, "PosRegGP2", (from m in this.PosRegGP2 select m.ToString()).ToArray<string>());
            AppendStringBuilder(sb, "PosRegGP3", (from m in this.PosRegGP3 select m.ToString()).ToArray<string>());
            AppendStringBuilder(sb, "PosRegGP4", (from m in this.PosRegGP4 select m.ToString()).ToArray<string>());
            AppendStringBuilder(sb, "PosRegGP5", (from m in this.PosRegGP5 select m.ToString()).ToArray<string>());
            AppendStringBuilder(sb, "FAST_CLOCK", this.FAST_CLOCK.ToString());
            AppendStringBuilder(sb, "Timer10_TIMER_VAL", this.Timer10_TIMER_VAL.ToString());
            AppendStringBuilder(sb, "MOR_GRP_CURRENT_ANG", this.MOR_GRP_CURRENT_ANG.ToString());
            AppendStringBuilder(sb, "DUTY_TEMP", this.DUTY_TEMP.ToString());
            AppendStringBuilder(sb, "TIMER10_COMMENT", this.TIMER10_COMMENT.ToString());
            AppendStringBuilder(sb, "TIMER2_COMMENT", this.TIMER2_COMMENT.ToString());
            AppendStringBuilder(sb, "MNUTOOL1_1", this.MNUTOOL1_1.ToString());
            AppendStringBuilder(sb, "HTTPKCL_CMDS", this.HTTPKCL_CMDS.ToString());
            AppendStringBuilder(sb, "NumReg1", SoftBasic.ArrayFormat<int>(this.NumReg1));
            AppendStringBuilder(sb, "NumReg2", SoftBasic.ArrayFormat<float>(this.NumReg2));
            AppendStringBuilder(sb, "DataPosRegMG", (from m in this.DataPosRegMG select m.ToString()).ToArray<string>());
            AppendStringBuilder(sb, "DIComment", SoftBasic.ArrayFormat<string>(this.DIComment));
            AppendStringBuilder(sb, "DOComment", SoftBasic.ArrayFormat<string>(this.DOComment));
            AppendStringBuilder(sb, "RIComment", SoftBasic.ArrayFormat<string>(this.RIComment));
            AppendStringBuilder(sb, "ROComment", SoftBasic.ArrayFormat<string>(this.ROComment));
            AppendStringBuilder(sb, "UIComment", SoftBasic.ArrayFormat<string>(this.UIComment));
            AppendStringBuilder(sb, "UOComment", SoftBasic.ArrayFormat<string>(this.UOComment));
            AppendStringBuilder(sb, "SIComment", SoftBasic.ArrayFormat<string>(this.SIComment));
            AppendStringBuilder(sb, "SOComment", SoftBasic.ArrayFormat<string>(this.SOComment));
            AppendStringBuilder(sb, "WIComment", SoftBasic.ArrayFormat<string>(this.WIComment));
            AppendStringBuilder(sb, "WOComment", SoftBasic.ArrayFormat<string>(this.WOComment));
            AppendStringBuilder(sb, "WSIComment", SoftBasic.ArrayFormat<string>(this.WSIComment));
            AppendStringBuilder(sb, "AIComment", SoftBasic.ArrayFormat<string>(this.AIComment));
            AppendStringBuilder(sb, "AOComment", SoftBasic.ArrayFormat<string>(this.AOComment));
            AppendStringBuilder(sb, "GIComment", SoftBasic.ArrayFormat<string>(this.GIComment));
            AppendStringBuilder(sb, "GOComment", SoftBasic.ArrayFormat<string>(this.GOComment));
            AppendStringBuilder(sb, "STRREGComment", SoftBasic.ArrayFormat<string>(this.STRREGComment));
            AppendStringBuilder(sb, "STRREG_COMMENT_Comment", SoftBasic.ArrayFormat<string>(this.STRREG_COMMENT_Comment));
            return sb.ToString();
        }

        public string[] AIComment { get; set; }

        public FanucAlarm AlarmCurrent { get; set; }

        public FanucAlarm[] AlarmList { get; set; }

        public FanucAlarm AlarmPassword { get; set; }

        public string[] AOComment { get; set; }

        public FanucPose CurrentPose { get; set; }

        public FanucPose CurrentPose2 { get; set; }

        public FanucPose CurrentPose3 { get; set; }

        public FanucPose CurrentPose4 { get; set; }

        public FanucPose CurrentPose5 { get; set; }

        public FanucPose CurrentPoseUF { get; set; }

        public FanucPose[] DataPosRegMG { get; set; }

        public string[] DIComment { get; set; }

        public string[] DOComment { get; set; }

        public float DUTY_TEMP { get; set; }

        public int FAST_CLOCK { get; set; }

        public string[] GIComment { get; set; }

        public string[] GOComment { get; set; }

        public string HTTPKCL_CMDS { get; set; }

        public FanucPose MNUTOOL1_1 { get; set; }

        public float MOR_GRP_CURRENT_ANG { get; set; }

        public int[] NumReg1 { get; set; }

        public float[] NumReg2 { get; set; }

        public FanucPose[] PosRegGP1 { get; set; }

        public FanucPose[] PosRegGP2 { get; set; }

        public FanucPose[] PosRegGP3 { get; set; }

        public FanucPose[] PosRegGP4 { get; set; }

        public FanucPose[] PosRegGP5 { get; set; }

        public string[] RIComment { get; set; }

        public string[] ROComment { get; set; }

        public string[] SIComment { get; set; }

        public string[] SOComment { get; set; }

        public string[] STRREG_COMMENT_Comment { get; set; }

        public string[] STRREGComment { get; set; }

        public FanucTask Task { get; set; }

        public FanucTask TaskIgnoreKarel { get; set; }

        public FanucTask TaskIgnoreMacro { get; set; }

        public FanucTask TaskIgnoreMacroKarel { get; set; }

        public string TIMER10_COMMENT { get; set; }

        public int Timer10_TIMER_VAL { get; set; }

        public string TIMER2_COMMENT { get; set; }

        public string[] UIComment { get; set; }

        public string[] UOComment { get; set; }

        public string[] WIComment { get; set; }

        public string[] WOComment { get; set; }

        public string[] WSIComment { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FanucData.<>c <>9 = new FanucData.<>c();
            public static Func<FanucAlarm, string> <>9__185_0;
            public static Func<FanucPose, string> <>9__185_1;
            public static Func<FanucPose, string> <>9__185_2;
            public static Func<FanucPose, string> <>9__185_3;
            public static Func<FanucPose, string> <>9__185_4;
            public static Func<FanucPose, string> <>9__185_5;
            public static Func<FanucPose, string> <>9__185_6;

            internal string <ToString>b__185_0(FanucAlarm m)
            {
                return m.ToString();
            }

            internal string <ToString>b__185_1(FanucPose m)
            {
                return m.ToString();
            }

            internal string <ToString>b__185_2(FanucPose m)
            {
                return m.ToString();
            }

            internal string <ToString>b__185_3(FanucPose m)
            {
                return m.ToString();
            }

            internal string <ToString>b__185_4(FanucPose m)
            {
                return m.ToString();
            }

            internal string <ToString>b__185_5(FanucPose m)
            {
                return m.ToString();
            }

            internal string <ToString>b__185_6(FanucPose m)
            {
                return m.ToString();
            }
        }
    }
}

