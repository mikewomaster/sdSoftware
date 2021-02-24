namespace HslCommunication.Profinet.Geniitek
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct VibrationSensorActualValue
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <AcceleratedSpeedX>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <AcceleratedSpeedY>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <AcceleratedSpeedZ>k__BackingField;
        public float AcceleratedSpeedX { get; set; }
        public float AcceleratedSpeedY { get; set; }
        public float AcceleratedSpeedZ { get; set; }
        public override string ToString()
        {
            return string.Format("ActualValue[{0},{1},{2}]", this.AcceleratedSpeedX, this.AcceleratedSpeedY, this.AcceleratedSpeedZ);
        }
    }
}

