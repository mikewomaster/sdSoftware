namespace HslCommunication.CNC.Fanuc
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class SysAllCoors
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double[] <Absolute>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double[] <Machine>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double[] <Relative>k__BackingField;

        public double[] Absolute { get; set; }

        public double[] Machine { get; set; }

        public double[] Relative { get; set; }
    }
}

