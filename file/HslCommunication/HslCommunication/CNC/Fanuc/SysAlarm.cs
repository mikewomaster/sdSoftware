namespace HslCommunication.CNC.Fanuc
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class SysAlarm
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <AlarmId>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <Axis>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Message>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <Type>k__BackingField;

        public int AlarmId { get; set; }

        public short Axis { get; set; }

        public string Message { get; set; }

        public short Type { get; set; }
    }
}

