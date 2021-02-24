namespace HslCommunication.CNC.Fanuc
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class CutterInfo
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double <LengthSharpOffset>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double <LengthWearOffset>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double <RadiusSharpOffset>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double <RadiusWearOffset>k__BackingField;

        public override string ToString()
        {
            return string.Format("LengthSharpOffset:{0:10} LengthWearOffset:{1:10} RadiusSharpOffset:{2:10} RadiusWearOffset:{3:10}", new object[] { this.LengthSharpOffset, this.LengthWearOffset, this.RadiusSharpOffset, this.RadiusWearOffset });
        }

        public double LengthSharpOffset { get; set; }

        public double LengthWearOffset { get; set; }

        public double RadiusSharpOffset { get; set; }

        public double RadiusWearOffset { get; set; }
    }
}

