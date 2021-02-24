namespace HslCommunication.Profinet.Geniitek
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class VibrationSensorPeekValue
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <AcceleratedSpeedX>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <AcceleratedSpeedY>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <AcceleratedSpeedZ>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <OffsetX>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <OffsetY>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <OffsetZ>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <SendingInterval>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <SpeedX>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <SpeedY>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <SpeedZ>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <Temperature>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <Voltage>k__BackingField;

        public float AcceleratedSpeedX { get; set; }

        public float AcceleratedSpeedY { get; set; }

        public float AcceleratedSpeedZ { get; set; }

        public int OffsetX { get; set; }

        public int OffsetY { get; set; }

        public int OffsetZ { get; set; }

        public int SendingInterval { get; set; }

        public float SpeedX { get; set; }

        public float SpeedY { get; set; }

        public float SpeedZ { get; set; }

        public float Temperature { get; set; }

        public float Voltage { get; set; }
    }
}

