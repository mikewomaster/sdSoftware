namespace HslCommunication.Robot.FANUC
{
    using HslCommunication.Core;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class FanucTask
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <LineNumber>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ParentProgramName>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ProgramName>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <State>k__BackingField;

        public void LoadByContent(IByteTransform byteTransform, byte[] content, int index, Encoding encoding)
        {
            this.ProgramName = encoding.GetString(content, index, 0x10).Trim(new char[1]);
            this.LineNumber = BitConverter.ToInt16(content, index + 0x10);
            this.State = BitConverter.ToInt16(content, index + 0x12);
            this.ParentProgramName = encoding.GetString(content, index + 20, 0x10).Trim(new char[1]);
        }

        public static FanucTask PraseFrom(IByteTransform byteTransform, byte[] content, int index, Encoding encoding)
        {
            FanucTask task = new FanucTask();
            task.LoadByContent(byteTransform, content, index, encoding);
            return task;
        }

        public override string ToString()
        {
            return string.Format("ProgramName[{0}] LineNumber[{1}] State[{2}] ParentProgramName[{3}]", new object[] { this.ProgramName, this.LineNumber, this.State, this.ParentProgramName });
        }

        public short LineNumber { get; set; }

        public string ParentProgramName { get; set; }

        public string ProgramName { get; set; }

        public short State { get; set; }
    }
}

