namespace HslCommunication.Core
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class MqttFileOperateInfo
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <FileNames>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Groups>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Operate>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TimeSpan <TimeCost>k__BackingField;

        public string[] FileNames { get; set; }

        public string Groups { get; set; }

        public string Operate { get; set; }

        public TimeSpan TimeCost { get; set; }
    }
}

