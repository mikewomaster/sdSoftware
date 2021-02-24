namespace HslCommunication.Robot.FANUC
{
    using HslCommunication.Core;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class FanucAlarm
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <AlarmID>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <AlarmMessage>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <AlarmNumber>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <CauseAlarmID>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <CauseAlarmMessage>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <CauseAlarmNumber>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <Severity>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <SeverityMessage>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <Time>k__BackingField;

        public void LoadByContent(IByteTransform byteTransform, byte[] content, int index, Encoding encoding)
        {
            this.AlarmID = BitConverter.ToInt16(content, index);
            this.AlarmNumber = BitConverter.ToInt16(content, index + 2);
            this.CauseAlarmID = BitConverter.ToInt16(content, index + 4);
            this.CauseAlarmNumber = BitConverter.ToInt16(content, index + 6);
            this.Severity = BitConverter.ToInt16(content, index + 8);
            if (BitConverter.ToInt16(content, index + 10) > 0)
            {
                this.Time = new DateTime(BitConverter.ToInt16(content, index + 10), BitConverter.ToInt16(content, index + 12), BitConverter.ToInt16(content, index + 14), BitConverter.ToInt16(content, index + 0x10), BitConverter.ToInt16(content, index + 0x12), BitConverter.ToInt16(content, index + 20));
            }
            this.AlarmMessage = encoding.GetString(content, index + 0x16, 80).Trim(new char[1]);
            this.CauseAlarmMessage = encoding.GetString(content, index + 0x66, 80).Trim(new char[1]);
            this.SeverityMessage = encoding.GetString(content, index + 0xb6, 0x12).Trim(new char[1]);
        }

        public static FanucAlarm PraseFrom(IByteTransform byteTransform, byte[] content, int index, Encoding encoding)
        {
            FanucAlarm alarm = new FanucAlarm();
            alarm.LoadByContent(byteTransform, content, index, encoding);
            return alarm;
        }

        public override string ToString()
        {
            return string.Format("FanucAlarm ID[{0},{1},{2},{3},{4}]{5}{6}{7}{8}{9}{10}", new object[] { this.AlarmID, this.AlarmNumber, this.CauseAlarmID, this.CauseAlarmNumber, this.Severity, Environment.NewLine, this.AlarmMessage, Environment.NewLine, this.CauseAlarmMessage, Environment.NewLine, this.SeverityMessage });
        }

        public short AlarmID { get; set; }

        public string AlarmMessage { get; set; }

        public short AlarmNumber { get; set; }

        public short CauseAlarmID { get; set; }

        public string CauseAlarmMessage { get; set; }

        public short CauseAlarmNumber { get; set; }

        public short Severity { get; set; }

        public string SeverityMessage { get; set; }

        public DateTime Time { get; set; }
    }
}

