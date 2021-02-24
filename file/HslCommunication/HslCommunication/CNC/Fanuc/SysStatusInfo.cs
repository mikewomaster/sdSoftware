namespace HslCommunication.CNC.Fanuc
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class SysStatusInfo
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <Alarm>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <Dummy>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <Edit>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <Emergency>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <Motion>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <MSTB>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private CNCRunStatus <RunStatus>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <TMMode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private CNCWorkMode <WorkMode>k__BackingField;

        public override string ToString()
        {
            return (string.Format("Dummy: {0}, TMMode:{1}, WorkMode:{2}, RunStatus:{3}, ", new object[] { this.Dummy, this.TMMode, this.WorkMode, this.RunStatus }) + string.Format("Motion:{0}, MSTB:{1}, Emergency:{2}, Alarm:{3}, Edit:{4}", new object[] { this.Motion, this.MSTB, this.Emergency, this.Alarm, this.Edit }));
        }

        public short Alarm { get; set; }

        public short Dummy { get; set; }

        public short Edit { get; set; }

        public short Emergency { get; set; }

        public short Motion { get; set; }

        public short MSTB { get; set; }

        public CNCRunStatus RunStatus { get; set; }

        public short TMMode { get; set; }

        public CNCWorkMode WorkMode { get; set; }
    }
}

